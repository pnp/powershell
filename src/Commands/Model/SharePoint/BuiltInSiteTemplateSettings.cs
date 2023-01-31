using System;
using PnP.PowerShell.Commands.Enums;
using System.Linq;

namespace PnP.PowerShell.Commands.Model.SharePoint
{
    /// <summary>
    /// All properties of a built in site template setting
    /// </summary>
    public class BuiltInSiteTemplateSettings
    {
        /// <summary>
        /// Contains all mappings between the built in SharePoint Online site templates and their matching GUIDs. See https://learn.microsoft.com/powershell/module/sharepoint-online/set-spobuiltinsitetemplatesettings?view=sharepoint-ps#description
        /// </summary>
        public static System.Collections.Generic.Dictionary<Guid, BuiltInSiteTemplates> BuiltInSiteTemplateMappings = new System.Collections.Generic.Dictionary<Guid, BuiltInSiteTemplates>
        {
            { new Guid("00000000-0000-0000-0000-000000000000"), BuiltInSiteTemplates.All },
            { new Guid("9522236e-6802-4972-a10d-e98dc74b3344"), BuiltInSiteTemplates.EventPlanning },
            { new Guid("f0a3abf4-afe8-4409-b7f3-484113dee93e"), BuiltInSiteTemplates.ProjectManagement },
            { new Guid("695e52c9-8af7-4bd3-b7a5-46aca95e1c7e"), BuiltInSiteTemplates.TrainingAndCourses },
            { new Guid("64aaa31e-7a1e-4337-b646-0b700aa9a52c"), BuiltInSiteTemplates.TrainingAndDevelopmentTeam },
            { new Guid("6b96e7b1-035f-430b-92ca-31511c51ca72"), BuiltInSiteTemplates.TeamCollaboration },
            { new Guid("e4ec393e-da09-4816-b6b2-195393656edd"), BuiltInSiteTemplates.RetailManagement },
            { new Guid("905bb0b4-01e8-4f55-b73c-f07f08aee3a4"), BuiltInSiteTemplates.CrisisManagement },
            { new Guid("73495f08-0140-499b-8927-dd26a546f26a"), BuiltInSiteTemplates.Department },
            { new Guid("cd4c26b2-b231-419a-8bb4-9b1d9b83aef6"), BuiltInSiteTemplates.LeadershipConnection },
            { new Guid("b8ef3134-92a2-4c9d-bca6-c2f14e79fe98"), BuiltInSiteTemplates.LearningCentral },
            { new Guid("2a23fa44-52b0-4814-baba-06fef1ab931e"), BuiltInSiteTemplates.NewEmployeeOnboarding },
            { new Guid("89f21161-0892-497a-91cb-5783eeb1f5f2"), BuiltInSiteTemplates.Showcase },
            { new Guid("5215c092-152f-4912-a12a-7e1efdcc6878"), BuiltInSiteTemplates.Healthcare },
            { new Guid("811ecf9a-b33f-44e6-81bd-da77729906dc"), BuiltInSiteTemplates.StoreCollaboration },
            { new Guid("b6e04a41-1535-4313-a856-6f3515d31999"), BuiltInSiteTemplates.VolunteerCenter },
            { new Guid("a30fef54-a4e5-4beb-a8b5-962c528d753a"), BuiltInSiteTemplates.Topic },
            { new Guid("665da395-e0f9-4c92-b35c-773d8c292f2d"), BuiltInSiteTemplates.Blank }
        };
        
        /// <summary>
        /// Unique identifier of the built in SharePoint Online site template
        /// </summary>
        public Guid? Id { get; set; }

        /// <summary>
        /// Enum of the built in SharePoint Online site template
        /// </summary>
        public BuiltInSiteTemplates? BuiltInSiteTemplate
        { 
            get
            {
                if(!Id.HasValue || !BuiltInSiteTemplateMappings.TryGetValue(Id.Value, out BuiltInSiteTemplates builtInSiteTemplate))
                {
                    return null;
                }
                return builtInSiteTemplate;
            }
            set
            {
                Id = BuiltInSiteTemplateMappings.FirstOrDefault(tm => tm.Value == value).Key;
            } 
        }

        /// <summary>
        /// Boolean indicating if this built in SharePoint Online site template is hidden
        /// </summary>
        public bool? IsHidden { get; set; }
    }
}