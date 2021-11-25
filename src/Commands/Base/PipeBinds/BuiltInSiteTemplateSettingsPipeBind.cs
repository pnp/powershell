using PnP.PowerShell.Commands.Enums;
using PnP.PowerShell.Commands.Model.SharePoint;
using System;
using System.Linq;

namespace PnP.PowerShell.Commands.Base.PipeBinds
{
    public sealed class BuiltInSiteTemplateSettingsPipeBind
    {
        private readonly Guid? _id;

        public BuiltInSiteTemplateSettingsPipeBind(Guid guid)
        {
            _id = guid;
        }

        public BuiltInSiteTemplateSettingsPipeBind(BuiltInSiteTemplates builtInSiteTemplate)
        {
            _id = BuiltInSiteTemplateSettings.BuiltInSiteTemplateMappings.FirstOrDefault(tm => tm.Value == builtInSiteTemplate).Key;
        }

        public BuiltInSiteTemplateSettingsPipeBind(string id)
        {
            if(Guid.TryParse(id, out Guid parsedId))
            {
                _id = parsedId;
            }
        }

        public BuiltInSiteTemplateSettingsPipeBind(BuiltInSiteTemplateSettings builtInSiteTemplateSettings)
        {
            _id = builtInSiteTemplateSettings.Id;
        }

        public Guid? Id => _id;
    }
}