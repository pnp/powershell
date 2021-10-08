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
    public class AddTeamsTeam : PnPGraphCmdlet
    {

        protected override void ExecuteCmdlet()
        {
            try
            {
                var results = SiteCollection.TeamifySiteAsync(ClientContext);
                var returnedBool = results.GetAwaiter().GetResult();
                WriteObject(returnedBool);
            }
            catch(Exception)
            {
                try
                {
                    var groupId = ClientContext.Site.EnsureProperty(s => s.GroupId);                    
                    Microsoft365GroupsUtility.CreateTeamAsync(HttpClient, AccessToken, groupId).GetAwaiter().GetResult();                    
                }
                catch
                {
                    throw;
                }                
            }
            
        }
    }
}