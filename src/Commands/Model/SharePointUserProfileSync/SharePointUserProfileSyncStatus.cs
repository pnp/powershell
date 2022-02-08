using System;
using Microsoft.Online.SharePoint.TenantManagement;
using PnP.PowerShell.Commands.Enums;

namespace PnP.PowerShell.Commands.Model.SharePoint.SharePointUserProfileSync
{
    /// <summary>
    /// Contains the status of a SharePoint Online User Profile Import job
    /// </summary>
    public class SharePointUserProfileSyncStatus
    {
        #region Properties

        /// <summary>
        /// Details on the type of error that occurred, if any
        /// </summary>
        public SharePointUserProfileImportProfilePropertiesJobError Error { get; set; }

        /// <summary>
        /// The error message, if an error occurred
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Unique identifier of the import job
        /// </summary>
        public Guid? JobId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LogFolderUri { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SourceUri { get; set; }

        /// <summary>
        /// State the user profile import process is in
        /// </summary>
        public SharePointUserProfileImportProfilePropertiesJobState State { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Takes an instance of ImportProfilePropertiesJobInfo from CSOM and maps it to a local SharePointUserProfileSyncStatus entity
        /// </summary>
        /// <param name="importProfilePropertiesJobInfo">Instance to map from</param>
        public static SharePointUserProfileSyncStatus ParseFromImportProfilePropertiesJobInfo(ImportProfilePropertiesJobInfo importProfilePropertiesJobInfo)
        {
            var result = new SharePointUserProfileSyncStatus
            {
                Error = Enum.TryParse(importProfilePropertiesJobInfo.Error.ToString(), out SharePointUserProfileImportProfilePropertiesJobError sharePointUserProfileImportProfilePropertiesJobError) ? sharePointUserProfileImportProfilePropertiesJobError : SharePointUserProfileImportProfilePropertiesJobError.NoError,
                ErrorMessage = importProfilePropertiesJobInfo.ErrorMessage,
                JobId = importProfilePropertiesJobInfo.JobId,
                LogFolderUri = importProfilePropertiesJobInfo.LogFolderUri,
                SourceUri = importProfilePropertiesJobInfo.SourceUri,
                State = Enum.TryParse(importProfilePropertiesJobInfo.State.ToString(), out SharePointUserProfileImportProfilePropertiesJobState sharePointUserProfileImportProfilePropertiesJobState) ? sharePointUserProfileImportProfilePropertiesJobState : SharePointUserProfileImportProfilePropertiesJobState.Unknown
            };
            return result;
        }

        #endregion
    }
}