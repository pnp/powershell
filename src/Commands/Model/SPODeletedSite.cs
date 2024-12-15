using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.Framework.Diagnostics;
using System;

namespace PnP.PowerShell.Commands.Model
{
	/// <summary>
	/// Contains information about a sitecollection that is residing in the tenant recycle bin
	/// </summary>
	public class SPODeletedSite
	{
		#region Basic properties

		/// <summary>
		/// Unique identifier of the sitecollection
		/// </summary>
		public Guid SiteId { private set; get; }

		/// <summary>
		/// Url of the sitecollection
		/// </summary>
		public string Url { get; set; }

		/// <summary>
		/// Status of recycling
		/// </summary>
		public string Status { private set; get; }

		/// <summary>
		/// Date and time at which this sitecollection was sent to the recycle bin
		/// </summary>
		public DateTime DeletionTime { private set; get; }

		/// <summary>
		/// Amount of days remaining in the recycle bin before it will be deleted permanently
		/// </summary>
		public int DaysRemaining { private set; get; }

		/// <summary>
		/// The maximum amount of data that is allowed to be stored in this sitecollection
		/// </summary>
		public long StorageQuota { private set; get; }

		/// <summary>
		/// The sandboxed solution resource quota points assigned to this sitecollection. This is not being used anymore.
		/// </summary>
		public double ResourceQuota { private set; get; }

		#endregion

		#region Additional details

		// Note: These properties are only fetched if the request is made to fetch additional properties on the site

		/// <summary>
		/// Date and time the sitecollection was last modified
		/// </summary>
		public DateTime? LastModified { private set; get; }

		/// <summary>
		/// Date and time at which the site collection has been created
		/// </summary>
		public DateTime? CreationTime { private set; get; }

		/// <summary>
		/// Date and time at which a list has last been modified within this sitecollection
		/// </summary>
		public DateTime? LastListActivityOn { private set; get; }

		/// <summary>
		/// Date and time at which a list item has last been modified within this sitecollection
		/// </summary>
		public DateTime? LastItemModifiedDate { private set; get; }

		/// <summary>
		/// Date and time at which there last was activity taking place on this sitecollection
		/// </summary>
		public DateTime? LastWebActivityOn { private set; get; }

		/// <summary>
		/// Name of the user having created the sitecollection
		/// </summary>
		public string CreatedBy { private set; get; }

		/// <summary>
		/// Name of the user having deleted the sitecollection
		/// </summary>
		public string DeletedBy { private set; get; }

		/// <summary>
		/// Boolean indicating if this sitecollection can still be restored from the recycle bin
		/// </summary>
		public bool? IsRestorable { private set; get; }

		/// <summary>
		/// The number of files stores within this sitecollection
		/// </summary>
		public long? NumberOfFiles { private set; get; }

		/// <summary>
		/// The e-mail address of the primary sitecollection owner
		/// </summary>
		public string SiteOwnerEmail { private set; get; }

		/// <summary>
		/// The name of the primary sitecollection owner
		/// </summary>
		public string SiteOwnerName { private set; get; }

		/// <summary>
		/// The amount of SharePoint Online storage used in this sitecollection
		/// </summary>
		public double? StorageUsed { private set; get; }

		/// <summary>
		/// The percentage of storage used towards the storage quota assigned to this sitecollection
		/// </summary>
		public double? StorageUsedPercentage { private set; get; }

		/// <summary>
		/// The Id of the template used for creating the sitecollection
		/// </summary>
		public short? TemplateId { private set; get; }

		/// <summary>
		/// The name of the template used for creating the sitecollection
		/// </summary>
		public string TemplateName { private set; get; }

		/// <summary>
		/// The Id of the sensititivy label applied to this sitecollection, if any
		/// </summary>
		public Guid? SensitivityLabelId { private set; get; }

		/// <summary>
		/// The mode for informationbarriers that is applied to this sitecollection
		/// </summary>
		public string InformationBarrierMode { private set; get; }

		#endregion

