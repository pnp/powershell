using System;
using System.Collections.Generic;
using System.Management.Automation;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Enums;
using PnP.PowerShell.Commands.Model.EntraID;
using PnP.PowerShell.Commands.Utilities;

namespace PnP.PowerShell.Commands.Apps
{
    [Cmdlet(VerbsCommon.Get, "PnPEntraIDServicePrincipal", DefaultParameterSetName = ParameterSet_ALL)]
    [RequiredMinimalApiPermissions("Application.Read.All")]
    [OutputType(typeof(List<ServicePrincipal>))]
    [Alias("Get-PnPAzureADServicePrincipal")]
    public class GetEntraIDServicePrincipal : PnPGraphCmdlet
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
            ServicePrincipal servicePrincipal = null;
            switch (ParameterSetName)
            {
                case ParameterSet_BYAPPID:
                    servicePrincipal = ServicePrincipalUtility.GetServicePrincipalByAppId(Connection, AccessToken, AppId);
                    break;
                case ParameterSet_BYOBJECTID:
                    servicePrincipal = ServicePrincipalUtility.GetServicePrincipalByObjectId(Connection, AccessToken, ObjectId);
                    break;
                case ParameterSet_BYAPPNAME:
                    servicePrincipal = ServicePrincipalUtility.GetServicePrincipalByAppName(Connection, AccessToken, AppName);
                    break;
                case ParameterSet_BYBUILTINTYPE:
                    servicePrincipal = ServicePrincipalUtility.GetServicePrincipalByBuiltInType(Connection, AccessToken, BuiltInType);
                    break;
                case ParameterSet_ALL:
                    var servicePrincipals = ServicePrincipalUtility.GetServicePrincipals(Connection, AccessToken, Filter);
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