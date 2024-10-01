
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model.Teams;
using PnP.PowerShell.Commands.Utilities;
using System.Management.Automation;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Model.Graph;
using System.Text.RegularExpressions;

namespace PnP.PowerShell.Commands.Teams
{
    [Cmdlet(VerbsCommon.Add, "PnPTeamsTab")]
    [RequiredApiApplicationPermissions("https://graph.microsoft.com/Group.ReadWrite.All")]
    public class AddTeamsTab : PnPGraphCmdlet, IDynamicParameters
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public TeamsTeamPipeBind Team;

        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public TeamsChannelPipeBind Channel;

        [Parameter(Mandatory = true)]
        public string DisplayName;

        [Parameter(Mandatory = true)]
        public TeamTabType Type;

        private OfficeFileParameters officeFileParameters;
        private DocumentLibraryParameters documentLibraryParameters;
        private SharePointPageAndListParameters sharePointPageAndListParameters;
        private CustomParameters customParameters;

        public object GetDynamicParameters()
        {
            switch (Type)
            {
                case TeamTabType.Word:
                case TeamTabType.Excel:
                case TeamTabType.PowerPoint:
                case TeamTabType.PDF:
                    {
                        officeFileParameters = new OfficeFileParameters();
                        return officeFileParameters;
                    }
                case TeamTabType.DocumentLibrary:
                case TeamTabType.Planner:
                case TeamTabType.WebSite:
                    {
                        documentLibraryParameters = new DocumentLibraryParameters();
                        return documentLibraryParameters;
                    }
                case TeamTabType.SharePointPageAndList:
                    {
                        sharePointPageAndListParameters = new SharePointPageAndListParameters();
                        return sharePointPageAndListParameters;
                    }
                case TeamTabType.Custom:
                    {
                        customParameters = new CustomParameters();
                        return customParameters;
                    }
            }
            return null;
        }

        protected override void ExecuteCmdlet()
        {
            var groupId = Team.GetGroupId(this, Connection, AccessToken);
            if (groupId != null)
            {
                var channelId = Channel.GetId(this, Connection, AccessToken, groupId);
                if (channelId != null)
                {
                    try
                    {
                        string entityId = null;
                        string contentUrl = null;
                        string removeUrl = null;
                        string webSiteUrl = null;
                        string teamsAppId = null;
                        switch (Type)
                        {
                            case TeamTabType.Word:
                            case TeamTabType.Excel:
                            case TeamTabType.PowerPoint:
                            case TeamTabType.PDF:
                                {
                                    EnsureDynamicParameters(officeFileParameters);
                                    entityId = officeFileParameters.EntityId;
                                    contentUrl = officeFileParameters.ContentUrl;
                                    break;
                                }
                            case TeamTabType.DocumentLibrary:
                            case TeamTabType.Planner:
                            case TeamTabType.WebSite:
                                {
                                    EnsureDynamicParameters(documentLibraryParameters);
                                    contentUrl = documentLibraryParameters.ContentUrl;
                                    break;
                                }
                            case TeamTabType.SharePointPageAndList:
                                {
                                    EnsureDynamicParameters(sharePointPageAndListParameters);
                                    // Using a Regular Expression we'll define the URL to use within Teams allowing for automatic logon to the SharePoint Online component. Result will be a syntax similar to:
                                    // https://contoso.sharepoint.com/sites/Marketing/_layouts/15/teamslogon.aspx?spfx=true&dest=https%3A%2F%2Fcontoso.sharepoint.com%2Fsites%2FMarketing%2FSitePages%2FHome.aspx
                                    contentUrl = string.Concat(Regex.Replace(sharePointPageAndListParameters.WebsiteUrl, @"^(.*?://.*?/(?:(?:sites|teams)/.*?/)?)(.*)", "$1", RegexOptions.IgnoreCase), "_layouts/15/teamslogon.aspx?spfx=true&dest=", UrlUtilities.UrlEncode(sharePointPageAndListParameters.WebsiteUrl));
                                    webSiteUrl = sharePointPageAndListParameters.WebsiteUrl;
                                    break;
                                }
                            case TeamTabType.Custom:
                                {
                                    EnsureDynamicParameters(customParameters);
                                    entityId = customParameters.EntityId;
                                    contentUrl = customParameters.ContentUrl;
                                    removeUrl = customParameters.RemoveUrl;
                                    webSiteUrl = customParameters.WebsiteUrl;
                                    teamsAppId = customParameters.TeamsAppId;
                                    break;
                                }
                        }
                        WriteObject(TeamsUtility.AddTab(this, Connection, AccessToken, groupId, channelId, DisplayName, Type, teamsAppId, entityId, contentUrl, removeUrl, webSiteUrl));
                    }
                    catch (GraphException ex)
                    {
                        if (ex.Error != null)
                        {
                            throw new PSInvalidOperationException(ex.Error.Message);
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
                else
                {
                    throw new PSArgumentException("Channel not found");
                }
            }
            else
            {
                throw new PSArgumentException("Group not found");
            }
        }

        private void EnsureDynamicParameters(object dynamicParameters)
        {
            if (dynamicParameters == null)
            {
                throw new PSArgumentException($"Please specify the parameter -{nameof(Type)} when invoking this cmdlet", nameof(Type));
            }
        }

        public class OfficeFileParameters
        {
            [Parameter(Mandatory = true)]
            public string EntityId;

            [Parameter(Mandatory = true)]
            public string ContentUrl;
        }

        public class DocumentLibraryParameters
        {
            [Parameter(Mandatory = true)]
            public string ContentUrl;
        }

        public class CustomParameters
        {
            [Parameter(Mandatory = true)]
            public string TeamsAppId;

            [Parameter(Mandatory = false)]
            public string EntityId;

            [Parameter(Mandatory = false)]
            public string ContentUrl;

            [Parameter(Mandatory = false)]
            public string RemoveUrl;

            [Parameter(Mandatory = false)]
            public string WebsiteUrl;
        }

        public class SharePointPageAndListParameters
        {
            [Parameter(Mandatory = true)]
            public string WebsiteUrl;
        }
    }
}