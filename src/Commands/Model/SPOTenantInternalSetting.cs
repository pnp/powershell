using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using System.Text.Json;

namespace PnP.PowerShell.Commands.Model
{
     public class SPOTenantInternalSetting
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

        public SPOTenantInternalSetting(Tenant tenant, ClientContext clientContext)
        {
            try
            {
                this.initSPOTenantInternalSetting(clientContext);
            }
            catch
            {
            }
        }
        private void initSPOTenantInternalSetting(ClientContext clientContext)
        {
            var httpClient = PnP.Framework.Http.PnPHttpClient.Instance.GetHttpClient(clientContext);
            var internalSettingsData = Utilities.REST.RestHelper.Get<TenantInternalSetting>(httpClient, $"{clientContext.Url}_api/SPOInternalUseOnly.TenantAdminSettings", clientContext.GetAccessToken(), false);

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
        private class TenantInternalSetting
        {
            #region Properties
            public SettingsBoolProperty AutoQuotaEnabled { get; set; }
            public string[] AvailableManagedPathsForSiteCreation { get; set; }
            public SettingsBoolProperty IncludeAtAGlanceInShareEmails { get; set; }
            public SettingsStringProperty MailFromAddress { get; set; }
            public SettingsBoolProperty MobileNotificationIsEnabledForSharepoint { get; set; }
            public SettingsStringProperty NewSiteManagedPath { get; set; }
            public SettingsStringProperty ParentSiteUrl { get; set; }
            public SettingsStringProperty PolicyOption { get; set; }
            public SettingsBoolProperty RequireSecondaryContact { get; set; }
            public SettingsBoolProperty ShowSelfServiceSiteCreation { get; set; }
            public SettingsBoolProperty SiteCreationNewUX { get; set; }
            public SettingsStringProperty SmtpServer { get; set; }
            public SettingsBoolProperty SPListModernUXOff { get; set; }
            public SettingsIntProperty TenantDefaultTimeZoneId { get; set; }
            public SettingsBoolProperty SitePagesEnabled { get; set; }
            public SettingsBoolProperty DisableGroupify { get; set; }
            public SettingsStringProperty CustomFormUrl { get; set; }
            public SettingsBoolProperty EnableAutoNewsDigest { get; set; }
            public SettingsBoolProperty DisableSelfServiceSiteCreation { get; set; }
            public SettingsBoolProperty NewSubsiteInModernOffForAll { get; set; }
            public SettingsBoolProperty NewSubsiteInModernOffForModernTemplates { get; set; }
            public SettingsStringProperty NewTeamSiteManagedPath { get; set; }

            #endregion

            #region Helper Types
            public class SettingsBoolProperty
            {
                public bool IsReadOnly { get; set; }
                public bool Value { get; set; }
            }
            public class SettingsIntProperty
            {
                public bool IsReadOnly { get; set; }
                public int Value { get; set; }
            }
            public class SettingsStringProperty
            {
                public bool IsReadOnly { get; set; }
                public string Value { get; set; }
            }

            #endregion
        }

    }
    
}
