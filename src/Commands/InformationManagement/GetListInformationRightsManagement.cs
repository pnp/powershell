using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base.Completers;
using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.InformationManagement
{

    [Cmdlet(VerbsCommon.Get, "PnPListInformationRightsManagement")]
    public class GetListInformationRightsManagement : PnPWebCmdlet
    {
        [Parameter(Mandatory = true)]
        [ArgumentCompleter(typeof(ListNameCompleter))]
        public ListPipeBind List;

        protected override void ExecuteCmdlet()
        {
            var list = List.GetList(CurrentWeb, l => l.IrmEnabled, l => l.IrmExpire, l => l.IrmReject);
            if (list == null)
                throw new PSArgumentException($"No list found with id, title or url '{List}'", "List");

            ClientContext.Load(list.InformationRightsManagementSettings);
            ClientContext.ExecuteQueryRetry();

            var irm = new
            {
                InformationRightsManagementEnabled = list.IrmEnabled,
                InformationRightsManagementExpire = list.IrmExpire,
                InformationRightsManagementReject = list.IrmReject,
                list.InformationRightsManagementSettings.AllowPrint,
                list.InformationRightsManagementSettings.AllowScript,
                list.InformationRightsManagementSettings.AllowWriteCopy,
                list.InformationRightsManagementSettings.DisableDocumentBrowserView,
                list.InformationRightsManagementSettings.DocumentAccessExpireDays,
                list.InformationRightsManagementSettings.DocumentLibraryProtectionExpireDate,
                list.InformationRightsManagementSettings.EnableDocumentAccessExpire,
                list.InformationRightsManagementSettings.EnableDocumentBrowserPublishingView,
                list.InformationRightsManagementSettings.EnableGroupProtection,
                list.InformationRightsManagementSettings.EnableLicenseCacheExpire,
                list.InformationRightsManagementSettings.GroupName,
                list.InformationRightsManagementSettings.LicenseCacheExpireDays,
                list.InformationRightsManagementSettings.PolicyDescription,
                list.InformationRightsManagementSettings.PolicyTitle,
                list.InformationRightsManagementSettings.TemplateId
            };
            WriteObject(irm);
        }
    }
}
