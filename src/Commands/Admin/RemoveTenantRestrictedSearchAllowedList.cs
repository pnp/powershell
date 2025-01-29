using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using System.Collections.Generic;
using System.Management.Automation;
using System.Linq;
using System;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Remove, "PnPTenantRestrictedSearchAllowedList", DefaultParameterSetName = ParameterSet_SiteList)]
    public class RemoveTenantRestrictedSearchAllowedList : PnPSharePointOnlineAdminCmdlet
    {
        private const string ParameterSet_SiteList = "SiteList";
        private const string ParameterSet_File = "File";

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_SiteList)]
        public string[] SitesList;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_File)]
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

            if(_sitelist.Count > 100)
            {
                WriteWarning($"The maximum number of sites that can be added to the allowed list is 100. You have specified {_sitelist.Count} sites. Will try to add them anyway.");
            }

            Tenant.RemoveSPORestrictedSearchAllowedList(_sitelist);
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
                if (line.Contains(','))
                {
                    throw new InvalidOperationException("File should only contain one column and no commas");
                }
            }

            return lines.ToList();
        }
    }
}