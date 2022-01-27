using System.Collections.Generic;

namespace PnP.PowerShell.Commands.Model
{
    public class Microsoft365GroupTemplateSettingValueCollection
    {
        public List<Microsoft365GroupSettingTemplate> Value { get; set; }
    }
    
    public class Microsoft365GroupSettingTemplate
    {
        public string Id { get; set; }
        public object DeletedDateTime { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public List<Microsoft365GroupSettingItemValues> Values { get; set; }
    }

    public class Microsoft365GroupTemplateSettingItemValues
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string DefaultValue { get; set; }
        public string Description { get; set; }
    }
}