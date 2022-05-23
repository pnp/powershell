
using System.IdentityModel.Tokens.Jwt;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Base
{
    [Cmdlet(VerbsCommon.Get, "PnPGraphAccessToken", DefaultParameterSetName = ParameterSet_Encoded)]
    [OutputType(typeof(string), ParameterSetName = new[] { ParameterSet_Encoded })]
    [OutputType(typeof(JwtSecurityToken), ParameterSetName = new[] { ParameterSet_Decoded })]
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
                WriteObject(new JwtSecurityToken(AccessToken));
            }
            else
            {
                WriteObject(AccessToken);
            }
        }
    }
}