using System;
using System.Configuration;
using System.Linq;
using System.Linq.Dynamic;
using AngusAnyWhere.SecurityIntegration.Cfg;
using log4net;
using Topshelf;

namespace ConfigurationSectionDesignerDemo
{
	class Program
	{
		static void Main(string[] args)
		{
			var config = ConfigurationManager.GetSection("angusSettings") as AngusSettings;
			var tenantApiSettings = config.TenantApiSettings;
			var generalSettings = config.GeneralSettings;

			var baseUrl = tenantApiSettings.BaseUrl;
			var tokenEndpoint = tenantApiSettings.TokenEndpoint;
			var clientId = tenantApiSettings.ClientId;
			var clientSecret = tenantApiSettings.ClientSecret;
			var grantType = tenantApiSettings.GrantType;
			var scope = tenantApiSettings.Scope;
			var buildingId = tenantApiSettings.BuildingId;
			var numImmediateRetries = tenantApiSettings.NumImmediateRetries;
			var numDelayedRetries = tenantApiSettings.NumDelayedRetries;
			var delaySecs = tenantApiSettings.DelaySecs;
			var retryStatusCodes = tenantApiSettings.RetryStatusCodes.Split(';').Select(x => x.Trim()).Where(x => !string.IsNullOrEmpty(x)).ToArray();

			var tenantName = generalSettings.TenantName;
			var usesVerifyStatus = generalSettings.UsesVerifyStatus;
			var verifyDelay = generalSettings.VerifyDelay;
			var facilityCode = generalSettings.FacilityCode;
			var usesEmailNotification = generalSettings.UsesEmailNotification;
			var fromEmailAddress = generalSettings.FromEmailAddress;
			var toEmailAddresses = generalSettings.ToEmailAddresses;
			var filters = generalSettings.Filters;

		}
	}
}
