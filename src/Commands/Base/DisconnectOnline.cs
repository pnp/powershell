
using System;
using System.Linq;
using System.Management.Automation;
using System.Reflection;
using PnP.PowerShell.Commands.Provider;
using PnP.PowerShell.Commands.Model;

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
            if (Connection == null)
            {
                Connection = PnPConnection.Current;
            }
            if (Connection?.Certificate != null)
            {
                if (Connection != null && Connection.DeleteCertificateFromCacheOnDisconnect)
                {
                    PnPConnection.CleanupCryptoMachineKey(Connection.Certificate);
                }
                Connection.Certificate = null;
            }
            var success = false;
            if (Connection != null)
            {
                success = DisconnectProvidedService(Connection);
            }
            else
            {
                success = DisconnectCurrentService();
            }
            if (!success)
            {
                throw new InvalidOperationException(Properties.Resources.NoConnectionToDisconnect);
            }

            // clear credentials
            PnPConnection.Current = null;

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

        internal static bool DisconnectProvidedService(PnPConnection connection)
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

        internal static bool DisconnectCurrentService()
        {
            Environment.SetEnvironmentVariable("PNPPSHOST", string.Empty);
            Environment.SetEnvironmentVariable("PNPPSSITE", string.Empty);

            if (PnPConnection.Current == null)
            {
                return false;
            }
            else
            {
                PnPConnection.Current.Context = null;
                PnPConnection.Current = null;
                return true;
            }
        }
    }
}
