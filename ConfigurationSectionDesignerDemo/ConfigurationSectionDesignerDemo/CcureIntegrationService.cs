using log4net;
using log4net.Config;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using AngusAnyWhere.SecurityIntegration.Cfg;
using Newtonsoft.Json;
using SoftwareHouse.CrossFire.Common.ClientInterfaceLayer;
using SoftwareHouse.CrossFire.Common.Core;
using SoftwareHouse.CrossFire.Common.DataServiceLayer;
using SoftwareHouse.NextGen.Common.SecurityObjects;

namespace ConfigurationSectionDesignerDemo
{
	public class CcureIntegrationService
	{
		private static readonly ILog _log = LogManager.GetLogger(typeof(CcureIntegrationService));

		private AngusSettings _appSettings;
		private TenantApiService _tenantApiService;

		public void Start()
		{
			try
			{
				InitializeConfiguration();
				RegisterTypeClientCallbackEvents();
				ConnectToServer();
				_log.Debug("Integration Service Started");
			}
			catch (Exception e)
			{
				_log.Error(e.Message);
				throw;
			}
		}



		public void Stop()
		{
			ClientServerConnection.Instance.Dispose();
			_log.Info("Integration Service Stopped");
		}

		private void InitializeConfiguration()
		{
			_log.Info("Initializing Configuration...");
			_appSettings = ConfigurationManager.GetSection("angusSettings") as AngusSettings;
			_tenantApiService = new TenantApiService(_appSettings?.TenantApiSettings);
		}

		private void ConnectToServer()
		{
			_log.Info("Connecting to CCURE Server...");
			ClientServerConnection.LostServerCommunication += (sender, args) => _log.Error((args as ServerStatusEventArgs)?.StatusMessage);
			ClientServerConnection.ServerCommunicationRestored += (sender, args) => _log.Debug((args as ServerStatusEventArgs)?.StatusMessage);
			var connected = ClientServerConnection.Instance.ConnectToServer();
			if (!connected) throw new Exception($"Unable to connect to [{ClientServerConnection.Instance.ServerMachineName}] Server");
			_log.Debug($"Successfully connected to [{ClientServerConnection.Instance.ServerMachineName}] server");
		}

		public void RegisterTypeClientCallbackEvents()
		{
			_log.Info("Registering Callback Events...");
			ClientServerConnection.Instance.RegisterTypeClientCallback(typeof(ObjectCreatedEventArgs), new[] { typeof(Personnel).FullName }, PersonnelEventHandler, NotificationReturn.Actual);
			ClientServerConnection.Instance.RegisterTypeClientCallback(typeof(ObjectChangedEventArgs), new[] { typeof(Personnel).FullName }, PersonnelEventHandler, NotificationReturn.Actual);
			ClientServerConnection.Instance.RegisterTypeClientCallback(typeof(ObjectDeletedEventArgs), new[] { typeof(Personnel).FullName }, PersonnelEventHandler, NotificationReturn.Actual);
		}

		private void PersonnelEventHandler(object sender, EventArgs e)
		{
			if (e is ObjectCreatedEventArgs) _log.Info("Personnel Created Event...");
			if (e is ObjectChangedEventArgs) _log.Info("Personnel Changed Event...");
			if (e is ObjectDeletedEventArgs) _log.Info("Personnel Deleted Event...");

			using (var personnel = sender as Personnel)
			{
				if (personnel == null) return;
				var personnelId = personnel.ObjectID.ToString();
				var credentials = (ClientServerConnection.Instance.FindCollection(typeof(Credential), "Credential.PersonnelID = ?", new object[] { personnelId }) as DataServiceCollection)?.ToList();
				var clearances = (ClientServerConnection.Instance.FindCollection(typeof(PersonnelClearancePair), "PersonnelClearancePair.PersonnelId = ?", new object[] { personnelId }) as DataServiceCollection)?.ToList();
				if (MeetsFilterRequirements(personnel, credentials, clearances))
					ProcessPersonnel(personnel, credentials, clearances);
			}
		}

		private bool MeetsFilterRequirements(Personnel personnel, List<DataServiceObject> credentials, List<DataServiceObject> clearances)
		{
			return MeetsFilterRequirements(personnel) && MeetsFilterRequirements(credentials) &&
				   MeetsFilterRequirements(clearances);
		}

