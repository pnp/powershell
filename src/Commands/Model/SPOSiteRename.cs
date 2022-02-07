using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model
{
    public class SPOSiteRenameJob
    {
        public string JobState { get; set; }

        public Guid SiteId { get; set; }

        public Guid JobId { get; set; }

        public Guid ParentId { get; set; }

        public string TriggeredBy { get; set; }

        public int ErrorCode { get; set; }

        public string ErrorDescription { get; set; }

        public string SourceSiteUrl { get; set; }

        public string TargetSiteUrl { get; set; }

        public object TargetSiteTitle { get; set; }

        public int Option { get; set; }

        public object Reserve { get; set; }

        public object SkipGestures { get; set; }        

    }
}
