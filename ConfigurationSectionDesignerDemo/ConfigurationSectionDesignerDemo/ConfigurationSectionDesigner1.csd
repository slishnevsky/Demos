<?xml version="1.0" encoding="utf-8"?>
<configurationSectionModel xmlns:dm0="http://schemas.microsoft.com/VisualStudio/2008/DslTools/Core" dslVersion="1.0.0.0" Id="c60012cb-1104-4ae1-80ab-3c72e4415acc" namespace="ConfigurationSectionDesignerDemo" xmlSchemaNamespace="urn:ConfigurationSectionDesignerDemo" xmlns="http://schemas.microsoft.com/dsltools/ConfigurationSectionDesigner">
  <typeDefinitions>
    <externalType name="String" namespace="System" />
    <externalType name="Boolean" namespace="System" />
    <externalType name="Int32" namespace="System" />
    <externalType name="Int64" namespace="System" />
    <externalType name="Single" namespace="System" />
    <externalType name="Double" namespace="System" />
    <externalType name="DateTime" namespace="System" />
    <externalType name="TimeSpan" namespace="System" />
  </typeDefinitions>
  <configurationElements>
    <configurationSection name="AngusSettings" codeGenOptions="Singleton, XmlnsProperty" xmlSectionName="angusSettings">
      <elementProperties>
        <elementProperty name="TenantApiSettings" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="tenantApiSettings" isReadOnly="false">
          <type>
            <configurationElementMoniker name="/c60012cb-1104-4ae1-80ab-3c72e4415acc/TenantApiSettings" />
          </type>
        </elementProperty>
        <elementProperty name="GeneralSettings" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="generalSettings" isReadOnly="false">
          <type>
            <configurationElementMoniker name="/c60012cb-1104-4ae1-80ab-3c72e4415acc/GeneralSettings" />
          </type>
        </elementProperty>
      </elementProperties>
    </configurationSection>
    <configurationElement name="TenantApiSettings">
      <attributeProperties>
        <attributeProperty name="BaseUrl" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="baseUrl" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/c60012cb-1104-4ae1-80ab-3c72e4415acc/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="TokenEndpoint" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="tokenEndpoint" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/c60012cb-1104-4ae1-80ab-3c72e4415acc/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="ClientId" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="clientId" isReadOnly="false" defaultValue="&quot;0&quot;">
          <type>
            <externalTypeMoniker name="/c60012cb-1104-4ae1-80ab-3c72e4415acc/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="ClientSecret" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="clientSecret" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/c60012cb-1104-4ae1-80ab-3c72e4415acc/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="BuildingId" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="buildingId" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/c60012cb-1104-4ae1-80ab-3c72e4415acc/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="GrantType" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="grantType" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/c60012cb-1104-4ae1-80ab-3c72e4415acc/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="Scope" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="scope" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/c60012cb-1104-4ae1-80ab-3c72e4415acc/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="NumImmediateRetries" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="numImmediateRetries" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/c60012cb-1104-4ae1-80ab-3c72e4415acc/Int32" />
          </type>
        </attributeProperty>
        <attributeProperty name="NumDelayedRetries" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="numDelayedRetries" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/c60012cb-1104-4ae1-80ab-3c72e4415acc/Int32" />
          </type>
        </attributeProperty>
        <attributeProperty name="DelaySecs" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="delaySecs" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/c60012cb-1104-4ae1-80ab-3c72e4415acc/Int32" />
          </type>
        </attributeProperty>
        <attributeProperty name="RetryStatusCodes" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="retryStatusCodes" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/c60012cb-1104-4ae1-80ab-3c72e4415acc/String" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
    <configurationElement name="GeneralSettings">
      <attributeProperties>
        <attributeProperty name="TenantName" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="tenantName" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/c60012cb-1104-4ae1-80ab-3c72e4415acc/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="UsesVerifyStatus" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="usesVerifyStatus" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/c60012cb-1104-4ae1-80ab-3c72e4415acc/Boolean" />
          </type>
        </attributeProperty>
        <attributeProperty name="VerifyDelay" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="verifyDelay" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/c60012cb-1104-4ae1-80ab-3c72e4415acc/Int32" />
          </type>
        </attributeProperty>
        <attributeProperty name="FacilityCode" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="facilityCode" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/c60012cb-1104-4ae1-80ab-3c72e4415acc/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="UsesEmailNotification" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="usesEmailNotification" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/c60012cb-1104-4ae1-80ab-3c72e4415acc/Boolean" />
          </type>
        </attributeProperty>
        <attributeProperty name="FromEmailAddress" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="fromEmailAddress" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/c60012cb-1104-4ae1-80ab-3c72e4415acc/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="ToEmailAddresses" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="toEmailAddresses" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/c60012cb-1104-4ae1-80ab-3c72e4415acc/String" />
          </type>
        </attributeProperty>
      </attributeProperties>
      <elementProperties>
        <elementProperty name="Filters" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="filters" isReadOnly="false">
          <type>
            <configurationElementCollectionMoniker name="/c60012cb-1104-4ae1-80ab-3c72e4415acc/Filters" />
          </type>
        </elementProperty>
      </elementProperties>
    </configurationElement>
    <configurationElementCollection name="Filters" xmlItemName="filter" codeGenOptions="Indexer, AddMethod, RemoveMethod, GetItemMethods">
      <itemType>
        <configurationElementMoniker name="/c60012cb-1104-4ae1-80ab-3c72e4415acc/Filter" />
      </itemType>
    </configurationElementCollection>
    <configurationElement name="Filter">
      <attributeProperties>
        <attributeProperty name="Name" isRequired="true" isKey="true" isDefaultCollection="false" xmlName="name" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/c60012cb-1104-4ae1-80ab-3c72e4415acc/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="Value" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="value" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/c60012cb-1104-4ae1-80ab-3c72e4415acc/String" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
  </configurationElements>
  <propertyValidators>
    <validators>
      <regexStringValidator name="ClassDotPropertyValidator" regularExpression="^[A-Za-z_][A-Za-z0-9_]*\.[A-Za-z_][A-Za-z0-9_]*$" />
      <regexStringValidator name="SemicolonSeparatedStringsValidator" regularExpression="^[A-Za-z0-9_]*(\;[A-Za-z0-9_]*)*$" />
      <regexStringValidator name="SemicolonSeparatedNumbersValidator" regularExpression="^([0-9]+;)*[0-9]+$" />
    </validators>
  </propertyValidators>
</configurationSectionModel>