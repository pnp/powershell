using System;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base.Completers;
using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.Lists
{
    [Cmdlet(VerbsCommon.Remove, "PnPView")]
    [OutputType(typeof(void))]
    public class RemoveView : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        public ViewPipeBind Identity = new ViewPipeBind();

        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 1)]
        [ArgumentCompleter(typeof(ListNameCompleter))]
        public ListPipeBind List;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            if (List != null)
            {
                var list = List.GetList(CurrentWeb);

                if (list != null)
                {
                    View view = null;
                    if (Identity != null)
                    {
                        if (Identity.Id != Guid.Empty)
                        {
                            view = list.GetViewById(Identity.Id);
                        }
                        else if (!string.IsNullOrEmpty(Identity.Title))
                        {
                            view = list.GetViewByName(Identity.Title);
                        }
                        else if (Identity.View != null)
                        {
                            view = Identity.View;
                        }
                        if (view != null)
                        {
                            if (Force || ShouldContinue(string.Format(Properties.Resources.RemoveView0, view.Title), Properties.Resources.Confirm))
                            {
                                view.DeleteObject();
                                ClientContext.ExecuteQueryRetry();
                            }
                        }
                    }
                }
            }
        }
    }

}
