using System.Management.Automation;
using PnP.PowerShell.CmdletHelpAttributes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using PnP.PowerShell.Commands.Utilities;

namespace PnP.PowerShell.Commands.Base
{
    [Cmdlet(VerbsCommon.Get, "PnPAzureCertificate", DefaultParameterSetName = "SELF")]
    public class GetPnPAdalCertificate : PSCmdlet
    {
        [Parameter(Mandatory = true)]
        public string CertificatePath;

        [Parameter(Mandatory = false)]
        public SecureString CertificatePassword;

        protected override void ProcessRecord()
        {
            if (!Path.IsPathRooted(CertificatePath))
            {
                CertificatePath = Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, CertificatePath);
            }
            var certificate = new X509Certificate2(CertificatePath, CertificatePassword, X509KeyStorageFlags.Exportable | X509KeyStorageFlags.MachineKeySet);
            var rawCert = certificate.GetRawCertData();
            var base64Cert = Convert.ToBase64String(rawCert);
            var rawCertHash = certificate.GetCertHash();
            var base64CertHash = Convert.ToBase64String(rawCertHash);
            var keyId = Guid.NewGuid();

            var template = @"
{{
    ""customKeyIdentifier"": ""{0}"",
    ""keyId"": ""{1}"",
    ""type"": ""AsymmetricX509Cert"",
    ""usage"": ""Verify"",
    ""value"":  ""{2}""
}}
";
            var manifestEntry = string.Format(template, base64CertHash, keyId, base64Cert);

            var record = new PSObject();
            record.Properties.Add(new PSVariableProperty(new PSVariable("Subject", certificate.Subject)));
            record.Properties.Add(new PSVariableProperty(new PSVariable("ValidFrom", certificate.NotBefore)));
            record.Properties.Add(new PSVariableProperty(new PSVariable("ValidTo", certificate.NotAfter)));
            record.Properties.Add(new PSVariableProperty(new PSVariable("Thumbprint", certificate.Thumbprint)));

            record.Properties.Add(new PSVariableProperty(new PSVariable("KeyCredentials", manifestEntry)));
            record.Properties.Add(new PSVariableProperty(new PSVariable("Certificate", CertificateHelper.CertificateToBase64(certificate))));
            record.Properties.Add(new PSVariableProperty(new PSVariable("PrivateKey", CertificateHelper.PrivateKeyToBase64(certificate))));

            WriteObject(record);
        }
    }
}
