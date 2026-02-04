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
            { new Guid("e4ec393e-da09-4816-b6b2-195393656edd"), BuiltInSiteTemplates.RetailManagement },
            { new Guid("951190b8-8541-4f8c-8e8a-10a17c466c94"), BuiltInSiteTemplates.CrisisManagement },
            { new Guid("73495f08-0140-499b-8927-dd26a546f26a"), BuiltInSiteTemplates.Department },
            { new Guid("cd4c26b2-b231-419a-8bb4-9b1d9b83aef6"), BuiltInSiteTemplates.LeadershipConnection },
            { new Guid("b8ef3134-92a2-4c9d-bca6-c2f14e79fe98"), BuiltInSiteTemplates.LearningCentral },
            { new Guid("2a23fa44-52b0-4814-baba-06fef1ab931e"), BuiltInSiteTemplates.NewEmployeeOnboarding },
            { new Guid("6142d2a0-63a5-4ba0-aede-d9fefca2c767"), BuiltInSiteTemplates.Showcase },
            { new Guid("811ecf9a-b33f-44e6-81bd-da77729906dc"), BuiltInSiteTemplates.StoreCollaboration },
            { new Guid("34a39504-194c-4605-87be-d48d00070c67"), BuiltInSiteTemplates.VolunteerCenter },
            { new Guid("f6cc5403-0d63-442e-96c0-285923709ffc"), BuiltInSiteTemplates.Blank },
            { new Guid("af9037eb-09ef-4217-80fe-465d37511b33"), BuiltInSiteTemplates.EmployeeOnboardingTeam },
            { new Guid("33537eba-a7d6-4d76-96cc-ee1930bd3907"), BuiltInSiteTemplates.SetUpYourHomePage },
            { new Guid("fb513aef-c06f-4dc3-b08c-963a2d2360c1"), BuiltInSiteTemplates.CrisisCommunicationTeam },
            { new Guid("71308406-f31d-445f-85c7-b31942d1508c"), BuiltInSiteTemplates.ITHelpDesk },
            { new Guid("2a7dd756-75f6-4f0f-a06a-a672939ea2a3"), BuiltInSiteTemplates.ContractsManagement },
            { new Guid("403ffe4e-12d4-41a2-8153-208069eaf2b8"), BuiltInSiteTemplates.AccountsPayable },
            { new Guid("f2c6bb0c-9234-40c2-9ec3-ee86a70330fb"), BuiltInSiteTemplates.BrandCentral },
            { new Guid("c8b3137a-ca4c-48a9-b356-a8e7987dd693"), BuiltInSiteTemplates.StandardTeam },
            { new Guid("96c933ac-3698-44c7-9f4a-5fd17d71af9e"), BuiltInSiteTemplates.StandardCommunication },
            { new Guid("3d5ef50b-88a0-42a7-9fb2-8036009f6f42"), BuiltInSiteTemplates.Event },
            { new Guid("c298ddc9-628d-48bf-b1e5-5939a1962fb1"), BuiltInSiteTemplates.HumanResources },
            { new Guid("30eebaf6-48ea-4af9-a564-a5c50297c826"), BuiltInSiteTemplates.OrganizationHome },
            { new Guid("94e24f52-dfaf-40e4-b629-df2c85570adc"), BuiltInSiteTemplates.CopilotCampaign },
            { new Guid("da99c5d9-baad-4e81-81f6-03a061972d49"), BuiltInSiteTemplates.VivaCampaign },
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