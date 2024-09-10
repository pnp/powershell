using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using System.Collections.Generic;
using System.Management.Automation;
using System.Linq;
using System;

namespace PnP.PowerShell.Commands.Files
{
    [Cmdlet(VerbsCommon.Add, "PnPTenantRestrictedSearchAllowedList", DefaultParameterSetName = ParameterSet_SiteList)]
    public class AddTenantRestrictedSearchAllowedList : PnPAdminCmdlet
    {
        private const string ParameterSet_SiteList = "SiteList";
        private const string ParameterSet_File = "File";

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SiteList)]
        public string[] SitesList;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_File)]
        public string SitesListFileUrl;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_File)]
        public SwitchParameter ContainsHeader;

        protected override void ExecuteCmdlet()
        {
            IList<string> _sitelist = null;
            if (ParameterSetName == ParameterSet_File)
            {
                _sitelist = ReadFileContents();
            }
            else if (ParameterSetName == ParameterSet_SiteList)
            {
                _sitelist = SitesList;
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

        private IList<string> ReadFileContents()
        {
            var lines = System.IO.File.ReadAllLines(SitesListFileUrl);
            if (ContainsHeader)
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
    }
}