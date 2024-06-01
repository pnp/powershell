using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using System.Collections.Generic;
using System.Management.Automation;
using System.Linq;
using System;

namespace PnP.PowerShell.Commands.Files
{
    [Cmdlet(VerbsCommon.Add, "PnPTenantRestrictedSearchAllowedList")]
    public class AddTenantRestrictedSearchAllowedList : PnPAdminCmdlet,IDynamicParameters
    {
        private const string ParameterSet_file = "file";
        private FileParameters _fileParameters;

        [Parameter(Mandatory = false)]
        public string[] SiteList;

        [Parameter(Mandatory = false)]
        public string SitesListFileUrl;
        public object GetDynamicParameters()
        {
            if (!ParameterSpecified(nameof(SiteList)) && !ParameterSpecified(nameof(SitesListFileUrl)))
            {
                throw new ArgumentException("Parameter set cannot be resolved using the specified named parameters.");
            }
            else if (ParameterSpecified(nameof(SitesListFileUrl)))
            {
                _fileParameters = new FileParameters();
                return _fileParameters;
            }
            
            return null;        
        }
        protected override void ExecuteCmdlet()
        {
            IList<string> _sitelist = null;
            if (_fileParameters != null)
            {
                _sitelist = ReadFileContents();
            }
            else if (SiteList != null)
            {
                _sitelist = SiteList;
            }
            else
            {
                throw new ArgumentException("Parameter set cannot be resolved using the specified named parameters.");
            }
            if (_sitelist == null)
            {
                throw new InvalidOperationException("SiteList cannot be null");
            }

            Tenant.AddSPORestrictedSearchAllowedList(_sitelist);
            AdminContext.ExecuteQueryRetry();
        }

         public IList<string> ReadFileContents()
         {
            var lines = System.IO.File.ReadAllLines(SitesListFileUrl);
            if (_fileParameters.ContainsHeader)
            {
                lines = lines.Skip(1).ToArray();
            }

            foreach (var line in lines)
            {
                var columns = line.Split(',');
                if (columns.Length != 1)
                {
                    throw new InvalidOperationException("File should only contain one column");
                }
            }

            return lines.ToList();
        }
        public class FileParameters
        {
            [Parameter(Mandatory = false,ParameterSetName = ParameterSet_file)]
            public SwitchParameter ContainsHeader;
        }   
    }
}
