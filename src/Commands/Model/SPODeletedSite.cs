using Microsoft.Online.SharePoint.TenantAdministration;
using System;
using System.Collections.Generic;
using System.Text;

namespace PnP.PowerShell.Commands.Model
{
    public class SPODeletedSite
    {
		private Guid siteId;

		private string url;

		private string status;

		private DateTime deletionTime;

		private int daysRemaining;

		private long storageQuota;

		private double resourceQuota;

		public Guid SiteId => siteId;

		public string Url
		{
			get
			{
				return url;
			}
			set
			{
				url = value;
			}
		}

		public string Status => status;

		public DateTime DeletionTime => deletionTime;

		public int DaysRemaining => daysRemaining;

		public long StorageQuota => storageQuota;

		public double ResourceQuota => resourceQuota;

		internal SPODeletedSite(DeletedSiteProperties deletedSiteProperties)
		{
			siteId = deletedSiteProperties.SiteId;
			url = deletedSiteProperties.Url;
			deletionTime = deletedSiteProperties.DeletionTime;
			daysRemaining = deletedSiteProperties.DaysRemaining;
			status = deletedSiteProperties.Status;
			storageQuota = deletedSiteProperties.StorageMaximumLevel;
			resourceQuota = deletedSiteProperties.UserCodeMaximumLevel;
		}
	}
}
