using PnP.Framework.Provisioning.Model;
using PnP.Framework.Provisioning.Providers;
using PnP.Framework.Provisioning.Providers.Xml;

using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Xml.Linq;
using PnP.Framework.Provisioning.Connectors.OpenXML;
using PnP.Framework.Provisioning.Connectors.OpenXML.Model;
using PnP.PowerShell.Commands.Utilities;

namespace PnP.PowerShell.Commands.Provisioning
{
    [Cmdlet(VerbsCommon.New, "PnPSiteTemplateFromFolder")]
    public class NewSiteTemplateFromFolder : PnPWebCmdlet
    {
        [Parameter(Mandatory = false, Position = 0)]
        public string Out;

        [Parameter(Mandatory = false, Position = 0)]
        public string Folder;

        [Parameter(Mandatory = false, Position = 1)]
        public string TargetFolder;

        [Parameter(Mandatory = false)]
        public string Match = "*.*";

        [Parameter(Mandatory = false)]
        public ContentTypePipeBind ContentType;

        [Parameter(Mandatory = false)]
        public Hashtable Properties;

        [Parameter(Mandatory = false, Position = 1)]
        public XMLPnPSchemaVersion Schema = XMLPnPSchemaVersion.LATEST;

        [Parameter(Mandatory = false)]
        public SwitchParameter AsIncludeFile;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        [Parameter(Mandatory = false)]
        public System.Text.Encoding Encoding = System.Text.Encoding.Unicode;

        protected override void ExecuteCmdlet()
        {
            Microsoft.SharePoint.Client.ContentType ct = null;
            if (string.IsNullOrEmpty(Folder))
            {
                Folder = SessionState.Path.CurrentFileSystemLocation.Path;
            }
            else
            {
                if (!Path.IsPathRooted(Folder))
                {
                    Folder = Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, Folder);
                    Folder = new DirectoryInfo(Folder).FullName.TrimEnd('\\', '/'); // normalize away relative ./ paths
                }
            }
            if (ContentType != null)
            {
                ct = ContentType.GetContentType(CurrentWeb);
            }
            if (TargetFolder == null)
            {
                TargetFolder = new DirectoryInfo(SessionState.Path.CurrentFileSystemLocation.Path).Name;
            }

            if (!string.IsNullOrEmpty(Out))
            {
                if (!ShouldContinue()) return;

                if (Path.GetExtension(Out).ToLower() == ".pnp")
                {
                    byte[] pack = CreatePnPPackageFile(ct?.StringId);
                    System.IO.File.WriteAllBytes(Out, pack);
                }
                else
                {
                    var xml = CreateXmlAsStringFrom(ct?.StringId);
                    System.IO.File.WriteAllText(Out, xml, Encoding);
                }
            }
            else
            {
                var xml = CreateXmlAsStringFrom(ct?.StringId);
                WriteObject(xml);
            }
        }

        private bool ShouldContinue()
        {
            if (!Path.IsPathRooted(Out))
            {
                Out = Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, Out);
            }

