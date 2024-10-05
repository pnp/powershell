using System.Management.Automation;
using Microsoft.SharePoint.Client;
using System;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Enums;
using System.Collections.Generic;
using System.Linq;
using PnP.Core.Model.SharePoint;

namespace PnP.PowerShell.Commands.Branding
{
    [Cmdlet(VerbsCommon.Set, "PnPApplicationCustomizer")]
    public class SetApplicationCustomizer : PnPWebCmdlet
    {
        private const string ParameterSet_CUSTOMACTIONID = "Custom Action Id";
        private const string ParameterSet_CLIENTSIDECOMPONENTID = "Client Side Component Id";

        [Parameter(Mandatory = false, Position = 0, ValueFromPipeline = true, ParameterSetName = ParameterSet_CUSTOMACTIONID)]
        public UserCustomActionPipeBind Identity = null;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_CLIENTSIDECOMPONENTID)]
        public Guid ClientSideComponentId;

        [Parameter(Mandatory = false)]
        public CustomActionScope Scope = CustomActionScope.Web;

        [Parameter(Mandatory = false)]
        public string Title = null;

        [Parameter(Mandatory = false)]
        public string Description = null;

        [Parameter(Mandatory = false)]
        public int? Sequence = null;

        [Parameter(Mandatory = false)]
        public string ClientSideComponentProperties = null;

        [Parameter(Mandatory = false)]
        public string ClientSideHostProperties;
        protected override void ExecuteCmdlet()
        {
            IEnumerable<IUserCustomAction> actions = null;
            var pnpContext = Connection.PnPContext;
            if (Identity != null)
            {
                actions = Identity.GetCustomActions(pnpContext, Scope);
            }

            else
            {
                var customActions = new List<IUserCustomAction>();

                if (Scope == CustomActionScope.Web || Scope == CustomActionScope.All)
                {
                    customActions.AddRange(pnpContext.Web.UserCustomActions.ToList());
                }
                if (Scope == CustomActionScope.Site || Scope == CustomActionScope.All)
                {
                    customActions.AddRange(pnpContext.Site.UserCustomActions.ToList());
                }

                actions = customActions.AsEnumerable();
            }

            // If a ClientSideComponentId has been provided, only leave those who have a matching client side component id
            if (ParameterSetName == ParameterSet_CLIENTSIDECOMPONENTID)
            {
                actions = actions.Where(a => a.ClientSideComponentId == ClientSideComponentId && a.Location == "ClientSideExtension.ApplicationCustomizer").ToList();
            }

            if (!actions.Any())
            {
                WriteVerbose($"No Application Customizers representing the client side extension registration found within the scope '{Scope}'");
                return;
            }

            // Update each of the matched custom actions
            foreach (var action in actions)
            {
                if (ParameterSpecified(nameof(Title)))
                {
                    action.Title = Title;
                }
                if (ParameterSpecified(nameof(Description)))
                {
                    action.Description = Description;
                }
                if (ParameterSpecified(nameof(ClientSideComponentId)))
                {
                    action.ClientSideComponentId = ClientSideComponentId;
                }
                if (ParameterSpecified(nameof(ClientSideComponentProperties)))
                {
                    action.ClientSideComponentProperties = ClientSideComponentProperties;
                }
                if (ParameterSpecified(nameof(ClientSideHostProperties)))
                {
                    action.HostProperties = ClientSideHostProperties;
                }
                if (ParameterSpecified(nameof(Sequence)))
                {
                    action.Sequence = Sequence.Value;
                }
                action.Update();
            }
        }
    }
}