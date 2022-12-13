using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using SecureString = System.Security.SecureString;
using RuntimeHelpers = System.Runtime.CompilerServices.RuntimeHelpers;
using System.Management.Automation;
using System.Collections;
using System.Linq;

namespace PnP.PowerShell.Commands.Utilities
{
    internal class CertificateHelper
    {
        private enum PemStringType
        {
            Certificate,
            RsaPrivateKey
        }


        internal static string PrivateKeyToBase64(X509Certificate2 certificate, bool useLineBreaks = false)
        {
#if NETFRAMEWORK
            var param = ((RSACryptoServiceProvider)certificate.PrivateKey).ExportParameters(true);
#else
            RSAParameters param = new RSAParameters();            
            switch (certificate.GetRSAPrivateKey())
            {
                case RSACng rsaCNGKey:
                    {
                        using (var rsa = MakeExportable(rsaCNGKey))
                        {
                            param = rsa.ExportParameters(true);
                        }
                        break;
                    }
                case System.Security.Cryptography.RSAOpenSsl rsaOpenSslKey:
                    {
                        param = rsaOpenSslKey.ExportParameters(true);
                        break;
                    }
            }
            //var param = ((RSACng)certificate.PrivateKey).ExportParameters(true);
#endif
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

        internal static X509Certificate2 GetCertificateFromPath(string certificatePath, SecureString certificatePassword)
        {
            if (System.IO.File.Exists(certificatePath))
            {
                var certFile = System.IO.File.OpenRead(certificatePath);
                if (certFile.Length == 0)
                    throw new PSArgumentException($"The specified certificate path '{certificatePath}' points to an empty file");

                var certificateBytes = new byte[certFile.Length];
                certFile.Read(certificateBytes, 0, (int)certFile.Length);
                var certificate = new X509Certificate2(
                    certificateBytes,
                    certificatePassword,
                    X509KeyStorageFlags.Exportable |
                    X509KeyStorageFlags.MachineKeySet |
                    X509KeyStorageFlags.PersistKeySet);
                return certificate;
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

        #region self-signed
        internal static byte[] CreateSelfSignCertificatePfx(
            string x500,
            DateTime startTime,
            DateTime endTime)
        {
            byte[] pfxData = CreateSelfSignCertificatePfx(
                x500,
                startTime,
                endTime,
                (SecureString)null);
            return pfxData;
        }

        internal static byte[] CreateSelfSignCertificatePfx(
            string x500,
            DateTime startTime,
            DateTime endTime,
            string insecurePassword)
        {
            byte[] pfxData;
            SecureString password = null;

            try
            {
                if (!string.IsNullOrEmpty(insecurePassword))
                {
                    password = new SecureString();
                    foreach (char ch in insecurePassword)
                    {
                        password.AppendChar(ch);
                    }

                    password.MakeReadOnly();
                }

                pfxData = CreateSelfSignCertificatePfx(
                    x500,
                    startTime,
                    endTime,
                    password);
            }
            finally
            {
                if (password != null)
                {
                    password.Dispose();
                }
            }

            return pfxData;
        }


#if !NETFRAMEWORK
        internal static X509Certificate2 CreateSelfSignedCertificate(string commonName, string country, string state, string locality, string organization, string organizationUnit, SecureString password, string friendlyName, DateTimeOffset from, DateTimeOffset to)
        {
            SubjectAlternativeNameBuilder sanBuilder = new SubjectAlternativeNameBuilder();
            sanBuilder.AddDnsName("localhost");
            sanBuilder.AddDnsName(Environment.MachineName);

            var x500Values = new List<string>();
            if (!string.IsNullOrWhiteSpace(commonName)) x500Values.Add($"CN={commonName}");
            if (!string.IsNullOrWhiteSpace(country)) x500Values.Add($"C={country}");
            if (!string.IsNullOrWhiteSpace(state)) x500Values.Add($"S={state}");
            if (!string.IsNullOrWhiteSpace(locality)) x500Values.Add($"L={locality}");
            if (!string.IsNullOrWhiteSpace(organization)) x500Values.Add($"O={organization}");
            if (!string.IsNullOrWhiteSpace(organizationUnit)) x500Values.Add($"OU={organizationUnit}");

            string distinguishedNameString = string.Join("; ", x500Values);

            X500DistinguishedName distinguishedName = new X500DistinguishedName(distinguishedNameString);
                        
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
#endif


        internal static byte[] CreateSelfSignCertificatePfx(
            string x500,
            DateTime startTime,
            DateTime endTime,
            SecureString password)
        {
            byte[] pfxData;

            if (x500 == null)
            {
                x500 = "";
            }

            SystemTime startSystemTime = ToSystemTime(startTime);
            SystemTime endSystemTime = ToSystemTime(endTime);
            string containerName = Guid.NewGuid().ToString();

            GCHandle dataHandle = new GCHandle();
            IntPtr providerContext = IntPtr.Zero;
            IntPtr cryptKey = IntPtr.Zero;
            IntPtr certContext = IntPtr.Zero;
            IntPtr certStore = IntPtr.Zero;
            IntPtr storeCertContext = IntPtr.Zero;
            IntPtr passwordPtr = IntPtr.Zero;
#if NETFRAMEWORK
            RuntimeHelpers.PrepareConstrainedRegions();
#endif
            try
            {
                Check(NativeMethods.CryptAcquireContextW(
                    out providerContext,
                    containerName,
                    null,
                    1, // PROV_RSA_FULL
                    8)); // CRYPT_NEWKEYSET

                Check(NativeMethods.CryptGenKey(
                    providerContext,
                    1, // AT_KEYEXCHANGE
                    1 | (2048 << 16), // CRYPT_EXPORTABLE | 2048bit
                    out cryptKey));

                IntPtr errorStringPtr;
                int nameDataLength = 0;
                byte[] nameData;

                // errorStringPtr gets a pointer into the middle of the x500 string,
                // so x500 needs to be pinned until after we've copied the value
                // of errorStringPtr.
                dataHandle = GCHandle.Alloc(x500, GCHandleType.Pinned);

                if (!NativeMethods.CertStrToNameW(
                    0x00010001, // X509_ASN_ENCODING | PKCS_7_ASN_ENCODING
                    dataHandle.AddrOfPinnedObject(),
                    3, // CERT_X500_NAME_STR = 3
                    IntPtr.Zero,
                    null,
                    ref nameDataLength,
                    out errorStringPtr))
                {
                    string error = Marshal.PtrToStringUni(errorStringPtr);
                    throw new ArgumentException(error);
                }

                nameData = new byte[nameDataLength];

                if (!NativeMethods.CertStrToNameW(
                    0x00010001, // X509_ASN_ENCODING | PKCS_7_ASN_ENCODING
                    dataHandle.AddrOfPinnedObject(),
                    3, // CERT_X500_NAME_STR = 3
                    IntPtr.Zero,
                    nameData,
                    ref nameDataLength,
                    out errorStringPtr))
                {
                    string error = Marshal.PtrToStringUni(errorStringPtr);
                    throw new ArgumentException(error);
                }

                dataHandle.Free();

                dataHandle = GCHandle.Alloc(nameData, GCHandleType.Pinned);
                CryptoApiBlob nameBlob = new CryptoApiBlob(
                    nameData.Length,
                    dataHandle.AddrOfPinnedObject());

                CryptKeyProviderInformation kpi = new CryptKeyProviderInformation();
                kpi.ContainerName = containerName;
                kpi.ProviderType = 1; // PROV_RSA_FULL
                kpi.KeySpec = 1; // AT_KEYEXCHANGE

                certContext = NativeMethods.CertCreateSelfSignCertificate(
                    providerContext,
                    ref nameBlob,
                    0,
                    ref kpi,
                    IntPtr.Zero, // default = SHA1RSA
                    ref startSystemTime,
                    ref endSystemTime,
                    IntPtr.Zero);
                Check(certContext != IntPtr.Zero);
                dataHandle.Free();

                certStore = NativeMethods.CertOpenStore(
                    "Memory", // sz_CERT_STORE_PROV_MEMORY
                    0,
                    IntPtr.Zero,
                    0x2000, // CERT_STORE_CREATE_NEW_FLAG
                    IntPtr.Zero);
                Check(certStore != IntPtr.Zero);

                Check(NativeMethods.CertAddCertificateContextToStore(
                    certStore,
                    certContext,
                    1, // CERT_STORE_ADD_NEW
                    out storeCertContext));

                NativeMethods.CertSetCertificateContextProperty(
                    storeCertContext,
                    2, // CERT_KEY_PROV_INFO_PROP_ID
                    0,
                    ref kpi);

                if (password != null)
                {
                    passwordPtr = Marshal.SecureStringToCoTaskMemUnicode(password);
                }

                CryptoApiBlob pfxBlob = new CryptoApiBlob();
                Check(NativeMethods.PFXExportCertStoreEx(
                    certStore,
                    ref pfxBlob,
                    passwordPtr,
                    IntPtr.Zero,
                    7)); // EXPORT_PRIVATE_KEYS | REPORT_NO_PRIVATE_KEY | REPORT_NOT_ABLE_TO_EXPORT_PRIVATE_KEY

                pfxData = new byte[pfxBlob.DataLength];
                dataHandle = GCHandle.Alloc(pfxData, GCHandleType.Pinned);
                pfxBlob.Data = dataHandle.AddrOfPinnedObject();
                Check(NativeMethods.PFXExportCertStoreEx(
                    certStore,
                    ref pfxBlob,
                    passwordPtr,
                    IntPtr.Zero,
                    7)); // EXPORT_PRIVATE_KEYS | REPORT_NO_PRIVATE_KEY | REPORT_NOT_ABLE_TO_EXPORT_PRIVATE_KEY
                dataHandle.Free();
            }
            finally
            {
                if (passwordPtr != IntPtr.Zero)
                {
                    Marshal.ZeroFreeCoTaskMemUnicode(passwordPtr);
                }

                if (dataHandle.IsAllocated)
                {
                    dataHandle.Free();
                }

                if (certContext != IntPtr.Zero)
                {
                    NativeMethods.CertFreeCertificateContext(certContext);
                }

                if (storeCertContext != IntPtr.Zero)
                {
                    NativeMethods.CertFreeCertificateContext(storeCertContext);
                }

                if (certStore != IntPtr.Zero)
                {
                    NativeMethods.CertCloseStore(certStore, 0);
                }

                if (cryptKey != IntPtr.Zero)
                {
                    NativeMethods.CryptDestroyKey(cryptKey);
                }

                if (providerContext != IntPtr.Zero)
                {
                    NativeMethods.CryptReleaseContext(providerContext, 0);
                    NativeMethods.CryptAcquireContextW(
                        out providerContext,
                        containerName,
                        null,
                        1, // PROV_RSA_FULL
                        0x10); // CRYPT_DELETEKEYSET
                }
            }

            return pfxData;
        }

        private static SystemTime ToSystemTime(DateTime dateTime)
        {
            long fileTime = dateTime.ToFileTime();
            SystemTime systemTime;
            Check(NativeMethods.FileTimeToSystemTime(ref fileTime, out systemTime));
            return systemTime;
        }

        private static void Check(bool nativeCallSucceeded)
        {
            if (!nativeCallSucceeded)
            {
                int error = Marshal.GetHRForLastWin32Error();
                Marshal.ThrowExceptionForHR(error);
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct SystemTime
        {
            public short Year;
            public short Month;
            public short DayOfWeek;
            public short Day;
            public short Hour;
            public short Minute;
            public short Second;
            public short Milliseconds;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct CryptoApiBlob
        {
            public int DataLength;
            public IntPtr Data;

            public CryptoApiBlob(int dataLength, IntPtr data)
            {
                this.DataLength = dataLength;
                this.Data = data;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct CryptKeyProviderInformation
        {
            [MarshalAs(UnmanagedType.LPWStr)] public string ContainerName;
            [MarshalAs(UnmanagedType.LPWStr)] public string ProviderName;
            public int ProviderType;
            public int Flags;
            public int ProviderParameterCount;
            public IntPtr ProviderParameters; // PCRYPT_KEY_PROV_PARAM
            public int KeySpec;
        }

        private static class NativeMethods
        {
            [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool FileTimeToSystemTime(
                [In] ref long fileTime,
                out SystemTime systemTime);

            [DllImport("AdvApi32.dll", SetLastError = true, ExactSpelling = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool CryptAcquireContextW(
                out IntPtr providerContext,
                [MarshalAs(UnmanagedType.LPWStr)] string container,
                [MarshalAs(UnmanagedType.LPWStr)] string provider,
                int providerType,
                int flags);

            [DllImport("AdvApi32.dll", SetLastError = true, ExactSpelling = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool CryptReleaseContext(
                IntPtr providerContext,
                int flags);

            [DllImport("AdvApi32.dll", SetLastError = true, ExactSpelling = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool CryptGenKey(
                IntPtr providerContext,
                int algorithmId,
                int flags,
                out IntPtr cryptKeyHandle);

            [DllImport("AdvApi32.dll", SetLastError = true, ExactSpelling = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool CryptDestroyKey(
                IntPtr cryptKeyHandle);

            [DllImport("Crypt32.dll", SetLastError = true, ExactSpelling = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool CertStrToNameW(
                int certificateEncodingType,
                IntPtr x500,
                int strType,
                IntPtr reserved,
                [MarshalAs(UnmanagedType.LPArray)][Out] byte[] encoded,
                ref int encodedLength,
                out IntPtr errorString);

            [DllImport("Crypt32.dll", SetLastError = true, ExactSpelling = true)]
            public static extern IntPtr CertCreateSelfSignCertificate(
                IntPtr providerHandle,
                [In] ref CryptoApiBlob subjectIssuerBlob,
                int flags,
                [In] ref CryptKeyProviderInformation keyProviderInformation,
                IntPtr signatureAlgorithm,
                [In] ref SystemTime startTime,
                [In] ref SystemTime endTime,
                IntPtr extensions);

            [DllImport("Crypt32.dll", SetLastError = true, ExactSpelling = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool CertFreeCertificateContext(
                IntPtr certificateContext);

            [DllImport("Crypt32.dll", SetLastError = true, ExactSpelling = true)]
            public static extern IntPtr CertOpenStore(
                [MarshalAs(UnmanagedType.LPStr)] string storeProvider,
                int messageAndCertificateEncodingType,
                IntPtr cryptProvHandle,
                int flags,
                IntPtr parameters);

            [DllImport("Crypt32.dll", SetLastError = true, ExactSpelling = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool CertCloseStore(
                IntPtr certificateStoreHandle,
                int flags);

            [DllImport("Crypt32.dll", SetLastError = true, ExactSpelling = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool CertAddCertificateContextToStore(
                IntPtr certificateStoreHandle,
                IntPtr certificateContext,
                int addDisposition,
                out IntPtr storeContextPtr);

            [DllImport("Crypt32.dll", SetLastError = true, ExactSpelling = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool CertSetCertificateContextProperty(
                IntPtr certificateContext,
                int propertyId,
                int flags,
                [In] ref CryptKeyProviderInformation data);

            [DllImport("Crypt32.dll", SetLastError = true, ExactSpelling = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool PFXExportCertStoreEx(
                IntPtr certificateStoreHandle,
                ref CryptoApiBlob pfxBlob,
                IntPtr password,
                IntPtr reserved,
                int flags);
        }

#endregion
    }

    internal class CertificateDistinguishedName
    {
        // Name of a person or an object host name
        public string CommonName;

        // 2-character ISO country code 
        public string Country;

        // The state or province where the owner is physically located
        public string StateOrProvince;

        // The city where the owner is located
        public string Locality;

        // The name of the registering organization
        public string Organization;

        // The division of the organization owning the certificate
        public string OrganizationalUnit;

        // Format the distinguished name like 'CN="com.contoso"; C="US"; S="Nebraska"'
        public string Format()
        {
            return this.Format(';', true);
        }

        // Format the distinguished name with the given separator and quote usage setting
        public string Format(char Separator, bool useQuotes)
        {
            var sb = new StringBuilder();

            if (useQuotes)
            {
                sb.Append($@"CN=""{CommonName}""");
            }
            else
            {
                sb.Append($"CN={CommonName}");
            }

            var fields = new Hashtable() {
                {"OU",OrganizationalUnit},
                {"O",Organization},
                {"L",Locality},
                {"S",StateOrProvince},
                {"C",Country}
            };

            foreach (var field in new[] { "OU", "O", "L", "S", "C" })
            {
                var val = fields[field];
                if (val != null)
                {
                    sb.Append(Separator);
                    sb.Append(" ");
                    if (useQuotes)
                    {
                        sb.Append($@"{field}=""{val}""");
                    }
                    else
                    {
                        sb.Append($"{field}={val}");
                    }
                }
            }
            return sb.ToString();
        }

        public override string ToString()
        {
            return Format(',', false);
        }

        public X500DistinguishedName AsX500DistinguishedName()
        {
            return new X500DistinguishedName(Format());
        }
    }

    internal enum EnhancedKeyUsageEnum
    {
        ServerAuthentication,
        ClientAuthentication
    }

}