		private bool MeetsFilterRequirements(IList<DataServiceObject> collection)
		{
			for (var i = 0; i < collection.Count; i++)
				if (!MeetsFilterRequirements(collection[i]))
					collection.Remove(collection[i]);

			return true;
		}

		private bool MeetsFilterRequirements(DataServiceObject o)
		{
			var filterFound = false;
			foreach (Filter filter in _appSettings.GeneralSettings.Filters)
			{
				var className = filter.Name.Split('.')[0];
				var propName = filter.Name.Split('.')[1];

				// Check if object's class name exists in this filter, if not, skip to next filter
				if (!className.Equals(o.GetType().Name, StringComparison.OrdinalIgnoreCase)) continue;

				filterFound = true;

				// Try to get object's property by filter name, if doesnt exist, , skip to next filter
				var oProperty = o.GetType().GetProperty(propName);
				if (oProperty == null) continue;

				// Get this filter values
				var values = filter.Value.Split(';');
				var oPropertyValue = oProperty.GetValue(o).ToString();
				if (values.Any(value => oPropertyValue.Equals(value))) return true;
			}
			return !filterFound;
		}

		private void ProcessPersonnel(Personnel personnel, List<DataServiceObject> credentials, List<DataServiceObject> clearances)
		{
			var personnelId = personnel.ObjectID.ToString();
			var referenceId = Guid.NewGuid().ToString();
			var multiAccessCardCmd = new AddMultiAccessCard
			{
				ReferenceId = referenceId,
				PersonnelId = personnelId,
				FirstName = personnel.FirstName,
				LastName = personnel.LastName,
				IsActive = !personnel.Disabled
			};

			// Create access cards from the list of external credentials
			var addAccessCards = credentials.OfType<Credential>()
				.Select(credential => new AddAccessCard
			{
				PersonnelId = personnelId,
				FirstName = personnel.FirstName,
				LastName = personnel.LastName,
				FacilityCode = _appSettings.GeneralSettings.FacilityCode,
				AccessCardId = credential.CardNumber.ToString(CultureInfo.InvariantCulture),
				//AccessCardType = chuidFormat.Name, // TODO what should this be?
				EffectiveDate = credential.ActivationDateTime,
				ExpiryDate = credential.ExpirationDateTime,
				IsActive = credential.Active,
				AccessLevels = clearances.OfType<PersonnelClearancePair>()
					.Select(clearance => clearance.ClearanceName)
					.ToArray()
			});

			if (personnel.PrimaryPortraitFullImage != null)
				multiAccessCardCmd.Photo = personnel.PrimaryPortraitFullImage;

			// Assign created access cards to this command
			multiAccessCardCmd.AccessCards = addAccessCards;

			try
			{
				var accessToken = _tenantApiService.GetToken();
				SaveCardholder(multiAccessCardCmd, accessToken);
				VerifyStatus(referenceId, accessToken);
			}
			catch (Exception e)
			{
				_log.Error(e.Message);
			}

		}

		private void SaveCardholder(ITenantApiCmd cmd, AccessTokenBody accessToken)
		{
			var result = _tenantApiService.SaveCardholderMulti(accessToken, (AddMultiAccessCard)cmd);
			HandleResponse(cmd, result);
		}

		private void VerifyStatus(string referenceId, AccessTokenBody accessToken)
		{
			if (!_appSettings.GeneralSettings.UsesVerifyStatus) return;
			Thread.Sleep(_appSettings.GeneralSettings.VerifyDelay);
			_log.Info($"Processing ReferenceId: {referenceId}");
			var cmd = new GetStatus { ReferenceId = referenceId };
			var result = _tenantApiService.GetTransactionStatus(accessToken, cmd);
			HandleResponse(cmd, result);
		}

		private static void HandleResponse(ITenantApiCmd cmd, AngusResponse result)
		{
			if (result == null || result.IsSuccess())
				_log.Info($"Successfully processed command {cmd.GetType().Name} for PersonnelId: {cmd.PersonnelId} and ReferenceId: {cmd.ReferenceId}");
			else
				_log.Error($"Failed to process command {cmd.GetType().Name} for PersonnelId: {cmd.PersonnelId} and ReferenceId: {cmd.ReferenceId}");
		}

	}
}

