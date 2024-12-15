using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Enums;
using PnP.PowerShell.Commands.Model.AzureAD;
using PnP.PowerShell.Commands.Utilities;
using System;
using System.Collections.Generic;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Apps
{
    [Cmdlet(VerbsCommon.Get, "PnPAzureADServicePrincipal", DefaultParameterSetName = ParameterSet_ALL)]
    [RequiredApiDelegatedOrApplicationPermissions("graph/Application.Read.All")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/Application.ReadWrite.All")]
    [OutputType(typeof(List<AzureADServicePrincipal>))]
    [Alias("Get-PnPEntraIDServicePrincipal")]
    public class GetAzureADServicePrincipal : PnPGraphCmdlet
    {
        private const string ParameterSet_ALL = "All";
        private const string ParameterSet_BYAPPID = "By App Id";
        private const string ParameterSet_BYOBJECTID = "By Object Id";
        private const string ParameterSet_BYAPPNAME = "By App Name";
        private const string ParameterSet_BYBUILTINTYPE = "By built in type";

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_BYAPPID)]
        public Guid AppId;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_BYOBJECTID)]
        public Guid ObjectId;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_BYAPPNAME)]
        public string AppName;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_BYBUILTINTYPE)]
        public ServicePrincipalBuiltInType BuiltInType;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ALL)]
        public string Filter;

        protected override void ExecuteCmdlet()
        {
            AzureADServicePrincipal servicePrincipal = null;
            switch (ParameterSetName)
            {
                case ParameterSet_BYAPPID:
                    servicePrincipal = ServicePrincipalUtility.GetServicePrincipalByAppId(RequestHelper, AppId);
                    break;
                case ParameterSet_BYOBJECTID:
                    servicePrincipal = ServicePrincipalUtility.GetServicePrincipalByObjectId(RequestHelper, ObjectId);
                    break;
                case ParameterSet_BYAPPNAME:
                    servicePrincipal = ServicePrincipalUtility.GetServicePrincipalByAppName(RequestHelper, AppName);
                    break;
                case ParameterSet_BYBUILTINTYPE:
                    servicePrincipal = ServicePrincipalUtility.GetServicePrincipalByBuiltInType(RequestHelper, BuiltInType);
                    break;
                case ParameterSet_ALL:
                    var servicePrincipals = ServicePrincipalUtility.GetServicePrincipals(RequestHelper, Filter);
                    WriteObject(servicePrincipals, true);
                    return;
            }

            if (servicePrincipal == null)
            {
                throw new PSArgumentException("Service principal not found");
            }

            WriteObject(servicePrincipal);
        }
    }
}