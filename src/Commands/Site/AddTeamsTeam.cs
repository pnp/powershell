using Microsoft.SharePoint.Client;

using PnP.Framework.Sites;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Utilities;

using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Site
{
    [Cmdlet(VerbsCommon.Add, "PnPTeamsTeam")]
    [RequiredMinimalApiPermissions("Group.ReadWrite.All")]
    [OutputType(typeof(string))]
    public class AddTeamsTeam : PnPGraphCmdlet
    {
        protected override void ExecuteCmdlet()
        {
            try
            {
                var results = SiteCollection.TeamifySiteAsync(ClientContext);
                string returned = results.GetAwaiter().GetResult();
                WriteObject(returned);
            }
            catch (Exception)
            {
                try
                {
                    var groupId = ClientContext.Site.EnsureProperty(s => s.GroupId);
                    Microsoft365GroupsUtility.CreateTeamAsync(this, Connection, AccessToken, groupId).GetAwaiter().GetResult();
                }
                catch
                {
                    throw;
                }
            }
        }
    }
}