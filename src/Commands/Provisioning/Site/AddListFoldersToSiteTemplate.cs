﻿using PnP.Framework.Provisioning.Connectors;
using PnP.Framework.Provisioning.Model;
using PnP.Framework.Provisioning.Providers;
using PnP.Framework.Provisioning.Providers.Xml;
using PnP.Framework.AppModelExtensions;

using PnP.PowerShell.Commands.Base.PipeBinds;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Utilities;

namespace PnP.PowerShell.Commands.Provisioning.Site
{
    [Cmdlet(VerbsCommon.Add, "PnPListFoldersToSiteTemplate")]
    public class AddListFoldersToSiteTemplate : PnPWebCmdlet
    {

        [Parameter(Mandatory = true, Position = 0)]
        public string Path;

        [Parameter(Mandatory = true, Position = 2)]
        public ListPipeBind List;

        [Alias("Recurse")]
        [Parameter(Mandatory = false, Position = 4)]
        public SwitchParameter Recursive;

        [Parameter(Mandatory = false, Position = 5)]
        public SwitchParameter IncludeSecurity;

        [Parameter(Mandatory = false, Position = 6)]
        public ITemplateProviderExtension[] TemplateProviderExtensions;


        protected override void ExecuteCmdlet()
        {
            if (!System.IO.Path.IsPathRooted(Path))
            {
                Path = System.IO.Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, Path);
            }
            // Load the template
            var template = ProvisioningHelper.LoadSiteTemplateFromFile(Path, TemplateProviderExtensions, (e) =>
                {
                    WriteError(new ErrorRecord(e, "TEMPLATENOTVALID", ErrorCategory.SyntaxError, null));
                });

            if (template == null)
            {
                throw new ApplicationException("Invalid template file.");
            }


            List spList = List.GetList(CurrentWeb);
            ClientContext.Load(spList, l => l.RootFolder, l => l.HasUniqueRoleAssignments);
            ClientContext.ExecuteQueryRetry();

            var tokenParser = new Framework.Provisioning.ObjectHandlers.TokenParser(ClientContext.Web, template);

            //We will remove a list if it's found so we can get the list
            ListInstance listInstance = template.Lists.Find(l => tokenParser.ParseString(l.Title) == spList.Title);

            if (listInstance == null)
            {
                throw new ApplicationException("List does not exist in the template file.");
            }


            Microsoft.SharePoint.Client.Folder listFolder = spList.RootFolder;
            ClientContext.Load(listFolder);
            ClientContext.ExecuteQueryRetry();

            IList<PnP.Framework.Provisioning.Model.Folder> folders = GetChildFolders(listFolder);

            template.Lists.Remove(listInstance);
            listInstance.Folders.AddRange(folders);
            template.Lists.Add(listInstance);

            // Determine the output file name and path
            var outFileName = System.IO.Path.GetFileName(Path);
            var outPath = new FileInfo(Path).DirectoryName;

            var fileSystemConnector = new FileSystemConnector(outPath, "");
            var formatter = XMLPnPSchemaFormatter.LatestFormatter;
            var extension = new FileInfo(Path).Extension.ToLowerInvariant();
            if (extension == ".pnp")
            {
                XMLTemplateProvider provider = new XMLOpenXMLTemplateProvider(new OpenXMLConnector(Path, fileSystemConnector));
                var templateFileName = outFileName.Substring(0, outFileName.LastIndexOf(".", StringComparison.Ordinal)) + ".xml";

                provider.SaveAs(template, templateFileName, formatter, TemplateProviderExtensions);
            }
            else
            {
                XMLTemplateProvider provider = new XMLFileSystemTemplateProvider(Path, "");
                provider.SaveAs(template, Path, formatter, TemplateProviderExtensions);
            }
        }

        private IList<PnP.Framework.Provisioning.Model.Folder> GetChildFolders(Microsoft.SharePoint.Client.Folder listFolder)
        {
            List<PnP.Framework.Provisioning.Model.Folder> retFolders = new List<PnP.Framework.Provisioning.Model.Folder>();
            ClientContext.Load(listFolder, l => l.Name, l => l.Folders);
            ClientContext.ExecuteQueryRetry();
            var folders = listFolder.Folders;
            ClientContext.Load(folders, fl => fl.Include(f => f.Name, f => f.ServerRelativeUrl, f => f.ListItemAllFields));
            ClientContext.ExecuteQueryRetry();
            foreach (var folder in folders)
            {
                if (folder.ListItemAllFields.ServerObjectIsNull != null && !folder.ListItemAllFields.ServerObjectIsNull.Value)
                {
                    var retFolder = GetFolder(folder);
                    retFolders.Add(retFolder);
                }
            }
            return retFolders;
        }

        private PnP.Framework.Provisioning.Model.Folder GetFolder(Microsoft.SharePoint.Client.Folder listFolder)
        {
            ListItem folderItem = listFolder.ListItemAllFields;
            ClientContext.Load(folderItem, fI => fI.HasUniqueRoleAssignments);
            ClientContext.Load(listFolder, l => l.Name, l => l.Folders);
            ClientContext.ExecuteQueryRetry();

            PnP.Framework.Provisioning.Model.Folder retFolder = new PnP.Framework.Provisioning.Model.Folder
            {
                Name = listFolder.Name
            };

            if (Recursive)
            {
                foreach (var folder in listFolder.Folders)
                {
                    var childFolder = GetFolder(folder);
                    retFolder.Folders.Add(childFolder);
                }
            }
            if (IncludeSecurity && folderItem.ServerObjectIsNull != null && !folderItem.ServerObjectIsNull.Value && folderItem.HasUniqueRoleAssignments)
            {
                var RoleAssignments = folderItem.RoleAssignments;
                ClientContext.Load(RoleAssignments);
                ClientContext.ExecuteQueryRetry();

                retFolder.Security.ClearSubscopes = true;
                retFolder.Security.CopyRoleAssignments = false;

                ClientContext.Load(RoleAssignments, r => r.Include(a => a.Member.LoginName, a => a.Member, a => a.RoleDefinitionBindings));
                ClientContext.ExecuteQueryRetry();

                foreach (var roleAssignment in RoleAssignments)
                {
                    var principalName = roleAssignment.Member.LoginName;
                    var roleBindings = roleAssignment.RoleDefinitionBindings;
                    foreach (var roleBinding in roleBindings)
                    {
                        if (roleBinding.Name == "Limited Access")
                        {
                            continue;
                        }
                        retFolder.Security.RoleAssignments.Add(new PnP.Framework.Provisioning.Model.RoleAssignment() { Principal = principalName, RoleDefinition = roleBinding.Name });
                    }
                }
            }

            return retFolder;
        }



    }
}
