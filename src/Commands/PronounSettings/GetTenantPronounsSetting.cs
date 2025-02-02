﻿using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.PronounSettings
{
    [Cmdlet(VerbsCommon.Get, "PnPTenantPronounsSetting")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/PeopleSettings.Read.All")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/PeopleSettings.ReadWrite.All")]
    [OutputType(typeof(Model.Graph.PronounsSettings))]
    public class GetTenantPronounsSetting : PnPGraphCmdlet
    {
        protected override void ExecuteCmdlet()
        {
            var pronouns = GraphRequestHelper.Get<Model.Graph.PronounsSettings>("/v1.0/admin/people/pronouns");
            WriteObject(pronouns, false);
        }
    }
}
