using System.Collections.Generic;

namespace PnP.PowerShell.Commands.Model
{
    /// <summary>The outcome of setting up a PnP PowerShell environment: the registration created, where its certificate went, and whether the connection holds the permissions it needs.</summary>
    public sealed class PnPEnvironmentSetupResult
    {
        /// <summary>The site collection the environment was set up for.</summary>
        public string Url { get; internal set; }

        /// <summary>The tenant the application registration was created in.</summary>
        public string Tenant { get; internal set; }

        /// <summary>Display name of the application registration.</summary>
        public string ApplicationName { get; internal set; }

        /// <summary>Client id of the application registration.</summary>
        public string ClientId { get; internal set; }

        /// <summary>Thumbprint of the certificate the registration authenticates with, NULL when only delegated permissions were requested.</summary>
        public string CertificateThumbprint { get; internal set; }

        /// <summary>Path of the PFX, NULL when no certificate was created; it is written even when the certificate also goes into the Windows store.</summary>
        public string CertificatePath { get; internal set; }

        /// <summary>Whether the client id was written to the credential store, so it can be looked up rather than remembered.</summary>
        public bool AppIdStored { get; internal set; }

        /// <summary>Whether a connection was established with the new registration.</summary>
        public bool Connected { get; internal set; }

        /// <summary>Outcome of Test-PnPConnectionPermission per cmdlet; NULL where the requirement could not be determined, which is not the same as it being unmet.</summary>
        public Dictionary<string, bool?> PermissionChecks { get; } = new Dictionary<string, bool?>();

        /// <summary>TRUE when a check returned FALSE so consent is outstanding, FALSE when the checks passed, NULL when no check ran and nothing was verified.</summary>
        public bool? ConsentRequired { get; internal set; }

        /// <summary>URL granting admin consent for every permission on the registration in one step.</summary>
        public string ConsentUrl { get; internal set; }

        /// <summary>URL of the API permissions blade of the registration, for reviewing before granting consent.</summary>
        public string PortalUrl { get; internal set; }

        /// <summary>The Connect-PnPOnline command to use for every later connection to this site.</summary>
        public string NextStep { get; internal set; }
    }
}