		/// <summary>
		/// Creates a new <see cref="SPODeletedSite"/> instance based out of a <see cref="DeletedSiteProperties"/> instance
		/// </summary>
		/// <param name="deletedSiteProperties">Instance containing details on a deleted site coming from CSOM</param>
		/// <param name="fetchAdditionalDetails">Boolean indicating if additional details should be fetched on the deleted site</param>
		/// <param name="clientContext">ClientContext that can be used to fetch the additional details. Required if <paramref name="fetchAdditionalDetails"/> is set to true, otherwise can be omitted.</param>
		/// <param name="cmdlet">Cmdlet instance that can be used to provide logging. Optional.</param>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="fetchAdditionalDetails"/> is set to true but no value is provided for <paramref name="clientContext"/></exception>
		internal SPODeletedSite(DeletedSiteProperties deletedSiteProperties, bool fetchAdditionalDetails = false, ClientContext clientContext = null)
		{
			if (fetchAdditionalDetails && clientContext == null)
			{
				throw new ArgumentNullException(nameof(clientContext), "ClientContext is required to be passed in when fetching additional details");
			}

			SiteId = deletedSiteProperties.SiteId;
			Url = deletedSiteProperties.Url;
			DeletionTime = deletedSiteProperties.DeletionTime;
			DaysRemaining = deletedSiteProperties.DaysRemaining;
			Status = deletedSiteProperties.Status;
			StorageQuota = deletedSiteProperties.StorageMaximumLevel;
			ResourceQuota = deletedSiteProperties.UserCodeMaximumLevel;

			if (fetchAdditionalDetails)
			{
				Log.Debug("SPODeletedSite", $"Fetching additional details for {Url}");
				var list = clientContext.Web.Lists.GetByTitle("DO_NOT_DELETE_SPLIST_TENANTADMIN_ALL_SITES_AGGREGATED_SITECOLLECTIONS");
				CamlQuery query = new CamlQuery
				{
					ViewXml = $"<View><Query><Where><Eq><FieldRef Name='SiteUrl' /><Value Type='Text'>{Url}</Value></Eq></Where></Query><RowLimit>1</RowLimit></View>"
				};

				var listItems = list.GetItems(query);
				clientContext.Load(listItems);
				clientContext.ExecuteQueryRetry();

				if (listItems.Count > 0)
				{
					Log.Debug("SPODeletedSite", $"Assigning additional details for {Url} to result");
					var fieldValues = listItems[0].FieldValues;

					CreatedBy = fieldValues["CreatedBy"]?.ToString();
					DeletedBy = fieldValues["DeletedBy"]?.ToString();
					SiteOwnerEmail = fieldValues["SiteOwnerEmail"]?.ToString();
					SiteOwnerName = fieldValues["SiteOwnerName"]?.ToString();
					TemplateName = fieldValues["TemplateName"]?.ToString();
					InformationBarrierMode = fieldValues["IBMode"]?.ToString();

					if (fieldValues["TimeCreated"] != null) CreationTime = DateTime.Parse(fieldValues["TimeCreated"].ToString());
					if (fieldValues["IsRestorable"] != null) IsRestorable = bool.Parse(fieldValues["IsRestorable"].ToString());
					if (fieldValues["LastListActivityOn"] != null) LastListActivityOn = DateTime.Parse(fieldValues["LastListActivityOn"].ToString());
					if (fieldValues["LastItemModifiedDate"] != null) LastItemModifiedDate = DateTime.Parse(fieldValues["LastItemModifiedDate"].ToString());
					if (fieldValues["LastItemModifiedDate"] != null) LastWebActivityOn = DateTime.Parse(fieldValues["LastItemModifiedDate"].ToString());
					if (fieldValues["NumOfFiles"] != null) NumberOfFiles = long.Parse(fieldValues["NumOfFiles"].ToString());
					if (fieldValues["StorageUsed"] != null) StorageUsed = double.Parse(fieldValues["StorageUsed"].ToString());
					if (fieldValues["TemplateId"] != null) TemplateId = short.Parse(fieldValues["TemplateId"].ToString());
					if (fieldValues["LastItemModifiedDate"] != null) LastWebActivityOn = DateTime.Parse(fieldValues["LastItemModifiedDate"].ToString());
					if (fieldValues["StorageUsedPercentage"] != null) StorageUsedPercentage = double.Parse(fieldValues["StorageUsedPercentage"].ToString());
					if (fieldValues["SensitivityLabel"] != null) SensitivityLabelId = Guid.Parse(fieldValues["SensitivityLabel"].ToString());
				}
				else
				{
					Log.Debug("SPODeletedSite", $"No additional details found for {Url}");
				}
			}
		}
	}
}