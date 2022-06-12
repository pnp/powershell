
using System;
using System.Linq;
using System.Management.Automation;
using System.Reflection;
using PnP.PowerShell.Commands.Provider;

namespace PnP.PowerShell.Commands.Base
{
    [Cmdlet(VerbsCommunications.Disconnect, "PnPOnline")]
    [OutputType(typeof(void))]
    public class DisconnectOnline : PSCmdlet
    {
        [Parameter(Mandatory = false)]
        public PnPConnection Connection = null;

        protected override void ProcessRecord()
        {
            // If no specific connection has been passed in, take the connection from the current context
            var connection = Connection ?? PnPConnection.Current;

            if (connection?.Certificate != null)
            {
                if (connection != null && connection.DeleteCertificateFromCacheOnDisconnect)
                {
                    PnPConnection.CleanupCryptoMachineKey(connection.Certificate);
                }
                connection.Certificate = null;
            }

            var success = DisconnectProvidedService(ref connection);
            
            if (!success)
            {
                throw new InvalidOperationException(Properties.Resources.NoConnectionToDisconnect);
            }

            var provider = SessionState.Provider.GetAll().FirstOrDefault(p => p.Name.Equals(SPOProvider.PSProviderName, StringComparison.InvariantCultureIgnoreCase));
            if (provider != null)
            {
                //ImplementingAssembly was introduced in Windows PowerShell 5.0.
                var drives = Host.Version.Major >= 5 ? provider.Drives.Where(d => d.Provider.Module.Name == Assembly.GetExecutingAssembly().FullName) : provider.Drives;
                foreach (var drive in drives)
                {
                    SessionState.Drive.Remove(drive.Name, true, "Global");
                }
            }
        }

        internal static bool DisconnectProvidedService(ref PnPConnection connection)
        {
            Environment.SetEnvironmentVariable("PNPPSHOST", string.Empty);
            Environment.SetEnvironmentVariable("PNPPSSITE", string.Empty);
            if (connection == null)
            {
                return false;
            }
            connection.Context = null;
            connection = null;
            return true;
        }
    }
}