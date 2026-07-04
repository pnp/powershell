using System;

namespace PnP.PowerShell.Commands.Model
{
	/// <summary>
	/// Contains the SharePoint Online multi-geo location details for a user's OneDrive personal site.
	/// </summary>
	public class UserPersonalSiteLocation
	{
		/// <summary>
		/// The user principal name for the OneDrive owner.
		/// </summary>
		public string UserPrincipalName { get; set; }

		/// <summary>
		/// The SharePoint Online multi-geo location code for the user's OneDrive personal site.
		/// </summary>
		public string Location { get; set; }

		/// <summary>
		/// The URL of the user's OneDrive personal site.
		/// </summary>
		public string MySiteUrl { get; set; }

		/// <summary>
		/// The site collection identifier of the user's OneDrive personal site.
		/// </summary>
		public Guid SiteId { get; set; }
	}
}
