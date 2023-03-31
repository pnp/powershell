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
        /// Team collaboration	6b96e7b1-035f-430b-92ca-31511c51ca72
        /// </summary>
        TeamCollaboration,

        /// <summary>
        /// Retail Management	e4ec393e-da09-4816-b6b2-195393656edd
        /// </summary>
        RetailManagement,

        #endregion

        #region Communication sites

        /// <summary>
        /// Crisis management	905bb0b4-01e8-4f55-b73c-f07f08aee3a4
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
        /// Showcase	89f21161-0892-497a-91cb-5783eeb1f5f2
        /// </summary>
        Showcase,

        /// <summary>
        /// Healthcare	5215c092-152f-4912-a12a-7e1efdcc6878
        /// </summary>
        Healthcare,

        /// <summary>
        /// Store Collaboration	811ecf9a-b33f-44e6-81bd-da77729906dc
        /// </summary>
        StoreCollaboration,

        /// <summary>
        /// Volunteer center	b6e04a41-1535-4313-a856-6f3515d31999
        /// </summary>
        VolunteerCenter,
	
        /// <summary>
        /// Topic	a30fef54-a4e5-4beb-a8b5-962c528d753a
        /// </summary>
        Topic,

        /// <summary>
        /// Blank	665da395-e0f9-4c92-b35c-773d8c292f2d
        /// </summary>
        Blank

        #endregion
    }
}