
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
        [Obsolete("The -Connection parameter technically cannot function and therefore will be removed in a future version. Read the documentation of this cmdlet how to best deal with this: https://pnp.github.io/powershell/cmdlets/Disconnect-PnPOnline.html")]
        public PnPConnection Connection = null;

        protected override void ProcessRecord()
        {
            // As parameters are passed in by value, there's no use in doing anything with the connection object here, so we'll simply exit.
            #pragma warning disable CS0618 
            if(Connection != null) return;
            #pragma warning restore CS6018

            if(PnPConnection.Current == null)
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