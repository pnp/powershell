using PnP.PowerShell.Commands.Enums;

namespace PnP.PowerShell.Commands.Model.SharePoint
{
	public class FileArchiveStateResult
	{
		public string FileName;

		public string ServerRelativeUrl;

		public FileArchiveState RequestedState;

		public string ArchiveStatus;
	}
}