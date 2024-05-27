using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.Online.SharePoint.TenantManagement;
using Microsoft.SharePoint.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PnP.PowerShell.Commands.Model
{
     public class SPOTenantInternalSettings
     {
        #region Internal Settings properties
        public bool SitePagesEnabled { private set; get; }
        public bool DisableSelfServiceSiteCreation { private set; get; }
        public bool EnableAutoNewsDigest { private set;get; }
        public string CustomFormUrl { private set; get; }
        public bool AutoQuotaEnabled { get; private set; }
        public bool DisableGroupify { get; private set; }
        public bool IncludeAtAGlanceInShareEmails { get; private set; }
        public string MailFromAddress { get; private set; }
        public bool MobileNotificationIsEnabledForSharepoint { get; private set; }
        public string NewSiteManagedPath { get; private set; }
        public bool NewSubsiteInModernOffForAll { get; private set; }
        public bool NewSubsiteInModernOffForModernTemplates { get; private set; }
        public string NewTeamSiteManagedPath { get; private set; }
        public string ParentSiteUrl { get; private set; }
        public string PolicyOption { get; private set; }
        public bool RequireSecondaryContact { get; private set; }
        public bool ShowSelfServiceSiteCreation { get; private set; }
        public int SiteCreationDefaultStorageQuota { get; private set; }
        public bool SiteCreationNewUX { get; private set; }
        public string SmtpServer { get; private set; }
        public bool SPListModernUXOff { get; private set; }
        public int TenantDefaultTimeZoneId { get; private set; }
        public string[] AvailableManagedPathsForSiteCreation { get; private set; }

        #endregion

        public SPOTenantInternalSettings(Tenant tenant, ClientContext clientContext)
        {
            try
            {
                this.initSPOTenantInternalSettings(clientContext);
            }
            catch
            {
            }
        }
        private void initSPOTenantInternalSettings(ClientContext clientContext)
        {
            var httpClient = PnP.Framework.Http.PnPHttpClient.Instance.GetHttpClient(clientContext);
            var internalSettingsData = Utilities.REST.RestHelper.GetAsync<TenantInternalSettings>(httpClient, $"{clientContext.Url}_api/SPOInternalUseOnly.TenantAdminSettings", clientContext.GetAccessToken(), false).GetAwaiter().GetResult();

            SitePagesEnabled = internalSettingsData.SitePagesEnabled.Value;
            DisableSelfServiceSiteCreation = internalSettingsData.DisableSelfServiceSiteCreation.Value;
            EnableAutoNewsDigest = internalSettingsData.EnableAutoNewsDigest.Value;
            CustomFormUrl = internalSettingsData.CustomFormUrl.Value;
            AutoQuotaEnabled = internalSettingsData.AutoQuotaEnabled.Value;
            DisableGroupify = internalSettingsData.DisableGroupify.Value;
            IncludeAtAGlanceInShareEmails = internalSettingsData.IncludeAtAGlanceInShareEmails.Value;
            MailFromAddress = internalSettingsData.MailFromAddress.Value;
            MobileNotificationIsEnabledForSharepoint = internalSettingsData.MobileNotificationIsEnabledForSharepoint.Value;
            NewSiteManagedPath = internalSettingsData.NewSiteManagedPath.Value;
            NewSubsiteInModernOffForAll = internalSettingsData.NewSubsiteInModernOffForAll.Value;
            NewSubsiteInModernOffForModernTemplates = internalSettingsData.NewSubsiteInModernOffForModernTemplates.Value;
            NewTeamSiteManagedPath = internalSettingsData.NewTeamSiteManagedPath.Value;
            ParentSiteUrl = internalSettingsData.ParentSiteUrl.Value;
            PolicyOption = internalSettingsData.PolicyOption.Value;
            RequireSecondaryContact = internalSettingsData.RequireSecondaryContact.Value;
            ShowSelfServiceSiteCreation = internalSettingsData.ShowSelfServiceSiteCreation.Value;
            SiteCreationNewUX = internalSettingsData.SiteCreationNewUX.Value;
            SmtpServer = internalSettingsData.SmtpServer.Value;
            SPListModernUXOff = internalSettingsData.SPListModernUXOff.Value;
            TenantDefaultTimeZoneId = internalSettingsData.TenantDefaultTimeZoneId.Value;
            AvailableManagedPathsForSiteCreation = internalSettingsData.AvailableManagedPathsForSiteCreation;
        }
        private class TenantInternalSettings
        {
            #region Properties

            [JsonProperty("AutoQuotaEnabled")]
            public SettingsBoolProperty AutoQuotaEnabled { get; set; }

            [JsonProperty("AvailableManagedPathsForSiteCreation")]
            public string[] AvailableManagedPathsForSiteCreation { get; set; }

            [JsonProperty("IncludeAtAGlanceInShareEmails")]
            public SettingsBoolProperty IncludeAtAGlanceInShareEmails { get; set; }

            [JsonProperty("MailFromAddress")]
            public SettingsStringProperty MailFromAddress { get; set; }

            [JsonProperty("MobileNotificationIsEnabledForSharepoint")]
            public SettingsBoolProperty MobileNotificationIsEnabledForSharepoint { get; set; }

            [JsonProperty("NewSiteManagedPath")]
            public SettingsStringProperty NewSiteManagedPath { get; set; }

            [JsonProperty("ParentSiteUrl")]
            public SettingsStringProperty ParentSiteUrl { get; set; }

            [JsonProperty("PolicyOption")]
            public SettingsStringProperty PolicyOption { get; set; }

            [JsonProperty("RequireSecondaryContact")]
            public SettingsBoolProperty RequireSecondaryContact { get; set; }

            [JsonProperty("ShowSelfServiceSiteCreation")]
            public SettingsBoolProperty ShowSelfServiceSiteCreation { get; set; }

            [JsonProperty("SiteCreationNewUX")]
            public SettingsBoolProperty SiteCreationNewUX { get; set; }

            [JsonProperty("SmtpServer")]
            public SettingsStringProperty SmtpServer { get; set; }

            [JsonProperty("SPListModernUXOff")]
            public SettingsBoolProperty SPListModernUXOff { get; set; }

            [JsonProperty("TenantDefaultTimeZoneId")]
            public SettingsIntProperty TenantDefaultTimeZoneId { get; set; }

            [JsonProperty("SitePagesEnabled")]
            public SettingsBoolProperty SitePagesEnabled { get; set; }

            [JsonProperty("DisableGroupify")]
            public SettingsBoolProperty DisableGroupify { get; set; }

            [JsonProperty("CustomFormUrl")]
            public SettingsStringProperty CustomFormUrl { get; set; }

            [JsonProperty("EnableAutoNewsDigest")]
            public SettingsBoolProperty EnableAutoNewsDigest { get; set; }

            [JsonProperty("DisableSelfServiceSiteCreation")]
            public SettingsBoolProperty DisableSelfServiceSiteCreation { get; set; }

            [JsonProperty("NewSubsiteInModernOffForAll")]
            public SettingsBoolProperty NewSubsiteInModernOffForAll { get; set; }
            [JsonProperty("NewSubsiteInModernOffForModernTemplates")]
            public SettingsBoolProperty NewSubsiteInModernOffForModernTemplates { get; set; }
            [JsonProperty("NewTeamSiteManagedPath")]
            public SettingsStringProperty NewTeamSiteManagedPath { get; set; }

            #endregion

            #region Helper Types
            public class SettingsBoolProperty
            {
                [JsonProperty("IsReadOnly")]
                public bool IsReadOnly { get; set; }

                [JsonProperty("Value")]
                public bool Value { get; set; }
            }
            public class SettingsIntProperty
            {
                [JsonProperty("IsReadOnly")]
                public bool IsReadOnly { get; set; }

                [JsonProperty("Value")]
                public int Value { get; set; }
            }
            public class SettingsStringProperty
            {
                [JsonProperty("IsReadOnly")]
                public bool IsReadOnly { get; set; }

                [JsonProperty("Value")]
                public string Value { get; set; }
            }

            #endregion
        }

    }
    
}
