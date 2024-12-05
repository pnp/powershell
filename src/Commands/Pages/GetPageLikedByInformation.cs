
using PnP.Core.QueryModel;
using PnP.PowerShell.Commands.Base.Completers;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model;
using System;
using System.Collections.Generic;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Pages
{
    [Cmdlet(VerbsCommon.Get, "PnPPageLikedByInformation")]
    [OutputType(typeof(PageLikedByInformation))]
    public class GetPageLikedByInformation : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        [ArgumentCompleter(typeof(PageCompleter))]
        public PagePipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            var clientSidePage = Identity.GetPage(Connection);

            if (clientSidePage == null)
                throw new Exception($"Page '{Identity?.Name}' does not exist");

            var pageLikeInformation = clientSidePage.GetLikedByInformation();
            
            var likes = pageLikeInformation.LikedBy.AsRequested();

            var likesList = new List<PageLikedByInformation>();
            
            if(likes != null)
            {
                foreach( var liked in likes)
                {
                    var likedInfo = new PageLikedByInformation
                    {
                        Name = liked.Name,
                        Mail = liked.Mail,
                        LoginName = liked.LoginName,
                        Id = liked.Id,
                        CreationDate = liked.CreationDate
                    };

                    likesList.Add(likedInfo);
                }
            }

            WriteObject(likesList, true);
        }
    }
}