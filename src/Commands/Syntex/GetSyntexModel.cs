using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model.Syntex;
using System.Collections.Generic;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Syntex
{
    [Cmdlet(VerbsCommon.Get, "PnPSyntexModel")]
    [OutputType(typeof(SyntexModel))]
    public class GetSyntexModel : PnPWebCmdlet
    {

        [Parameter(Mandatory = false, ValueFromPipeline = true, Position = 0)]
        public SyntexModelPipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            var ctx = Connection.PnPContext;

            if (ctx.Web.IsSyntexContentCenter())
            {
                if (ParameterSpecified(nameof(Identity)) && Identity != null)
                {
                    WriteObject(Identity.GetSyntexModel(Connection));
                }
                else
                {
                    var syntexContentCenter = ctx.Web.AsSyntexContentCenter();
                    var models = syntexContentCenter.GetSyntexModels();

                    List<SyntexModel> modelsToOutput = new List<SyntexModel>();
                    foreach(var model in models)
                    {
                        modelsToOutput.Add(new SyntexModel()
                        {
                            Id = model.Id,
                            Description = model.Description,
                            ModelLastTrained = model.ModelLastTrained,
                            Name = model.Name,
                            UniqueId = model.UniqueId
                        }); 
                    }

                    WriteObject(modelsToOutput, true);
                }
            }
            else
            {
                LogWarning("The connected site is not a Syntex Content Center site");
            }
        }
    }
}
