using Microsoft.SharePoint.Client;
using PnP.Core.Model.SharePoint;
using PnP.Core.QueryModel;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model;
using System;
using System.Linq;
using System.Management.Automation;

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

        [Parameter(Mandatory = false)]
        public PnPBatch Batch;

        protected override void ExecuteCmdlet()
        {
            if (ParameterSpecified(nameof(Batch)))
            {
                RemoveFieldBatch();
            }
            else
            {
                RemoveSingleField();
            }
        }

        private void RemoveFieldBatch()
        {
            if (List != null)
            {
                var list = List.GetList(Connection.PnPContext);
                list.EnsureProperties(l => l.Fields);
                var fieldCollection = list.Fields.AsRequested();
                var f = Identity.Field;
                IField pnpField = null;

                if (list != null)
                {
                    if (f == null)
                    {
                        if (Identity.Id != Guid.Empty)
                        {
                            pnpField = fieldCollection.Where(fi => fi.Id == Identity.Id).FirstOrDefault();
                        }
                        else if (!string.IsNullOrEmpty(Identity.Name))
                        {
                            pnpField = fieldCollection.Where(fi => fi.InternalName == Identity.Name).FirstOrDefault();
                            if (pnpField == null)
                            {
                                pnpField = fieldCollection.Where(fi => fi.Title == Identity.Name).FirstOrDefault();
                            }
                        }

                        if (pnpField != null)
                        {
                            if (Force || ShouldContinue(string.Format(Properties.Resources.DeleteField0, pnpField.InternalName), Properties.Resources.Confirm))
                            {
                                pnpField.DeleteBatch(Batch.Batch);
                            }
                        }
                    }
                }
            }
            else
            {
                var f = Identity.Field;
                var pnpWeb = Connection.PnPContext.Web;
                pnpWeb.EnsureProperties(w => w.Fields);
                var fieldCollection = pnpWeb.Fields.AsRequested();
                IField pnpField = null;
                if (f == null)
                {
                    if (Identity.Id != Guid.Empty)
                    {
                        pnpField = fieldCollection.Where(fi => fi.Id == Identity.Id).FirstOrDefault();
                    }
                    else if (!string.IsNullOrEmpty(Identity.Name))
                    {
                        pnpField = fieldCollection.Where(fi => fi.InternalName == Identity.Name).FirstOrDefault();

                        if (pnpField == null)
                        {
                            pnpField = fieldCollection.Where(fi => fi.Title == Identity.Name).FirstOrDefault();
                        }
                    }

                    if (pnpField != null)
                    {
                        if (Force || ShouldContinue(string.Format(Properties.Resources.DeleteField0, pnpField.InternalName), Properties.Resources.Confirm))
                        {
                            pnpField.DeleteBatch(Batch.Batch);
                        }
                    }
                }
            }
        }

        private void RemoveSingleField()
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
