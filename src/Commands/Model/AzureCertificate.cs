using PnP.PowerShell.Commands.Utilities;

using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace PnP.PowerShell.Commands.Model
{
    public sealed class AzureCertificate
    {
        internal AzureCertificate(string subject, DateTime notBefore, DateTime notAfter, string thumbprint, string/*?*/ pfxBase64, string keyCredentials, string certificate, string privateKey)
        {
            Subject = subject ?? throw new ArgumentNullException(nameof(subject));
            NotBefore = notBefore;
            NotAfter = notAfter;
            Thumbprint = thumbprint ?? throw new ArgumentNullException(nameof(thumbprint));
            PfxBase64 = pfxBase64;
            KeyCredentials = keyCredentials ?? throw new ArgumentNullException(nameof(keyCredentials));
            Certificate = certificate ?? throw new ArgumentNullException(nameof(certificate));
            PrivateKey = privateKey ?? throw new ArgumentNullException(nameof(privateKey));
        }

        public string Subject { get; }
        public DateTime NotBefore { get; }
        public DateTime NotAfter { get; }
        public string Thumbprint { get; }
        public string/*?*/ PfxBase64 { get; }
        public string KeyCredentials { get; }
        public string Certificate { get; }
        public string PrivateKey { get; }

    }
}
