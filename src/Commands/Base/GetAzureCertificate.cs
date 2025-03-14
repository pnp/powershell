using System.Management.Automation;
using System;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using PnP.PowerShell.Commands.Utilities;
using System.Security.Cryptography;
using System.Linq;

namespace PnP.PowerShell.Commands.Base
{
    [Cmdlet(VerbsCommon.Get, "PnPAzureCertificate", DefaultParameterSetName = "SELF")]
    [OutputType(typeof(Model.AzureCertificate))]
    public class GetPnPAzureCertificate : BasePSCmdlet
    {
        [Parameter(Mandatory = true)]
        [Alias("CertificatePath")]
        public string Path;

        [Parameter(Mandatory = false)]
        [Alias("CertificatePassword")]
        public SecureString Password;

        protected override void ProcessRecord()
        {
            if (!System.IO.Path.IsPathRooted(Path))
            {
                Path = System.IO.Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, Path);
            }
            if (System.IO.File.Exists(Path))
            {
                var certificate = new X509Certificate2(Path, Password, X509KeyStorageFlags.Exportable | X509KeyStorageFlags.MachineKeySet);
                WriteAzureCertificateOutput(this, certificate, Password);
            }
            else
            {
                throw new PSArgumentException("Certificate file does not exist");
            }
        }

        static string GetManifestEntry(X509Certificate2 certificate)
        {
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
            return manifestEntry;
        }

        static string/*?*/ GetPfxBase64OrWarn(Cmdlet cmdlet, X509Certificate2 certificate, SecureString password)
        {
            try
            {
                var pfxBytes = certificate.Export(X509ContentType.Pfx, password);
                var base64string = Convert.ToBase64String(pfxBytes);
                return base64string;
            }
            catch (Exception ex)
            {
                cmdlet.WriteWarning(ex.Message);
                return null;
            }
        }

        internal static void WriteAzureCertificateOutput(PSCmdlet cmdlet, X509Certificate2 certificate, SecureString password)
        {
            string manifestEntry = GetManifestEntry(certificate);
            var pfxBase64 = GetPfxBase64OrWarn(cmdlet, certificate, password);

            var record = new Model.AzureCertificate(
                subject: certificate.Subject,
                notBefore: certificate.NotBefore,
                notAfter: certificate.NotAfter,
                thumbprint: certificate.Thumbprint,
                pfxBase64: pfxBase64,
                keyCredentials: manifestEntry,
                certificate: CertificateHelper.CertificateToBase64(certificate),
                privateKey: CertificateHelper.PrivateKeyToBase64(certificate),
                sanNames: certificate.Extensions.Cast<X509Extension>()
                                                .Where(n => n.Oid.FriendlyName=="Subject Alternative Name")
                                                .Select(n => new AsnEncodedData(n.Oid, n.RawData))
                                                .Select(n => n.Format(false))
                                                .FirstOrDefault().Split(',', StringSplitOptions.TrimEntries)
            );

            cmdlet.WriteObject(record);
        }
    }
}
