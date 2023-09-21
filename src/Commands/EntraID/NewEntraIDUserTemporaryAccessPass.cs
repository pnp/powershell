using PnP.Framework.Graph;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.EntraID
{
    [Cmdlet(VerbsCommon.New, "PnPEntraIDUserTemporaryAccessPass")]
    [RequiredMinimalApiPermissions("UserAuthenticationMethod.ReadWrite.All")]
    [Alias("New-PnPAzureADUserTemporaryAccessPass")]
    public class NewEntraIDUserTemporaryAccessPass : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public EntraIDUserPipeBind Identity;

        [Parameter(Mandatory = false)]
        public DateTime? StartDateTime;

        [Parameter(Mandatory = false)]
        public int? LifeTimeInMinutes;

        [Parameter(Mandatory = false)]
        public SwitchParameter IsUsableOnce;

        protected override void ExecuteCmdlet()
        {
            var accessPass = UsersUtility.RequestTemporaryAccessPass(
                                accessToken: AccessToken,
                                userId: Identity.User?.Id?.ToString() ?? Identity.Upn ?? Identity.UserId,
                                startDateTime: StartDateTime,
                                lifeTimeInMinutes: LifeTimeInMinutes,
                                isUsableOnce: IsUsableOnce);

            WriteObject(accessPass);
        }
    }
}