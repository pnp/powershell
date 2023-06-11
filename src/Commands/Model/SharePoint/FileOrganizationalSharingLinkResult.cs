using PnP.Core.Model.Security;
using System.Collections.Generic;

namespace PnP.PowerShell.Commands.Model.SharePoint
{
    public class FileSharingLinkResult
    {
        public string Id;

        public ISharingLink Link;

        public List<PermissionRole> Roles;

        public string WebUrl;
    }
}
