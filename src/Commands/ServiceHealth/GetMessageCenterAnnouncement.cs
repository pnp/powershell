﻿using System.Management.Automation;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Utilities;

namespace PnP.PowerShell.Commands.ServiceHealth
{
    [Cmdlet(VerbsCommon.Get, "PnPMessageCenterAnnouncement")]
    [RequiredApiApplicationPermissions("graph/ServiceMessage.Read.All")]
    [RequiredApiDelegatedPermissions("graph/ServiceMessage.Read.All")]
    public class GetMessageCenterAnnouncement : PnPGraphCmdlet
    {
        [Parameter(Mandatory = false)]
        public string Identity;

        protected override void ExecuteCmdlet()
        {
            if (ParameterSpecified(nameof(Identity)))
            {
                WriteObject(ServiceHealthUtility.GetServiceUpdateMessageById(RequestHelper, Identity), false);
            }
            else
            {
                WriteObject(ServiceHealthUtility.GetServiceUpdateMessages(RequestHelper), true);
            }
        }
    }
}