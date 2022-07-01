using System;
using System.Management.Automation;
using Microsoft.SharePoint.Client;

using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.Fields
{
    [Cmdlet(VerbsCommon.Remove, "PnPField")]
    [OutputType(typeof(void))]
    public class RemoveField : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        public FieldPipeBind Identity = new FieldPipeBind();

        [Parameter(Mandatory = false, ValueFromPipeline = true, Position = 1)]
        public ListPipeBind List;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            if (List != null)
            {
                var list = List.GetList(CurrentWeb);

                var f = Identity.Field;
                if (list != null)
                {
                    if (f == null)
                    {
                        if (Identity.Id != Guid.Empty)
                        {
                            f = list.Fields.GetById(Identity.Id);
                        }
                        else if (!string.IsNullOrEmpty(Identity.Name))
                        {
                            f = list.Fields.GetByInternalNameOrTitle(Identity.Name);
                        }
                    }
                    ClientContext.Load(f);
                    ClientContext.ExecuteQueryRetry();
                    if (f != null && f.IsPropertyAvailable("InternalName"))
                    {
                        if (Force || ShouldContinue(string.Format(Properties.Resources.DeleteField0, f.InternalName), Properties.Resources.Confirm))
                        {
                            f.DeleteObject();
                            ClientContext.ExecuteQueryRetry();
                        }
                    }
                }
            } 
            else
            {
                var f = Identity.Field;

                if (f == null)
                {
                    if (Identity.Id != Guid.Empty)
                    {
                        f = CurrentWeb.Fields.GetById(Identity.Id);
                    }
                    else if (!string.IsNullOrEmpty(Identity.Name))
                    {
                        f = CurrentWeb.Fields.GetByInternalNameOrTitle(Identity.Name);
                    }
                }
                ClientContext.Load(f);
                ClientContext.ExecuteQueryRetry();

                if (f != null && f.IsPropertyAvailable("InternalName"))
                {
                    if (Force || ShouldContinue(string.Format(Properties.Resources.DeleteField0, f.InternalName), Properties.Resources.Confirm))
                    {
                        f.DeleteObject();
                        ClientContext.ExecuteQueryRetry();
                    }
                }
            }
        }
    }

}
