using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.Files
{
    [Cmdlet(VerbsCommon.Get, "PnPFileCheckedOut")]
    [OutputType(typeof(IEnumerable<Model.SharePoint.CheckedOutFile>))]
    public class GetFileCheckedOut : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        public ListPipeBind List;

        protected override void ExecuteCmdlet()
        {
            var list = List.GetList(CurrentWeb);
            var checkedOutFiles = list.GetCheckedOutFiles();
            
            ClientContext.Load(checkedOutFiles, cof => cof.Include(c => c.CheckedOutBy, c => c.ServerRelativePath));
            ClientContext.ExecuteQueryRetry();

            checkedOutFiles.Select(c => new Model.SharePoint.CheckedOutFile
            {
                ServerRelativeUrl = c.ServerRelativePath.DecodedUrl,
                CheckedOutBy = new Model.User
                {
                    DisplayName = c.CheckedOutBy.Title,
                    Email = c.CheckedOutBy.Email,
                    Id = c.CheckedOutBy.Id,
                    LoginName = c.CheckedOutBy.LoginName
                }
            }).ToList().ForEach(WriteObject);
        }
    }
}
