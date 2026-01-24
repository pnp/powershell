namespace PnP.PowerShell.Commands.Enums
{
    /// <summary>
    /// Defines all the built in SharePoint Online Site Template types. See https://learn.microsoft.com/powershell/module/sharepoint-online/set-spobuiltinsitetemplatesettings?view=sharepoint-ps#description
    /// </summary>
    public enum BuiltInSiteTemplates
    {
        /// <summary>
        /// All templates	00000000-0000-0000-0000-000000000000
        /// </summary>
        All,

        #region Teamsites

        /// <summary>
        /// Event planning	9522236e-6802-4972-a10d-e98dc74b3344
        /// </summary>
        EventPlanning,

        /// <summary>
        /// Project management	f0a3abf4-afe8-4409-b7f3-484113dee93e
        /// </summary>
        ProjectManagement,

        /// <summary>
        /// Training and courses	695e52c9-8af7-4bd3-b7a5-46aca95e1c7e
        /// </summary>
        TrainingAndCourses,

        /// <summary>
        /// Training and development team	64aaa31e-7a1e-4337-b646-0b700aa9a52c
        /// </summary>
        TrainingAndDevelopmentTeam,

        /// <summary>
        /// Retail Management	e4ec393e-da09-4816-b6b2-195393656edd
        /// </summary>
        RetailManagement,

        /// <summary>
        /// Employee onboarding team	af9037eb-09ef-4217-80fe-465d37511b33
        /// </summary>
        EmployeeOnboardingTeam,

        /// <summary>
        /// Set up your home page	33537eba-a7d6-4d76-96cc-ee1930bd3907
        /// </summary>
        SetUpYourHomePage,

        /// <summary>
        /// Crisis communication team	fb513aef-c06f-4dc3-b08c-963a2d2360c1
        /// </summary>
        CrisisCommunicationTeam,

        /// <summary>
        /// IT help desk	71308406-f31d-445f-85c7-b31942d1508c
        /// </summary>
        ITHelpDesk,

        /// <summary>
        /// Contracts management	2a7dd756-75f6-4f0f-a06a-a672939ea2a3
        /// </summary>
        ContractsManagement,

        /// <summary>
        /// Accounts payable	403ffe4e-12d4-41a2-8153-208069eaf2b8
        /// </summary>
        AccountsPayable,

        /// <summary>
        /// Standard team	c8b3137a-ca4c-48a9-b356-a8e7987dd693
        /// </summary>
        StandardTeam,

        #endregion

        #region Communication sites

        /// <summary>
        /// Crisis management	951190b8-8541-4f8c-8e8a-10a17c466c94
        /// </summary>
        CrisisManagement,

        /// <summary>
        /// Department	73495f08-0140-499b-8927-dd26a546f26a
        /// </summary>
        Department,

        /// <summary>
        /// Leadership connection	cd4c26b2-b231-419a-8bb4-9b1d9b83aef6
        /// </summary>
        LeadershipConnection,

        /// <summary>
        /// Learning central	b8ef3134-92a2-4c9d-bca6-c2f14e79fe98
        /// </summary>
        LearningCentral,

        /// <summary>
        /// New employee onboarding	2a23fa44-52b0-4814-baba-06fef1ab931e
        /// </summary>
        NewEmployeeOnboarding,

        /// <summary>
        /// Showcase	6142d2a0-63a5-4ba0-aede-d9fefca2c767
        /// </summary>
        Showcase,

        /// <summary>
        /// Store Collaboration	811ecf9a-b33f-44e6-81bd-da77729906dc
        /// </summary>
        StoreCollaboration,

        /// <summary>
        /// Volunteer center	34a39504-194c-4605-87be-d48d00070c67
        /// </summary>
        VolunteerCenter,

        /// <summary>
        /// Blank	f6cc5403-0d63-442e-96c0-285923709ffc
        /// </summary>
        Blank,

        /// <summary>
        /// Brand central	f2c6bb0c-9234-40c2-9ec3-ee86a70330fb
        /// </summary>
        BrandCentral,

        /// <summary>
        /// Standard communication	96c933ac-3698-44c7-9f4a-5fd17d71af9e
        /// </summary>
        StandardCommunication,

        /// <summary>
        /// Event	3d5ef50b-88a0-42a7-9fb2-8036009f6f42
        /// </summary>
        Event,

        /// <summary>
        /// Human resources	c298ddc9-628d-48bf-b1e5-5939a1962fb1
        /// </summary>
        HumanResources,

        /// <summary>
        /// Organization home	30eebaf6-48ea-4af9-a564-a5c50297c826
        /// </summary>
        OrganizationHome,

        #endregion

        #region 0
        /// <summary>
        /// Copilot Campaign	94e24f52-dfaf-40e4-b629-df2c85570adc
        /// </summary>
        CopilotCampaign,

        /// <summary>
        /// Viva Campaign	da99c5d9-baad-4e81-81f6-03a061972d49
        /// </summary>
        VivaCampaign
        #endregion
    }
}
