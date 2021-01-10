using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Management.Automation;
using Microsoft.Online.SharePoint.TenantManagement;
using Microsoft.SharePoint.Client;

using PnP.PowerShell.Commands.Base;
using PnP.Framework.Utilities;

namespace PnP.PowerShell.Commands.UserProfiles
{
    [Cmdlet(VerbsCommon.New, "PnPUPABulkImportJob")]
    public class NewUPABulkImportJob : PnPAdminCmdlet
    {
        [Parameter(Mandatory = true, Position = 0)]
        public string Folder;

        [Parameter(Mandatory = true, Position = 1)]
        public string Path = string.Empty;

        [Parameter(Mandatory = true, Position = 2)]
        public Hashtable UserProfilePropertyMapping;

        [Parameter(Mandatory = true, Position = 3)]
        public string IdProperty;

        [Parameter(Mandatory = false, Position = 4)]
        public ImportProfilePropertiesUserIdType IdType = ImportProfilePropertiesUserIdType.Email;

        protected override void ExecuteCmdlet()
        {
            if (string.IsNullOrWhiteSpace(Path))
            {
                throw new InvalidEnumArgumentException(@"Path cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(IdProperty))
            {
                throw new InvalidEnumArgumentException(@"IdProperty cannot be empty.");
            }
            
            var webCtx = ClientContext.Clone(PnPConnection.CurrentConnection.Url);
            var web = webCtx.Web;
            var webServerRelativeUrl = web.EnsureProperty(w => w.ServerRelativeUrl);
            if (!Folder.ToLower().StartsWith(webServerRelativeUrl))
            {
                Folder = UrlUtility.Combine(webServerRelativeUrl, Folder);
            }
            if (!web.DoesFolderExists(Folder))
            {
                throw new InvalidOperationException($"Folder {Folder} does not exist.");
            }
            var folder = web.GetFolderByServerRelativeUrl(Folder);

            var fileName = System.IO.Path.GetFileName(Path);
            File file = folder.UploadFile(fileName, Path, true);

            
            var o365 = new Office365Tenant(ClientContext);
            var propDictionary = UserProfilePropertyMapping.Cast<DictionaryEntry>().ToDictionary(kvp => (string)kvp.Key, kvp => (string)kvp.Value);
            var url = new Uri(webCtx.Url).GetLeftPart(UriPartial.Authority) + file.ServerRelativeUrl;
            var id = o365.QueueImportProfileProperties(IdType, IdProperty, propDictionary, url);
            ClientContext.ExecuteQueryRetry();

            var job = o365.GetImportProfilePropertyJob(id.Value);
            ClientContext.Load(job);
            ClientContext.ExecuteQueryRetry();
            WriteObject(job);
        }
    }
}