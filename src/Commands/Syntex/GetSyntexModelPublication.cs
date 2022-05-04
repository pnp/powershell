﻿using PnP.Core.Model.SharePoint;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Collections.Generic;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Syntex
{
    [Cmdlet(VerbsCommon.Get, "PnPSyntexModelPublication")]
    [OutputType(typeof(Model.Syntex.SyntexModelPublication))]
    public class GetSyntexModelPublication : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        public SyntexModelPipeBind Model;

        protected override void ExecuteCmdlet()
        {
            var ctx = PnPConnection.Current.PnPContext;

            if (ctx.Web.IsSyntexContentCenter())
            {
                // Get the model we're publishing
                ISyntexModel model = Model.GetSyntexModel();

                if (model == null)
                {
                    throw new PSArgumentException("Provide a valid model to get publications for");
                }

                var modelPublications = model.GetModelPublications();

                List<Model.Syntex.SyntexModelPublication> modelPublicationsToOutput = new List<Model.Syntex.SyntexModelPublication>();

                foreach(var modelPublication in modelPublications)
                {
                    modelPublicationsToOutput.Add(new Model.Syntex.SyntexModelPublication()
                    {
                        ModelUniqueId = modelPublication.ModelUniqueId,
                        TargetLibraryServerRelativeUrl = modelPublication.TargetLibraryServerRelativeUrl,
                        TargetSiteUrl = modelPublication.TargetSiteUrl,
                        TargetWebServerRelativeUrl = modelPublication.TargetWebServerRelativeUrl,
                        ViewOption = modelPublication.ViewOption
                    });
                }
                WriteObject(modelPublicationsToOutput);
            }
            else
            {
                WriteWarning("The connected site is not a Syntex Content Center site");
            }
        }
    }
}
