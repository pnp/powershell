using PnP.PowerShell.Commands.Enums;

namespace PnP.PowerShell.Commands.Model.SharePoint
{
	public class FolderArchiveStateResult
	{
		public string FolderName;

		public string ServerRelativeUrl;

		public FolderArchiveState RequestedState;

		public string MonitorUrl;
	}
}