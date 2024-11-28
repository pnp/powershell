
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
        public SwitchParameter ClearPersistedLogin;

        protected override void ProcessRecord()
        {
            if (PnPConnection.Current == null)
            {
                throw new InvalidOperationException(Properties.Resources.NoConnectionToDisconnect);
            }

            Environment.SetEnvironmentVariable("PNPPSHOST", string.Empty);
            Environment.SetEnvironmentVariable("PNPPSSITE", string.Empty);

            if (PnPConnection.Current.Certificate != null)
            {
                if (PnPConnection.Current.DeleteCertificateFromCacheOnDisconnect)
                {
                    PnPConnection.CleanupCryptoMachineKey(PnPConnection.Current.Certificate);
                }
                PnPConnection.Current.Certificate = null;
            }

            if(ClearPersistedLogin)
            {
                PnPConnection.ClearCache(PnPConnection.Current);
            }

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
    }
}