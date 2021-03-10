namespace PnP.PowerShell.Commands.Model.AzureAD
{
    public class AzureADGroupUser
    {
        public string UserPrincipalName
        {
            get;
            set;
        }

        public string DisplayName
        {
            get;
            set;
        }

        internal static AzureADGroupUser CreateFrom(PnP.Framework.Entities.GroupUser entity)
        {
            var o = new AzureADGroupUser
            {
                UserPrincipalName = entity.UserPrincipalName,
                DisplayName = entity.DisplayName
            };
            return o;
        }
    }
}