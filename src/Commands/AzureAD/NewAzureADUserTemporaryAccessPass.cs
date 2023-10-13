using PnP.Framework.Graph;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Graph
{
    [Cmdlet(VerbsCommon.New, "PnPAzureADUserTemporaryAccessPass")]
    [RequiredMinimalApiPermissions("UserAuthenticationMethod.ReadWrite.All")]
    public class NewAzureADUserTemporaryAccessPass : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public AzureADUserPipeBind Identity;

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
                                isUsableOnce: IsUsableOnce, azureEnvironment: Connection.AzureEnvironment);

            WriteObject(accessPass);
        }
    }
}