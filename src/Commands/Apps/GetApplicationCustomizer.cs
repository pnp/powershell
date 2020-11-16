using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Enums;
using System.Collections.Generic;
using System.Linq;
using System;

namespace PnP.PowerShell.Commands.Branding
{
    [Cmdlet(VerbsCommon.Get, "ApplicationCustomizer")]
    public class GetApplicationCustomizer : PnPWebRetrievalsCmdlet<UserCustomAction>
    {
        private const string ParameterSet_CUSTOMACTIONID = "Custom Action Id";
        private const string ParameterSet_CLIENTSIDECOMPONENTID = "Client Side Component Id";

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_CUSTOMACTIONID)]
        public Guid Identity;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_CLIENTSIDECOMPONENTID)]
        public Guid ClientSideComponentId;

        [Parameter(Mandatory = false)]
        public CustomActionScope Scope = CustomActionScope.All;

        [Parameter(Mandatory = false)]
        public SwitchParameter ThrowExceptionIfCustomActionNotFound;

        protected override void ExecuteCmdlet()
        {
            List<UserCustomAction> actions = new List<UserCustomAction>();

            if (Scope == CustomActionScope.All || Scope == CustomActionScope.Web)
            {
                actions.AddRange(SelectedWeb.GetCustomActions(RetrievalExpressions));
            }
            if (Scope == CustomActionScope.All || Scope == CustomActionScope.Site)
            {
                actions.AddRange(ClientContext.Site.GetCustomActions(RetrievalExpressions));
            }

            if (Identity != Guid.Empty)
            {
                var foundAction = actions.FirstOrDefault(x => x.Id == Identity && x.Location == "ClientSideExtension.ApplicationCustomizer");
                if (foundAction != null || !ThrowExceptionIfCustomActionNotFound)
                {
                    WriteObject(foundAction, true);
                }
                else
                {
                    throw new PSArgumentException($"No SharePoint Framework client side extension application customizer found with the Identity '{Identity}' within the scope '{Scope}'", "Identity");
                }
            }
            else
            {
                switch (ParameterSetName)
                {
                    case ParameterSet_CLIENTSIDECOMPONENTID:
                        actions = actions.Where(x => x.Location == "ClientSideExtension.ApplicationCustomizer" & x.ClientSideComponentId == ClientSideComponentId).ToList();
                        break;

                    case ParameterSet_CUSTOMACTIONID:
                        actions = actions.Where(x => x.Location == "ClientSideExtension.ApplicationCustomizer").ToList();
                        break;
                }

                WriteObject(actions, true);
            }
        }
    }
}