using PnP.PowerShell.Commands.Utilities;
using System.Management.Automation;
using CoreHierarchy = PnP.Core.Provisioning.Model.ProvisioningHierarchy;
using FrameworkHierarchy = PnP.Framework.Provisioning.Model.ProvisioningHierarchy;

namespace PnP.PowerShell.Commands.Base.PipeBinds
{
    /// <summary>
    /// Binds an in memory tenant template, produced by either the PnP Framework or the PnP.Core.Provisioning
    /// engine, so that the same parameter accepts both.
    /// </summary>
    public sealed class TenantTemplateInstancePipeBind
    {
        private readonly FrameworkHierarchy frameworkHierarchy;
        private readonly CoreHierarchy coreHierarchy;

        public TenantTemplateInstancePipeBind(FrameworkHierarchy hierarchy)
        {
            frameworkHierarchy = hierarchy;
        }

        public TenantTemplateInstancePipeBind(CoreHierarchy hierarchy)
        {
            coreHierarchy = hierarchy;
        }

        internal bool IsEmpty => frameworkHierarchy == null && coreHierarchy == null;

        internal FrameworkHierarchy GetFrameworkHierarchy()
        {
            if (frameworkHierarchy != null)
            {
                return frameworkHierarchy;
            }
            if (coreHierarchy == null)
            {
                return null;
            }
            throw new PSArgumentException("The tenant template passed in was produced by the experimental PnP.Core.Provisioning engine. Add -Experimental to apply it with that engine.");
        }

        internal CoreHierarchy GetCoreHierarchy()
        {
            if (coreHierarchy != null)
            {
                return coreHierarchy;
            }
            return frameworkHierarchy != null ? CoreProvisioningHelper.ToCoreHierarchy(frameworkHierarchy) : null;
        }
    }
}
