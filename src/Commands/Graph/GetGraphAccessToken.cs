using System.Management.Automation;

namespace PnP.PowerShell.Commands.Base
{
    [Cmdlet(VerbsCommon.Get, "PnPGraphAccessToken", DefaultParameterSetName = ParameterSet_Encoded)]
    [OutputType(typeof(string), ParameterSetName = new[] { ParameterSet_Encoded })]
    [OutputType(typeof(Microsoft.IdentityModel.JsonWebTokens.JsonWebToken), ParameterSetName = new[] { ParameterSet_Decoded })]
    public class GetGraphAccessToken : PnPGraphCmdlet
    {
        public const string ParameterSet_Decoded = "Decoded (JwtSecurityToken)";
        public const string ParameterSet_Encoded = "Encoded (string)";

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_Decoded)]
        public SwitchParameter Decoded;

        protected override void ExecuteCmdlet()
        {
            if (Decoded.IsPresent)
            {
                WriteObject(new Microsoft.IdentityModel.JsonWebTokens.JsonWebToken(AccessToken));
            }
            else
            {
                WriteObject(AccessToken);
            }
        }
    }
}