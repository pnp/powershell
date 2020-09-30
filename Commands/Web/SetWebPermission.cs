using System;
using System.Linq;
using System.Management.Automation;
using Microsoft.SharePoint.Client;

using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Extensions;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Set, "PnPWebPermission", DefaultParameterSetName = "User")]
    public class SetWebPermission : PnPWebCmdlet
    {
		[Parameter(Mandatory = true, ValueFromPipeline = true)]
		[Parameter(Mandatory = true, ValueFromPipeline = true)]
		public WebPipeBind Identity;

		[Parameter(Mandatory = true, ParameterSetName = "GroupByWebUrl")]
		[Parameter(Mandatory = true, ParameterSetName = "UserByWebUrl")]
		public string Url;

		[Parameter(Mandatory = true, ParameterSetName = "Group")]
		[Parameter(Mandatory = true, ParameterSetName = "GroupByWebIdentity")]
		[Parameter(Mandatory = true, ParameterSetName = "GroupByWebUrl")]
		public GroupPipeBind Group;

        [Parameter(Mandatory = true, ParameterSetName = "User")]
        [Parameter(Mandatory = true, ParameterSetName = "UserByWebIdentity")]
        [Parameter(Mandatory = true, ParameterSetName = "UserByWebUrl")]
        public string User;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
		public string[] AddRole = null;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public string[] RemoveRole = null;

        protected override void ExecuteCmdlet()
		{
			// Get Web
			Web web = SelectedWeb;
			if (ParameterSetName == "GroupByWebIdentity" || ParameterSetName == "UserByWebIdentity")
			{
				if (Identity.Id != Guid.Empty)
				{
					web = ClientContext.Web.GetWebById(Identity.Id);
				}
				else if (Identity.Web != null)
				{
					web = Identity.Web;
				}
				else if (Identity.Url != null)
				{
					web = ClientContext.Web.GetWebByUrl(Identity.Url);
				}
			}
			else if (ParameterSetName == "GroupByWebUrl" || ParameterSetName == "UserByWebUrl")
			{
				web = SelectedWeb.GetWeb(Url);
			}

			// Set permissions
			Principal principal = null;
			if (ParameterSetName == "Group" || ParameterSetName == "GroupByWebUrl" || ParameterSetName == "GroupByWebIdentity")
			{
				if (Group.Id != -1)
				{
					principal = web.SiteGroups.GetById(Group.Id);
				}
				else if (!string.IsNullOrEmpty(Group.Name))
				{
					principal = web.SiteGroups.GetByName(Group.Name);
				}
				else if (Group.Group != null)
				{
					principal = Group.Group;
				}
			}
			else
			{
				principal = web.EnsureUser(User);
			}
			if (principal != null)
			{
				if (AddRole != null)
				{
					foreach (var role in AddRole)
					{
						web.AddPermissionLevelToPrincipal(principal, role);
					}
				}
				if (RemoveRole != null)
				{
					foreach (var role in RemoveRole)
					{
						web.RemovePermissionLevelFromPrincipal(principal, role);
					}
				}
			}
			else
			{
				WriteError(new ErrorRecord(new Exception("Principal not found"), "1", ErrorCategory.ObjectNotFound, null));
			}
		}
	}
}
