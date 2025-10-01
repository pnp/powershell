using Microsoft.SharePoint.Client;
using PnP.Core.Admin.Model.Microsoft365;
using PnP.Core.Admin.Model.SharePoint;
using PnP.Core.Services;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Enums;
using PnP.PowerShell.Commands.Utilities;
using System;
using System.Linq;
using System.Management.Automation;
using System.Text.Json;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.New, "PnPSite")]
    [RequiredApiApplicationPermissions("graph/Group.ReadWrite.All")]
    public class NewSite : PnPSharePointCmdlet, IDynamicParameters
    {
        private const string ParameterSet_COMMUNICATIONBUILTINDESIGN = "Communication Site with Built-In Site Design";
        private const string ParameterSet_COMMUNICATIONCUSTOMDESIGN = "Communication Site with Custom Design";
        private const string ParameterSet_TEAM = "Team Site";
        private const string ParameterSet_TEAMSITENOGROUP = "Team Site No M365 Group";

        [Parameter(Mandatory = true)]
        public SiteType Type;

        [Parameter(Mandatory = false)]
        public Guid HubSiteId;

        private CommunicationSiteParameters _communicationSiteParameters;
        private TeamSiteParameters _teamSiteParameters;
        private TeamSiteWithoutMicrosoft365Group _teamSiteWithoutMicrosoft365GroupParameters;

        [Parameter(Mandatory = false)]
        public SwitchParameter Wait;

        [Parameter(Mandatory = false)]
        public Framework.Enums.TimeZone TimeZone;

        public object GetDynamicParameters()
        {
            switch (Type)
            {
                case SiteType.CommunicationSite:
                    _communicationSiteParameters = new CommunicationSiteParameters();
                    return _communicationSiteParameters;

                case SiteType.TeamSite:
                    _teamSiteParameters = new TeamSiteParameters();
                    return _teamSiteParameters;

                case SiteType.TeamSiteWithoutMicrosoft365Group:
                    _teamSiteWithoutMicrosoft365GroupParameters = new TeamSiteWithoutMicrosoft365Group();
                    return _teamSiteWithoutMicrosoft365GroupParameters;
            }
            return null;
        }

        protected override void ExecuteCmdlet()
        {
            if (Type == SiteType.CommunicationSite)
            {
                EnsureDynamicParameters(_communicationSiteParameters);
                if (!ParameterSpecified("Lcid"))
                {
                    ClientContext.Web.EnsureProperty(w => w.Language);
                    _communicationSiteParameters.Lcid = ClientContext.Web.Language;
                }
                var creationInformation = new Framework.Sites.CommunicationSiteCollectionCreationInformation();
                creationInformation.Title = _communicationSiteParameters.Title;
                creationInformation.Url = _communicationSiteParameters.Url;
                creationInformation.Description = _communicationSiteParameters.Description;
                creationInformation.Classification = _communicationSiteParameters.Classification;
                creationInformation.ShareByEmailEnabled = _communicationSiteParameters.ShareByEmailEnabled;
                creationInformation.Lcid = _communicationSiteParameters.Lcid;
                if (ParameterSpecified(nameof(HubSiteId)))
                {
                    creationInformation.HubSiteId = HubSiteId;
                }
                if (ParameterSetName == ParameterSet_COMMUNICATIONCUSTOMDESIGN)
                {
                    creationInformation.SiteDesignId = _communicationSiteParameters.SiteDesignId;
                }
                else
                {
                    creationInformation.SiteDesign = _communicationSiteParameters.SiteDesign;
                }
                creationInformation.Owner = _communicationSiteParameters.Owner;
                creationInformation.PreferredDataLocation = _communicationSiteParameters.PreferredDataLocation;
                creationInformation.SensitivityLabel = _communicationSiteParameters.SensitivityLabel;

                var returnedContext = Framework.Sites.SiteCollection.Create(ClientContext, creationInformation, noWait: !Wait);
                if (ParameterSpecified(nameof(TimeZone)))
                {
                    returnedContext.Web.EnsureProperties(w => w.RegionalSettings, w => w.RegionalSettings.TimeZones);
                    returnedContext.Web.RegionalSettings.TimeZone = returnedContext.Web.RegionalSettings.TimeZones.Where(t => t.Id == ((int)TimeZone)).First();
                    returnedContext.Web.RegionalSettings.Update();
                    returnedContext.ExecuteQueryRetry();
                    returnedContext.Site.EnsureProperty(s => s.Url);
                    WriteObject(returnedContext.Site.Url);
                }
                else
                {
                    WriteObject(returnedContext.Url);
                }
            }
            else if (Type == SiteType.TeamSite)
            {
                EnsureDynamicParameters(_teamSiteParameters);
                if (!ParameterSpecified("Lcid"))
                {
                    ClientContext.Web.EnsureProperty(w => w.Language);
                    _teamSiteParameters.Lcid = ClientContext.Web.Language;
                }
                var creationInformation = new Framework.Sites.TeamSiteCollectionCreationInformation();

                var groupVisibility = Core.Model.Security.GroupVisibility.Private;
                if (_teamSiteParameters.IsPublic == true)
                {
                    groupVisibility = Core.Model.Security.GroupVisibility.Public;
                }

                var teamSiteOptions = new TeamSiteOptions(_teamSiteParameters.Alias, _teamSiteParameters.Title)
                {
                    Classification = _teamSiteParameters.Classification,
                    Description = _teamSiteParameters.Description,
                    Visibility = groupVisibility,
                    Owners = _teamSiteParameters.Owners,
                    Language = (Core.Admin.Model.SharePoint.Language)_teamSiteParameters.Lcid,
                    Members = _teamSiteParameters.Members,
                    WelcomeEmailDisabled = _teamSiteParameters.WelcomeEmailDisabled,
                    SubscribeNewGroupMembers = _teamSiteParameters.SubscribeNewGroupMembers,
                    AllowOnlyMembersToPost = _teamSiteParameters.AllowOnlyMembersToPost,
                    CalendarMemberReadOnly = _teamSiteParameters.CalendarMemberReadOnly,
                    ConnectorsDisabled = _teamSiteParameters.ConnectorsDisabled,
                    HideGroupInOutlook = _teamSiteParameters.HideGroupInOutlook,
                    SubscribeMembersToCalendarEventsDisabled = _teamSiteParameters.SubscribeMembersToCalendarEventsDisabled,
                    SensitivityLabelId = GetSensitivityLabelGuid(_teamSiteParameters.SensitivityLabel),
                    SiteDesignId = _teamSiteParameters.SiteDesignId
                };

                if (ParameterSpecified(nameof(HubSiteId)))
                {
                    teamSiteOptions.HubSiteId = HubSiteId;
                }

                if (ParameterSpecified("SiteAlias"))
                {
                    teamSiteOptions.SiteAlias = _teamSiteParameters.SiteAlias;
                }

                if (ParameterSpecified("PreferredDataLocation") && _teamSiteParameters.PreferredDataLocation.HasValue)
                {
                    teamSiteOptions.PreferredDataLocation = GetGeoLocation(_teamSiteParameters.PreferredDataLocation.Value);
                }

                SiteCreationOptions siteCreationOptions = new()
                {
                    WaitForAsyncProvisioning = Wait
                };

                var tenantUrl = Connection.TenantAdminUrl ?? UrlUtilities.GetTenantAdministrationUrl(ClientContext.Url);
                VanityUrlOptions vanityUrlOptions = new()
                {
                    AdminCenterUri = new Uri(tenantUrl)
                };

                if (ClientContext.GetContextSettings()?.Type != Framework.Utilities.Context.ClientContextType.SharePointACSAppOnly)
                {
                    var rc = Connection.PnPContext.GetSiteCollectionManager().CreateSiteCollection(teamSiteOptions, siteCreationOptions, vanityUrlOptions);

                    if (ParameterSpecified(nameof(TimeZone)))
                    {
                        using (var newSiteContext = ClientContext.Clone(rc.Uri.AbsoluteUri))
                        {
                            newSiteContext.Web.EnsureProperties(w => w.RegionalSettings, w => w.RegionalSettings.TimeZones);
                            newSiteContext.Web.RegionalSettings.TimeZone = newSiteContext.Web.RegionalSettings.TimeZones.Where(t => t.Id == ((int)TimeZone)).First();
                            newSiteContext.Web.RegionalSettings.Update();
                            newSiteContext.ExecuteQueryRetry();
                            newSiteContext.Site.EnsureProperty(s => s.Url);
                            WriteObject(rc.Uri.AbsoluteUri);
                        }
                    }
                    else
                    {
                        WriteObject(rc.Uri.AbsoluteUri);
                    }
                }
                else
                {
                    LogError(new PSInvalidOperationException("Creating a new teamsite requires an underlying Microsoft 365 group. In order to create this we need to acquire an access token for the Microsoft Graph. This is not possible using ACS App Only connections."));
                }
            }
            else
            {
                EnsureDynamicParameters(_teamSiteWithoutMicrosoft365GroupParameters);
                if (!ParameterSpecified("Lcid"))
                {
                    ClientContext.Web.EnsureProperty(w => w.Language);
                    _teamSiteWithoutMicrosoft365GroupParameters.Lcid = ClientContext.Web.Language;
                }
                var creationInformation = new Framework.Sites.TeamNoGroupSiteCollectionCreationInformation();
                creationInformation.Title = _teamSiteWithoutMicrosoft365GroupParameters.Title;
                creationInformation.Url = _teamSiteWithoutMicrosoft365GroupParameters.Url;
                creationInformation.Description = _teamSiteWithoutMicrosoft365GroupParameters.Description;
                creationInformation.Classification = _teamSiteWithoutMicrosoft365GroupParameters.Classification;
                creationInformation.ShareByEmailEnabled = _teamSiteWithoutMicrosoft365GroupParameters.ShareByEmailEnabled;
                creationInformation.Lcid = _teamSiteWithoutMicrosoft365GroupParameters.Lcid;
                if (ParameterSpecified(nameof(HubSiteId)))
                {
                    creationInformation.HubSiteId = HubSiteId;
                }
                creationInformation.SiteDesignId = _teamSiteWithoutMicrosoft365GroupParameters.SiteDesignId;
                creationInformation.Owner = _teamSiteWithoutMicrosoft365GroupParameters.Owner;
                creationInformation.PreferredDataLocation = _teamSiteWithoutMicrosoft365GroupParameters.PreferredDataLocation;
                creationInformation.SensitivityLabel = _teamSiteWithoutMicrosoft365GroupParameters.SensitivityLabel;

                var returnedContext = Framework.Sites.SiteCollection.Create(ClientContext, creationInformation, noWait: !Wait);
                if (ParameterSpecified(nameof(TimeZone)))
                {
                    returnedContext.Web.EnsureProperties(w => w.RegionalSettings, w => w.RegionalSettings.TimeZones);
                    returnedContext.Web.RegionalSettings.TimeZone = returnedContext.Web.RegionalSettings.TimeZones.Where(t => t.Id == ((int)TimeZone)).First();
                    returnedContext.Web.RegionalSettings.Update();
                    returnedContext.ExecuteQueryRetry();
                    returnedContext.Site.EnsureProperty(s => s.Url);
                    WriteObject(returnedContext.Site.Url);
                }
                else
                {
                    WriteObject(returnedContext.Url);
                }
            }
        }

        private void EnsureDynamicParameters(object dynamicParameters)
        {
            if (dynamicParameters == null)
            {
                throw new PSArgumentException($"Please specify the parameter -{nameof(Type)} when invoking this cmdlet", nameof(Type));
            }
        }

        public class CommunicationSiteParameters
        {
            [Parameter(Mandatory = true, ParameterSetName = ParameterSet_COMMUNICATIONBUILTINDESIGN)]
            [Parameter(Mandatory = true, ParameterSetName = ParameterSet_COMMUNICATIONCUSTOMDESIGN)]
            public string Title;

            [Parameter(Mandatory = true, ParameterSetName = ParameterSet_COMMUNICATIONBUILTINDESIGN)]
            [Parameter(Mandatory = true, ParameterSetName = ParameterSet_COMMUNICATIONCUSTOMDESIGN)]
            public string Url;

            [Parameter(Mandatory = false, ParameterSetName = ParameterSet_COMMUNICATIONBUILTINDESIGN)]
            [Parameter(Mandatory = false, ParameterSetName = ParameterSet_COMMUNICATIONCUSTOMDESIGN)]
            public string Description;

            [Parameter(Mandatory = false, ParameterSetName = ParameterSet_COMMUNICATIONBUILTINDESIGN)]
            [Parameter(Mandatory = false, ParameterSetName = ParameterSet_COMMUNICATIONCUSTOMDESIGN)]
            public string Classification;

            [Parameter(Mandatory = false, ParameterSetName = ParameterSet_COMMUNICATIONBUILTINDESIGN)]
            [Parameter(Mandatory = false, ParameterSetName = ParameterSet_COMMUNICATIONCUSTOMDESIGN)]
            public SwitchParameter ShareByEmailEnabled;

            [Parameter(Mandatory = false, ParameterSetName = ParameterSet_COMMUNICATIONBUILTINDESIGN)]
            public PnP.Framework.Sites.CommunicationSiteDesign SiteDesign = PnP.Framework.Sites.CommunicationSiteDesign.Topic;

            [Parameter(Mandatory = true, ParameterSetName = ParameterSet_COMMUNICATIONCUSTOMDESIGN)]
            public Guid SiteDesignId;

            [Parameter(Mandatory = false, ParameterSetName = ParameterSet_COMMUNICATIONBUILTINDESIGN)]
            [Parameter(Mandatory = false, ParameterSetName = ParameterSet_COMMUNICATIONCUSTOMDESIGN)]
            public uint Lcid;

            [Parameter(Mandatory = false, ParameterSetName = ParameterSet_COMMUNICATIONBUILTINDESIGN)]
            [Parameter(Mandatory = false, ParameterSetName = ParameterSet_COMMUNICATIONCUSTOMDESIGN)]
            public string Owner;

            [Parameter(Mandatory = false, ParameterSetName = ParameterSet_COMMUNICATIONBUILTINDESIGN)]
            [Parameter(Mandatory = false, ParameterSetName = ParameterSet_COMMUNICATIONCUSTOMDESIGN)]
            public PnP.Framework.Enums.Office365Geography? PreferredDataLocation;

            [Parameter(Mandatory = false, ParameterSetName = ParameterSet_COMMUNICATIONBUILTINDESIGN)]
            [Parameter(Mandatory = false, ParameterSetName = ParameterSet_COMMUNICATIONCUSTOMDESIGN)]
            public string SensitivityLabel;
        }

        public class TeamSiteParameters
        {
            [Parameter(Mandatory = true, ParameterSetName = ParameterSet_TEAM)]
            public string Title;

            [Parameter(Mandatory = true, ParameterSetName = ParameterSet_TEAM)]
            public string Alias;

            [Parameter(Mandatory = false, ParameterSetName = ParameterSet_TEAM)]
            public string Description;

            [Parameter(Mandatory = false, ParameterSetName = ParameterSet_TEAM)]
            public string Classification;

            [Parameter(Mandatory = false, ParameterSetName = ParameterSet_TEAM)]
            public SwitchParameter IsPublic;

            [Parameter(Mandatory = false, ParameterSetName = ParameterSet_TEAM)]
            public uint Lcid;

            [Parameter(Mandatory = false, ParameterSetName = ParameterSet_TEAM)]
            public string[] Owners;

            [Parameter(Mandatory = false, ParameterSetName = ParameterSet_TEAM)]
            public PnP.Framework.Enums.Office365Geography? PreferredDataLocation;

            [Parameter(Mandatory = false, ParameterSetName = ParameterSet_TEAM)]
            public string SensitivityLabel;

            [Parameter(Mandatory = false, ParameterSetName = ParameterSet_TEAM)]
            public string SiteAlias;

            [Parameter(Mandatory = false, ParameterSetName = ParameterSet_TEAM)]
            public string[] Members;

            [Parameter(Mandatory = false, ParameterSetName = ParameterSet_TEAM)]
            public SwitchParameter WelcomeEmailDisabled;

            [Parameter(Mandatory = false, ParameterSetName = ParameterSet_TEAM)]
            public SwitchParameter SubscribeNewGroupMembers;

            [Parameter(Mandatory = false, ParameterSetName = ParameterSet_TEAM)]
            public SwitchParameter AllowOnlyMembersToPost;

            [Parameter(Mandatory = false, ParameterSetName = ParameterSet_TEAM)]
            public SwitchParameter CalendarMemberReadOnly;

            [Parameter(Mandatory = false, ParameterSetName = ParameterSet_TEAM)]
            public SwitchParameter ConnectorsDisabled;

            [Parameter(Mandatory = false, ParameterSetName = ParameterSet_TEAM)]
            public SwitchParameter HideGroupInOutlook;

            [Parameter(Mandatory = false, ParameterSetName = ParameterSet_TEAM)]
            public SwitchParameter SubscribeMembersToCalendarEventsDisabled;

            [Parameter(Mandatory = false, ParameterSetName = ParameterSet_TEAM)]
            public Guid SiteDesignId;
        }

        public class TeamSiteWithoutMicrosoft365Group
        {
            [Parameter(Mandatory = true, ParameterSetName = ParameterSet_TEAMSITENOGROUP)]
            public string Title;

            [Parameter(Mandatory = true, ParameterSetName = ParameterSet_TEAMSITENOGROUP)]
            public string Url;

            [Parameter(Mandatory = false, ParameterSetName = ParameterSet_TEAMSITENOGROUP)]
            public string Description;

            [Parameter(Mandatory = false, ParameterSetName = ParameterSet_TEAMSITENOGROUP)]
            public string Classification;

            [Parameter(Mandatory = false, ParameterSetName = ParameterSet_TEAMSITENOGROUP)]
            public SwitchParameter ShareByEmailEnabled;

            [Parameter(Mandatory = false, ParameterSetName = ParameterSet_TEAMSITENOGROUP)]
            public Guid SiteDesignId;

            [Parameter(Mandatory = false, ParameterSetName = ParameterSet_TEAMSITENOGROUP)]
            public uint Lcid;

            [Parameter(Mandatory = false, ParameterSetName = ParameterSet_TEAMSITENOGROUP)]
            public string Owner;

            [Parameter(Mandatory = false, ParameterSetName = ParameterSet_TEAMSITENOGROUP)]
            public PnP.Framework.Enums.Office365Geography? PreferredDataLocation;

            [Parameter(Mandatory = false, ParameterSetName = ParameterSet_TEAMSITENOGROUP)]
            public string SensitivityLabel;
        }

        private GeoLocation GetGeoLocation(PnP.Framework.Enums.Office365Geography office365Geography)
        {
            switch (office365Geography)
            {
                case Framework.Enums.Office365Geography.APC: return GeoLocation.APC;
                case Framework.Enums.Office365Geography.ARE: return GeoLocation.ARE;
                case Framework.Enums.Office365Geography.AUS: return GeoLocation.AUS;
                case Framework.Enums.Office365Geography.BRA: return GeoLocation.BRA;
                case Framework.Enums.Office365Geography.CAN: return GeoLocation.CAN;
                case Framework.Enums.Office365Geography.CHE: return GeoLocation.CHE;
                case Framework.Enums.Office365Geography.DEU: return GeoLocation.DEU;
                case Framework.Enums.Office365Geography.EUR: return GeoLocation.EUR;
                case Framework.Enums.Office365Geography.FRA: return GeoLocation.FRA;
                case Framework.Enums.Office365Geography.GBR: return GeoLocation.GBR;
                case Framework.Enums.Office365Geography.IND: return GeoLocation.IND;
                case Framework.Enums.Office365Geography.JPN: return GeoLocation.JPN;
                case Framework.Enums.Office365Geography.KOR: return GeoLocation.KOR;
                case Framework.Enums.Office365Geography.NAM: return GeoLocation.NAM;
                case Framework.Enums.Office365Geography.NOR: return GeoLocation.NOR;
                case Framework.Enums.Office365Geography.QAT: return GeoLocation.QAT;
                case Framework.Enums.Office365Geography.SWE: return GeoLocation.SWE;
                case Framework.Enums.Office365Geography.ZAF: return GeoLocation.ZAF;
            }
            return default;
        }

        private Guid GetSensitivityLabelGuid(string sensitivityLabel)
        {
            if (string.IsNullOrEmpty(sensitivityLabel))
                return Guid.Empty;

            var sensitivityLabelsPayload = Utilities.REST.RestHelper.Get(Connection.HttpClient, $"{ClientContext.Url.TrimEnd('/')}/_api/groupsitemanager/GetGroupCreationContext", AccessToken);
            var jsonDoc = JsonDocument.Parse(sensitivityLabelsPayload);

            var root = jsonDoc.RootElement;
            string sensitivityLabelStringId = "";
            if (root.TryGetProperty("DataClassificationOptionsNew", out var dataClassificationOptions))
            {
                JsonElement? val = dataClassificationOptions.EnumerateArray()
                    .FirstOrDefault(jt => jt.GetProperty("Value").GetString() == sensitivityLabel);

                sensitivityLabelStringId = val?.GetProperty("Key").GetString();
            }
            return Guid.Parse(sensitivityLabelStringId);
        }
    }
}