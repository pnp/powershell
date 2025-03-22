using System;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base.Completers;
using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.InformationManagement
{

    [Cmdlet(VerbsCommon.Set, "PnPListInformationRightsManagement")]
    public class SetListInformationRightsManagement : PnPWebCmdlet
    {
        [Parameter(Mandatory = true)]
        [ArgumentCompleter(typeof(ListNameCompleter))]
        public ListPipeBind List;

        [Parameter(Mandatory = false)]
        public bool? Enable;

        [Parameter(Mandatory = false)]
        public bool? EnableExpiration;

        [Parameter(Mandatory = false)]
        public bool? EnableRejection;

        [Parameter(Mandatory = false)]
        public bool? AllowPrint;

        [Parameter(Mandatory = false)]
        public bool? AllowScript;

        [Parameter(Mandatory = false)]
        public bool? AllowWriteCopy;

        [Parameter(Mandatory = false)]
        public bool? DisableDocumentBrowserView;

        [Parameter(Mandatory = false)]
        public int? DocumentAccessExpireDays;

        [Parameter(Mandatory = false)]
        public DateTime? DocumentLibraryProtectionExpireDate;

        [Parameter(Mandatory = false)]
        public bool? EnableDocumentAccessExpire;

        [Parameter(Mandatory = false)]
        public bool? EnableDocumentBrowserPublishingView;

        [Parameter(Mandatory = false)]
        public bool? EnableGroupProtection;

        [Parameter(Mandatory = false)]
        public bool? EnableLicenseCacheExpire;

        [Parameter(Mandatory = false)]
        public int? LicenseCacheExpireDays;

        [Parameter(Mandatory = false)]
        public string GroupName;

        [Parameter(Mandatory = false)]
        public string PolicyDescription;

        [Parameter(Mandatory = false)]
        public string PolicyTitle;

        [Parameter(Mandatory = false)]
        public string TemplateId;

        protected override void ExecuteCmdlet()
        {
            var list = List.GetList(CurrentWeb, l => l.InformationRightsManagementSettings, l => l.IrmEnabled, l => l.IrmExpire, l => l.IrmReject);
            if (list == null)
                throw new PSArgumentException($"No list found with id, title or url '{List}'", "List");

            if (list.IrmEnabled == false && !Enable.HasValue)
            {
                LogWarning("Information Rights Management is currently disabled for this list. Enable with Set-PnPListInformationRightsManagement -Enable $true");
            }
            else
            {
                var isDirty = false;
                     
                if (Enable.HasValue)
                {
                    list.IrmEnabled = Enable.Value;
                    isDirty = true;
                }
                if(EnableExpiration.HasValue)
                {
                    list.IrmExpire = EnableExpiration.Value;
                    isDirty = true;
                }
                if(EnableRejection.HasValue)
                {
                    list.IrmReject = EnableRejection.Value;
                    isDirty = true;
                }
                if(isDirty)
                {
                    list.Update();
                    ClientContext.Load(list, l => l.InformationRightsManagementSettings, l => l.IrmEnabled, l => l.IrmExpire, l => l.IrmReject);
                    ClientContext.ExecuteQueryRetry();
                }

                if (list.IrmEnabled)
                {
                    // Enablers
                    isDirty = false;
                    if (EnableDocumentAccessExpire.HasValue)
                    {
                        list.InformationRightsManagementSettings.EnableDocumentAccessExpire = EnableDocumentAccessExpire.Value;
                        isDirty = true;
                    }

                    if (EnableDocumentBrowserPublishingView.HasValue)
                    {
                        list.InformationRightsManagementSettings.EnableDocumentBrowserPublishingView = EnableDocumentBrowserPublishingView.Value;
                        isDirty = true;
                    }

                    if (EnableGroupProtection.HasValue)
                    {
                        list.InformationRightsManagementSettings.EnableGroupProtection = EnableGroupProtection.Value;
                        isDirty = true;
                    }

                    if (EnableLicenseCacheExpire.HasValue)
                    {
                        list.InformationRightsManagementSettings.EnableLicenseCacheExpire = EnableLicenseCacheExpire.Value;
                        isDirty = true;
                    }

                    if (DisableDocumentBrowserView.HasValue)
                    {
                        list.InformationRightsManagementSettings.DisableDocumentBrowserView = DisableDocumentBrowserView.Value;
                        isDirty = true;
                    }

                    if (isDirty)
                    {
                        list.Update();
                        ClientContext.ExecuteQueryRetry();
                    }

                    // Properties
                    isDirty = false;
                    if (AllowPrint.HasValue)
                    {
                        list.InformationRightsManagementSettings.AllowPrint = AllowPrint.Value;
                        isDirty = true;
                    }

                    if (AllowScript.HasValue)
                    {
                        list.InformationRightsManagementSettings.AllowScript = AllowScript.Value;
                        isDirty = true;
                    }

                    if (AllowWriteCopy.HasValue)
                    {
                        list.InformationRightsManagementSettings.AllowWriteCopy = AllowWriteCopy.Value;
                        isDirty = true;
                    }

                    if (DocumentAccessExpireDays.HasValue)
                    {
                        if (list.InformationRightsManagementSettings.EnableDocumentAccessExpire)
                        {
                            list.InformationRightsManagementSettings.DocumentAccessExpireDays = DocumentAccessExpireDays.Value;
                            isDirty = true;
                        }
                        else
                        {
                            LogWarning("Document Access expiration is not enabled. Enable with -EnableDocumentAccessExpire $true");
                        }
                    }

                    if(LicenseCacheExpireDays.HasValue)
                    {
                        if(list.InformationRightsManagementSettings.EnableLicenseCacheExpire)
                        {
                            list.InformationRightsManagementSettings.LicenseCacheExpireDays = LicenseCacheExpireDays.Value;
                            isDirty = true;
                        } else {
                            LogWarning("License Cache expiration is not enabled. Enable with -EnableLicenseCacheExpire $true");
                        }
                    }

                    if (DocumentLibraryProtectionExpireDate.HasValue)
                    {
                        if (list.IrmExpire)
                        {
                            list.InformationRightsManagementSettings.DocumentLibraryProtectionExpireDate = DocumentLibraryProtectionExpireDate.Value;
                            isDirty = true;
                        } else
                        {
                            LogWarning("Information Rights Management (IRM) expiration is not enabled. Enable with -EnableExpiration");
                        }
                    }

                    if(GroupName != null)
                    {
                        list.InformationRightsManagementSettings.GroupName = GroupName;
                        isDirty = true;
                    }

                    if (PolicyDescription != null)
                    {
                        list.InformationRightsManagementSettings.PolicyDescription = PolicyDescription;
                        isDirty = true;
                    }

                    if (PolicyTitle != null)
                    {
                        list.InformationRightsManagementSettings.PolicyTitle = PolicyTitle;
                        isDirty = true;
                    }

                    if (TemplateId != null)
                    {
                        list.InformationRightsManagementSettings.TemplateId = TemplateId;
                        isDirty = true;
                    }

                    if (isDirty)
                    {
                        //list.InformationRightsManagementSettings.Update();
                        list.Update();
                        ClientContext.Load(list.InformationRightsManagementSettings);
                        ClientContext.ExecuteQueryRetry();
                    }

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
    }
}
