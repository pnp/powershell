using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using SecureString = System.Security.SecureString;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Utilities
{
    internal class CertificateHelper
    {
        internal static string PrivateKeyToBase64(X509Certificate2 certificate, bool useLineBreaks = false)
        {

            RSAParameters param = new RSAParameters();

            var a = certificate.GetRSAPrivateKey();
            
            using(var rsa = MakeExportable(a))
            {
                param = rsa.ExportParameters(true);
            }
          
            string base64String;
            using (var stream = new MemoryStream())
            {
                var writer = new BinaryWriter(stream);
                writer.Write((byte)0x30); // SEQUENCE
                using (var innerStream = new MemoryStream())
                {
                    var innerWriter = new BinaryWriter(innerStream);
                    EncodeIntegerBigEndian(innerWriter, new byte[] { 0x00 }); // Version
                    EncodeIntegerBigEndian(innerWriter, param.Modulus);
                    EncodeIntegerBigEndian(innerWriter, param.Exponent);
                    EncodeIntegerBigEndian(innerWriter, param.D);
                    EncodeIntegerBigEndian(innerWriter, param.P);
                    EncodeIntegerBigEndian(innerWriter, param.Q);
                    EncodeIntegerBigEndian(innerWriter, param.DP);
                    EncodeIntegerBigEndian(innerWriter, param.DQ);
                    EncodeIntegerBigEndian(innerWriter, param.InverseQ);
                    var length = (int)innerStream.Length;
                    EncodeLength(writer, length);
                    writer.Write(innerStream.GetBuffer(), 0, length);
                }

                base64String = Convert.ToBase64String(stream.GetBuffer(), 0, (int)stream.Length);
            }

            StringBuilder sb = new StringBuilder();
            if (useLineBreaks)
            {
                sb.AppendLine("-----BEGIN RSA PRIVATE KEY-----");
                sb.AppendLine(string.Join(Environment.NewLine, SplitText(base64String, 64)));
                sb.AppendLine("-----END RSA PRIVATE KEY-----");
            }
            else
            {
                sb.Append("-----BEGIN RSA PRIVATE KEY-----");
                sb.Append(base64String);
                sb.Append("-----END RSA PRIVATE KEY-----");
            }

            return sb.ToString();
        }

        internal static string CertificateToBase64(X509Certificate2 certificate, bool useLineBreaks = false)
        {
            string base64String = Convert.ToBase64String(certificate.GetRawCertData());
            StringBuilder sb = new StringBuilder();
            if (useLineBreaks)
            {
                sb.AppendLine("-----BEGIN CERTIFICATE-----");
                sb.AppendLine(string.Join(Environment.NewLine, SplitText(base64String, 64)));
                sb.AppendLine("-----END CERTIFICATE-----");
            }
            else
            {
                sb.Append("-----BEGIN CERTIFICATE-----");
                sb.Append(base64String);
                sb.Append("-----END CERTIFICATE-----");
            }

            return sb.ToString();
        }

        internal static X509Certificate2 GetCertificateFromStore(string thumbprint)
        {
            List<StoreLocation> locations = new List<StoreLocation>
            {
                StoreLocation.CurrentUser,
                StoreLocation.LocalMachine
            };

            foreach (var location in locations)
            {
                X509Store store = new X509Store("My", location);
                try
                {
                    store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
                    X509Certificate2Collection certificates = store.Certificates.Find(
                        X509FindType.FindByThumbprint, thumbprint, false);
                    if (certificates.Count == 1)
                    {
                        return certificates[0];
                    }
                }
                finally
                {
                    store.Close();
                }
            }

            return null;
        }

        /// <summary>
        /// Opens the X509Certificate2 at the provided path using the provided certificate password
        /// </summary>
        /// <param name="cmdlet">Cmdlet executing this function</param>
        /// <param name="certificatePath">Path to the private key certificate file</param>
        /// <param name="certificatePassword">Password to open the certificate or NULL if no password set on the certificate</param>
        /// <param name="x509KeyStorageFlags">Key storage flags for created X509Certificate2</param>
        /// <returns>X509Certificate2 instance</returns>
        /// <exception cref="PSArgumentException">Thrown if the certificate cannot be read</exception>
        /// <exception cref="FileNotFoundException">Thrown if the certificate cannot be found at the provided path</exception>
        internal static X509Certificate2 GetCertificateFromPath(Cmdlet cmdlet, string certificatePath, SecureString certificatePassword, 
            X509KeyStorageFlags x509KeyStorageFlags = 
                        X509KeyStorageFlags.Exportable |
                        X509KeyStorageFlags.MachineKeySet |
                        X509KeyStorageFlags.PersistKeySet)
        {
            if (System.IO.File.Exists(certificatePath))
            {
                cmdlet.WriteVerbose($"Reading certificate from file '{certificatePath}'");

                var certFile = System.IO.File.OpenRead(certificatePath);
                if (certFile.Length == 0)
                {
                    throw new PSArgumentException($"The specified certificate path '{certificatePath}' points to an empty file");
                }

                var certificateBytes = new byte[certFile.Length];
                certFile.Read(certificateBytes, 0, (int)certFile.Length);

                cmdlet.WriteVerbose($"Opening certificate in file '{certificatePath}' {(certificatePassword == null ? "without" : "using")} a certificate password");
                try
                {
                    var certificate = new X509Certificate2(
                        certificateBytes,
                        certificatePassword,
                        x509KeyStorageFlags
                        );
                    return certificate;
                }
                catch (CryptographicException e)
                {
                    throw new PSArgumentException($"The specified certificate at '{certificatePath}' could not be read. The certificate could be corrupt or it may require a password which has not been provided or is incorrect.", e);
                }
            }
            else if (System.IO.Directory.Exists(certificatePath))
            {
                throw new FileNotFoundException($"The specified certificate path '{certificatePath}' points to a folder", certificatePath);
            }
            else
            {
                throw new FileNotFoundException($"The specified certificate path '{certificatePath}' does not exist", certificatePath);
            }
        }

        #region certificate manipulation
        private static void EncodeLength(BinaryWriter stream, int length)
        {
            if (length < 0x80)
            {
                // Short form
                stream.Write((byte)length);
            }
            else
            {
                // Long form
                var temp = length;
                var bytesRequired = 0;
                while (temp > 0)
                {
                    temp >>= 8;
                    bytesRequired++;
                }
                stream.Write((byte)(bytesRequired | 0x80));
                for (var i = bytesRequired - 1; i >= 0; i--)
                {
                    stream.Write((byte)(length >> (8 * i) & 0xff));
                }
            }
        }

        private static void EncodeIntegerBigEndian(BinaryWriter stream, byte[] value, bool forceUnsigned = true)
        {
            stream.Write((byte)0x02); // INTEGER
            var prefixZeros = 0;
            for (var i = 0; i < value.Length; i++)
            {
                if (value[i] != 0) break;
                prefixZeros++;
            }
            if (value.Length - prefixZeros == 0)
            {
                EncodeLength(stream, 1);
                stream.Write((byte)0);
            }
            else
            {
                if (forceUnsigned && value[prefixZeros] > 0x7f)
                {
                    // Add a prefix zero to force unsigned if the MSB is 1
                    EncodeLength(stream, value.Length - prefixZeros + 1);
                    stream.Write((byte)0);
                }
                else
                {
                    EncodeLength(stream, value.Length - prefixZeros);
                }
                for (var i = prefixZeros; i < value.Length; i++)
                {
                    stream.Write(value[i]);
                }
            }
        }

        private static IEnumerable<string> SplitText(string text, int length)
        {
            for (int i = 0; i < text.Length; i += length)
            {
                yield return text.Substring(i, Math.Min(length, text.Length - i));
            }
        }

        #endregion

        internal static X509Certificate2 CreateSelfSignedCertificate(string commonName, string country, string state, string locality, string organization, string organizationUnit, SecureString password, string friendlyName, DateTimeOffset from, DateTimeOffset to, string[] sanNames = null)
        {
            SubjectAlternativeNameBuilder sanBuilder = new SubjectAlternativeNameBuilder();
            if (sanNames != null)
            {
                foreach (var sanName in sanNames)
                {
                    sanBuilder.AddDnsName(sanName);
                }
            }
            else
            {
                sanBuilder.AddDnsName("localhost");
                sanBuilder.AddDnsName(Environment.MachineName);
            }

            var x500Values = new List<string>();
            if (!string.IsNullOrWhiteSpace(commonName)) x500Values.Add($"CN={commonName}");
            if (!string.IsNullOrWhiteSpace(country)) x500Values.Add($"C={country}");
            if (!string.IsNullOrWhiteSpace(state)) x500Values.Add($"S={state}");
            if (!string.IsNullOrWhiteSpace(locality)) x500Values.Add($"L={locality}");
            if (!string.IsNullOrWhiteSpace(organization)) x500Values.Add($"O={organization}");
            if (!string.IsNullOrWhiteSpace(organizationUnit)) x500Values.Add($"OU={organizationUnit}");

            string distinguishedNameString = string.Join("; ", x500Values);

            X500DistinguishedName distinguishedName = new X500DistinguishedName(distinguishedNameString);

#pragma warning disable CA1416 // Validate platform compatibility
            using (RSA rsa = Platform.IsWindows ? MakeExportable(new RSACng(2048)) : RSA.Create(2048))
            {                
                var request = new CertificateRequest(distinguishedName, rsa, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);

                request.CertificateExtensions.Add(
                    new X509KeyUsageExtension(X509KeyUsageFlags.DataEncipherment | X509KeyUsageFlags.KeyEncipherment | X509KeyUsageFlags.DigitalSignature, false));

                request.CertificateExtensions.Add(
                   new X509EnhancedKeyUsageExtension(
                       new OidCollection { new Oid("1.3.6.1.5.5.7.3.1") }, false));

                request.CertificateExtensions.Add(sanBuilder.Build());

                var certificate = request.CreateSelfSigned(from, to);

                if (Platform.IsWindows)
                {
                    certificate.FriendlyName = friendlyName;
                }

                return new X509Certificate2(certificate.Export(X509ContentType.Pfx, password), password, X509KeyStorageFlags.Exportable | X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.PersistKeySet);
            }
#pragma warning restore CA1416 // Validate platform compatibility
        }

        internal static RSA MakeExportable(RSA rsa)
        {
            if (rsa is RSACng rsaCng)
            {
                const CngExportPolicies Exportability =
                    CngExportPolicies.AllowExport |
                    CngExportPolicies.AllowPlaintextExport;

                if ((rsaCng.Key.ExportPolicy & Exportability) == CngExportPolicies.AllowExport)
                {
                    RSA copy = RSA.Create();

                    copy.ImportEncryptedPkcs8PrivateKey(
                        nameof(MakeExportable),
                        rsa.ExportEncryptedPkcs8PrivateKey(
                            nameof(MakeExportable),
                            new PbeParameters(
                                PbeEncryptionAlgorithm.TripleDes3KeyPkcs12,
                                HashAlgorithmName.SHA1,
                                2048)),
                        out _);
                    return copy;
                }
            }

            return rsa;
        }
    }
}
