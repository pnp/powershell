using System;
using System.Collections.Generic;

namespace PnP.PowerShell.Commands.Model.PowerAutomate
{
    public class Environment
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public string Type { get; set; }
        public string Id { get; set; }
        public EnvironmentProperties Properties { get; set; }
    }

    public class EnvironmentProperties
    {
        public string DisplayName { get; set; }
        public DateTime CreatedTime { get; set; }
        public Dictionary<string,string> RuntimeEndpoints { get; set; }
    }
}