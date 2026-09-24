using PnP.PowerShell.Commands.Utilities;
using System.Management.Automation;
using CoreTemplate = PnP.Core.Provisioning.Model.ProvisioningTemplate;
using FrameworkTemplate = PnP.Framework.Provisioning.Model.ProvisioningTemplate;

namespace PnP.PowerShell.Commands.Base.PipeBinds
{
    /// <summary>
    /// Binds an in memory site template, produced by either the PnP Framework or the PnP.Core.Provisioning
    /// engine, so that the same parameter accepts both.
    /// </summary>
    public sealed class SiteTemplateInstancePipeBind
    {
        private readonly FrameworkTemplate frameworkTemplate;
        private readonly CoreTemplate coreTemplate;

        public SiteTemplateInstancePipeBind(FrameworkTemplate template)
        {
            frameworkTemplate = template;
        }

        public SiteTemplateInstancePipeBind(CoreTemplate template)
        {
            coreTemplate = template;
        }

        internal bool IsEmpty => frameworkTemplate == null && coreTemplate == null;

        internal FrameworkTemplate GetFrameworkTemplate()
        {
            if (frameworkTemplate != null)
            {
                return frameworkTemplate;
            }
            if (coreTemplate == null)
            {
                return null;
            }
            throw new PSArgumentException("The template passed in was produced by the experimental PnP.Core.Provisioning engine. Add -Experimental to apply it with that engine.");
        }

        internal CoreTemplate GetCoreTemplate()
        {
            if (coreTemplate != null)
            {
                return coreTemplate;
            }
            return frameworkTemplate != null ? CoreProvisioningHelper.ToCoreTemplate(frameworkTemplate) : null;
        }
    }
}
