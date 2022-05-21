using System.Collections.Generic;

namespace PnP.PowerShell.Commands.Model
{
    public class Microsoft365GroupSettingValueCollection
    {
        public List<Microsoft365GroupSetting> Value { get; set; }
    }
    
    public class Microsoft365GroupSetting
    {
        public string Id { get; set; }
        public string TemplateId { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public List<Microsoft365GroupSettingItemValues> Values { get; set; }
    }

    public class Microsoft365GroupSettingItemValues
    {
        public string Name { get; set; }
        public object Value { get; set; }
    }
}