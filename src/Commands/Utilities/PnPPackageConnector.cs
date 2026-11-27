using System;
using System.IO;
using System.Management.Automation;
using CoreFileConnectorBase = PnP.Core.Provisioning.Connectors.FileConnectorBase;
using CoreOpenXMLConnector = PnP.Core.Provisioning.Connectors.OpenXMLConnector;
using FrameworkFileConnectorBase = PnP.Framework.Provisioning.Connectors.FileConnectorBase;
using FrameworkOpenXMLConnector = PnP.Framework.Provisioning.Connectors.OpenXMLConnector;

namespace PnP.PowerShell.Commands.Utilities
{
    /// <summary>
    /// Opens .pnp packages with either provisioning engine.
    /// </summary>
    internal static class PnPPackageConnector
    {
        private const string FrameworkPackageMessage = "This .pnp package was created by PnP Framework, and the experimental PnP.Core.Provisioning engine can't open it yet. Run the command without -Experimental to use this package.";
        private const string CorePackageMessage = "This .pnp package was created by the experimental PnP.Core.Provisioning engine, and PnP Framework can't open it. Add -Experimental to use this package.";

        /// <summary>
        /// Opens, or starts, a .pnp package with the PnP Framework engine.
        /// </summary>
        /// <param name="packageFileName">The name of the .pnp package file</param>
        /// <param name="persistenceConnector">The connector that reads and writes the package file</param>
        /// <param name="templateFileName">The name of the template file a new package records, optional</param>
        /// <returns>The connector of the package</returns>
        internal static FrameworkOpenXMLConnector OpenFramework(string packageFileName, FrameworkFileConnectorBase persistenceConnector, string templateFileName = null)
        {
            return Open(() => new FrameworkOpenXMLConnector(packageFileName, persistenceConnector, templateFileName: templateFileName), CorePackageMessage);
        }

        /// <summary>
        /// Opens a .pnp package from a stream with the PnP Framework engine.
        /// </summary>
        /// <param name="packageStream">The stream holding the package</param>
        /// <returns>The connector of the package</returns>
        internal static FrameworkOpenXMLConnector OpenFramework(Stream packageStream)
        {
            return Open(() => new FrameworkOpenXMLConnector(packageStream), CorePackageMessage);
        }

        /// <summary>
        /// Opens, or starts, a .pnp package with the PnP.Core.Provisioning engine.
        /// </summary>
        /// <param name="packageFileName">The name of the .pnp package file</param>
        /// <param name="persistenceConnector">The connector that reads and writes the package file</param>
        /// <param name="templateFileName">The name of the template file a new package records, optional</param>
        /// <returns>The connector of the package</returns>
        internal static CoreOpenXMLConnector OpenCore(string packageFileName, CoreFileConnectorBase persistenceConnector, string templateFileName = null)
        {
            return Open(() => new CoreOpenXMLConnector(packageFileName, persistenceConnector, templateFileName: templateFileName), FrameworkPackageMessage);
        }

        /// <summary>
        /// Opens a .pnp package from a stream with the PnP.Core.Provisioning engine.
        /// </summary>
        /// <param name="packageStream">The stream holding the package</param>
        /// <returns>The connector of the package</returns>
        internal static CoreOpenXMLConnector OpenCore(Stream packageStream)
        {
            return Open(() => new CoreOpenXMLConnector(packageStream), FrameworkPackageMessage);
        }

        private static T Open<T>(Func<T> open, string otherEngineMessage)
        {
            try
            {
                return open();
            }
            catch (InvalidCastException ex)
            {
                throw new PSInvalidOperationException(otherEngineMessage, ex);
            }
        }
    }
}
