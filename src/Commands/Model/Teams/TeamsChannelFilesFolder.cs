using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PnP.PowerShell.Commands.Model.Teams
{
    public partial class TeamsChannelFilesFolder
    {        
        public string id { get; set; }
        public DateTime createdDateTime { get; set; }
        public DateTime lastModifiedDateTime { get; set; }
        public string name { get; set; }
        public string webUrl { get; set; }
        public long size { get; set; }
        public TeamChannelParentReference parentReference { get; set; }
        public TeamChannelFileSystemInfo fileSystemInfo { get; set; }
        public TeamChannelFolder folder { get; set; }
    }

    public partial class TeamChannelParentReference
    {
        public string driveId { get; set; }
        public string driveType { get; set; }
    }

    public partial class TeamChannelFileSystemInfo
    {
        public DateTime createdDateTime { get; set; }
        public DateTime lastModifiedDateTime { get; set; }
    }

    public partial class TeamChannelFolder
    {
        public int childCount { get; set; }
    }
}
