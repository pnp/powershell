using System;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model
{
    public class CopyJobLog
    {
        public string Event { get; set; }
        public string JobId { get; set; }
        public string Time { get; set; }
        public string TotalRetryCount { get; set; }
        public string MigrationCopy { get; set; }
        public string MigrationDirection { get; set; }
        public string ObjectType { get; set; }
        public string Url { get; set; }
        public Guid Id { get; set; }

        [JsonPropertyName("SourceListItemIntId")]
        public string ItemId { get; set; }
        public string ErrorCode { get; set; }
        public string Message { get; set; }
        public Guid CorrelationId { get; set; }
    }
}