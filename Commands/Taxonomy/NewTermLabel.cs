using System;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Taxonomy;
using PnP.PowerShell.CmdletHelpAttributes;
using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.Taxonomy
{
    [Cmdlet(VerbsCommon.New, "PnPTermLabel", SupportsShouldProcess = false)]
    public class NewTermLabel : PnPSharePointCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        public TaxonomyItemPipeBind<Term> Term;

        [Parameter(Mandatory = true)]
        public string Name;

        [Parameter(Mandatory = true)]
        public int Lcid;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public SwitchParameter IsDefault = true;

        protected override void ExecuteCmdlet()
        {
            if (Term.Item == null)
            {
                throw new ArgumentException("You must pass in a Term instance to this command", nameof(Term));
            }

            var label = Term.Item.CreateLabel(Name, Lcid, IsDefault.IsPresent ? IsDefault.ToBool() : true);
            ClientContext.ExecuteQueryRetry();
            WriteObject(label);
        }
    }
}
