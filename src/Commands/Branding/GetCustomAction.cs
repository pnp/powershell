using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using System;
using PnP.PowerShell.Commands.Enums;
using PnP.PowerShell.Commands.Base.Completers;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.Core.Model.SharePoint;
using PnP.Core.Model;
using PnP.Core.Auth.Services.Builder.Configuration;

namespace PnP.PowerShell.Commands.Branding
{
    [Cmdlet(VerbsCommon.Get, "PnPCustomAction")]
    public class GetCustomAction : PnPWebRetrievalsCmdlet<UserCustomAction>
    {
        [Parameter(Mandatory = false)]
        [ArgumentCompleter(typeof(CustomerActionCompleter))]
        public UserCustomActionPipeBind Identity;

        [Parameter(Mandatory = false)]
        public CustomActionScope Scope = CustomActionScope.Web;

        [Parameter(Mandatory = false)]
        public SwitchParameter ThrowExceptionIfCustomActionNotFound;

        protected override void ExecuteCmdlet()
        {
            if (Identity != null)
            {
                WriteObject(Identity.GetCustomActions(PnPContext, Scope).FirstOrDefault());
            }
            else
            {
                List<IUserCustomAction> actions = null;
                switch (Scope)
                {
                    case CustomActionScope.Web:
                        {
                            actions = PnPContext.Web.UserCustomActions.ToList();
                            break;
                        }
                    case CustomActionScope.Site:
                        {
                            actions = PnPContext.Site.UserCustomActions.ToList();
                            break;
                        }

                }
                WriteObject(actions, true);
            }
        }
    }
}
