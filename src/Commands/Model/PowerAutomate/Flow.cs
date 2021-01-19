using System;
using System.Collections.Generic;

namespace PnP.PowerShell.Commands.Model.PowerAutomate
{
    public class Flow
    {
        public string Name { get; set;}
        public string Id { get; set; }
        public string Type { get; set; }
        public FlowProperties Properties {get;set;}
    }

    public class FlowProperties
    {
        public string ApiId { get; set;}
        public string DisplayName { get; set; }
        public string UserType { get; set; }
        public string State { get; set; }
        public DateTime CreatedTime { get; set; }
        public Dictionary<string, string> Environment { get; set; }
    }
}