            bool shouldContinue = true;
            if (System.IO.File.Exists(Out))
            {
                shouldContinue = (Force ||
                                  ShouldContinue(string.Format(Commands.Properties.Resources.File0ExistsOverwrite, Out),
                                      Commands.Properties.Resources.Confirm));
            }
            return shouldContinue;
        }

        private byte[] CreatePnPPackageFile(string ctId)
        {
            PnPInfo info = new PnPInfo
            {
                Manifest = new PnPManifest()
                {
                    Type = PackageType.Full
                },
                Properties = new PnPProperties()
                {
                    Generator = PnP.Framework.Utilities.PnPCoreUtilities.PnPCoreVersionTag,
                    Author = string.Empty,
                },
                Files = new List<PnPFileInfo>()
            };
            DirectoryInfo dirInfo = new DirectoryInfo(Path.GetFullPath(Folder));
            string templateFileName = Path.GetFileNameWithoutExtension(Out) + ".xml";
            var xml = CreateXmlAsStringFrom(ctId);
            PnPFileInfo templateInfo = new PnPFileInfo
            {
                InternalName = templateFileName.AsInternalFilename(),
                OriginalName = templateFileName,
                Folder = "",
                Content = System.Text.Encoding.UTF8.GetBytes(xml)
            };
            info.Files.Add(templateInfo);

            foreach (var currentFile in dirInfo.GetFiles("*.*", SearchOption.AllDirectories))
            {
                var folder = GetFolderName(currentFile, dirInfo);
                PnPFileInfo fileInfo = new PnPFileInfo
                {
                    InternalName = currentFile.Name.AsInternalFilename(),
                    OriginalName = currentFile.Name,
                    Folder = folder,
                    Content = System.IO.File.ReadAllBytes(currentFile.FullName)
                };
                WriteVerbose("Adding file:" + currentFile.Name + " - " + folder);
                info.Files.Add(fileInfo);
            }
            byte[] pack = info.PackTemplate().ToArray();
            return pack;
        }

        private string GetFolderName(FileInfo currentFile, DirectoryInfo rootFolderInfo)
        {
            var fileFolder = currentFile.DirectoryName ?? string.Empty;
            fileFolder = fileFolder.Replace('\\', '/').Replace(' ', '_');
            var rootFolder = rootFolderInfo.FullName.Replace('\\', '/').Replace(' ', '_').TrimEnd('/');
            return fileFolder.Replace(rootFolder, "");
        }

        private string CreateXmlAsStringFrom(string ctId)
        {
            var xml = GetFiles(Schema, Folder, ctId);
            if (!AsIncludeFile) return xml;
            XElement xElement = XElement.Parse(xml);
            // Get the Files Element

            XNamespace pnp;

            switch (Schema)
            {
                case XMLPnPSchemaVersion.V201909:
                    pnp = XMLConstants.PROVISIONING_SCHEMA_NAMESPACE_2019_09;
                    break;
                case XMLPnPSchemaVersion.V202002:
                    pnp = XMLConstants.PROVISIONING_SCHEMA_NAMESPACE_2020_02;
                    break;
                case XMLPnPSchemaVersion.V202103:
                    pnp = XMLConstants.PROVISIONING_SCHEMA_NAMESPACE_2021_03;
                    break;
                default:
                    pnp = XMLConstants.PROVISIONING_SCHEMA_NAMESPACE_2021_03;
                    break;
            }

            var filesElement = xElement.Descendants(pnp + "Files").FirstOrDefault();
            if (filesElement != null)
            {
                xml = filesElement.ToString();
            }
            return xml;
        }

        private string GetFiles(XMLPnPSchemaVersion schema, string folder, string ctid)
        {
            ProvisioningTemplate template = new ProvisioningTemplate
            {
                Id = "FOLDEREXPORT",
                Security = null,
                Features = null,
                ComposedLook = null
            };

            template.Files.AddRange(EnumerateFiles(folder, ctid, Properties));

            var formatter = ProvisioningHelper.GetFormatter(schema);
            var outputStream = formatter.ToFormattedTemplate(template);
            StreamReader reader = new StreamReader(outputStream);

            return reader.ReadToEnd();
        }

        private List<PnP.Framework.Provisioning.Model.File> EnumerateFiles(string folder, string ctid, Hashtable properties)
        {
            var files = new List<PnP.Framework.Provisioning.Model.File>();

            DirectoryInfo dirInfo = new DirectoryInfo(folder);

            foreach (var directory in dirInfo.GetDirectories().Where(d => (d.Attributes & FileAttributes.Hidden) == 0))
            {
                files.AddRange(EnumerateFiles(directory.FullName, ctid, properties));
            }

            var fileInfo = dirInfo.GetFiles(Match);
            foreach (var file in fileInfo.Where(f => (f.Attributes & FileAttributes.Hidden) == 0))
            {
                var unrootedPath = file.FullName.Substring(Folder.Length);
                var targetFolder = Path.Combine(TargetFolder, unrootedPath.LastIndexOf("\\") > -1 ? unrootedPath.Substring(0, unrootedPath.LastIndexOf("\\")) : "");
                targetFolder = targetFolder.Replace('\\', '/');
                var modelFile = new PnP.Framework.Provisioning.Model.File()
                {
                    Folder = targetFolder,
                    Overwrite = true,
                    Src = unrootedPath,
                };
                if (ctid != null)
                {
                    modelFile.Properties.Add("ContentTypeId", ctid);
                }
                if (properties != null && properties.Count > 0)
                {
                    foreach (var key in properties.Keys)
                    {
                        modelFile.Properties.Add(key.ToString(), properties[key].ToString());
                    }
                }
                modelFile.Security = null;
                files.Add(modelFile);
            }

            return files;
        }
    }
}