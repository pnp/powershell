using System;
using System.Linq;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Enums;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.New, "PnPSite")]
    [RequiredMinimalApiPermissions("Group.ReadWrite.All")]
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
                creationInformation.DisplayName = _teamSiteParameters.Title;
                creationInformation.Alias = _teamSiteParameters.Alias;
                creationInformation.Classification = _teamSiteParameters.Classification;
                creationInformation.Description = _teamSiteParameters.Description;
                creationInformation.IsPublic = _teamSiteParameters.IsPublic;
                creationInformation.Lcid = _teamSiteParameters.Lcid;
                if (ParameterSpecified(nameof(HubSiteId)))
                {
                    creationInformation.HubSiteId = HubSiteId;
                }
                creationInformation.Owners = _teamSiteParameters.Owners;
                creationInformation.PreferredDataLocation = _teamSiteParameters.PreferredDataLocation;
                creationInformation.SensitivityLabel = _teamSiteParameters.SensitivityLabel;

                if (ParameterSpecified("SiteAlias"))
                {
                    creationInformation.SiteAlias = _teamSiteParameters.SiteAlias;
                }

                if (ClientContext.GetContextSettings()?.Type != Framework.Utilities.Context.ClientContextType.SharePointACSAppOnly)
                {
                    var returnedContext = Framework.Sites.SiteCollection.Create(ClientContext, creationInformation, noWait: !Wait, graphAccessToken: GraphAccessToken, azureEnvironment: Connection?.AzureEnvironment ?? Framework.AzureEnvironment.Production);
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
                else
                {
                    WriteError(new PSInvalidOperationException("Creating a new teamsite requires an underlying Microsoft 365 group. In order to create this we need to acquire an access token for the Microsoft Graph. This is not possible using ACS App Only connections."), ErrorCategory.SecurityError);
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
    }
}