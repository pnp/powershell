using System;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Set, "PnPWebPermission", DefaultParameterSetName = "User")]
    [OutputType(typeof(void))]
    public class SetWebPermission : PnPWebCmdlet
    {
        private const string ParameterSet_GROUP = "Set group permissions";
        private const string ParameterSet_USER = "Set user permissions";

        [Parameter(Mandatory = false, ValueFromPipeline = true)]
        public WebPipeBind Identity;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_GROUP)]
        public GroupPipeBind Group;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_USER)]
        public string User;

        [Parameter(Mandatory = false)]
        [ValidateNotNullOrEmpty]
        public string[] AddRole = null;

        [Parameter(Mandatory = false)]
        [ValidateNotNullOrEmpty]
        public string[] RemoveRole = null;

        protected override void ExecuteCmdlet()
        {
            if (!ParameterSpecified(nameof(AddRole)) && !ParameterSpecified(nameof(RemoveRole)))
            {
                throw new PSInvalidOperationException("AddRole or RemoveRole is required");
            }

            Web web = CurrentWeb;
            if (ParameterSpecified(nameof(Identity)))
            {
                web = Identity.GetWeb(ClientContext);
            }

            Principal principal = null;
            if (ParameterSetName == ParameterSet_GROUP)
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
