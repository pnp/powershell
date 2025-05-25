# ACS Deprecation

As [announced back in November 2023](https://techcommunity.microsoft.com/blog/spblog/azure-acs-retirement-in-microsoft-365/3982039) already, Microsoft will deprecate ACS (Access Control Services) for new tenants as of November 1st, 2024 and it will stop working for existing tenants and will be fully retired as of April 2nd, 2026. This applies to all environments including Government Clouds and Department of Defense. In essence, we're talking about the `_layouts/appregnew.aspx`, `_layouts/appinv.aspx` and `_layouts/appprincipals.aspx` pages that will be going away.

## What does this mean for me?
If you're using any application registrations that have formerly been set up through `_layouts/appregnew.aspx`, it will stop working on above mentioned dates. It does not matter if you're using it with PnP PowerShell, through an SharePoint Add-In, direclty using CSOM (Client Side Object Model), direct `_api` calls to SharePoint, or any other means.

## What can I do to ensure my applications remain working
Simple: replace them with a proper Entra ID Application Registration. 

If the `_layouts/apprenew.aspx` was done in December 2024 or later, it will already [have created an Entra ID Application registration](https://learn.microsoft.com/sharepoint/dev/sp-add-ins/add-ins-and-azure-acs-retirements-faq#when-i-use-appregnewaspx-the-created-acs-principals-show-up-in-entra) for you. To validate this, simply take the Client ID/App Id of your application, go to [Entra ID](https://entra.microsoft.com), navigate to Identity > Applications > App registrations, click on the "All applications" tab and search for your Client ID/App ID. If it yields a result, it means your application also exists in Entra ID. If you go into the Entra ID Application registration and click on API permissions you will likely see no permissions being added to it. This is a clear indicator that this application registration has been done through `_layouts/appregnew.aspx` in or after December 2024, that it created an Entra ID Application registration counterpart, but that it still leverages ACS for its authentication.

If you cannot find an entry in Entra ID with the same Client ID/App ID, it means the `_layouts/appregnew.aspx` operation has been done before December 2024 and no entry exists for it yet in Entra ID. 

## FAQ

### Can I use `-PersistLogin` in Azure?

No you cannot, as there are no profiles folders in Azure.

### Can I use `-PersistLogin` with an app only context?

No, it is meant to be used for an interactive delegated authentication context only. If you want to use an app only context, you can just use the parameters with the `Connect-PnPOnline` cmdlet that support app only authentication as normal. Documentation for it can be [found here](../cmdlets/Connect-PnPOnline.md#app-only-with-azure-active-directory).

### Do I still need my own application registration in Entra ID when using `-PersistLogin`?

Yes, this is still required.

### Can I use a different application registration for `-PersistLogin` for different tenants or even site collections on the same tenant?

Yes, that is supported. Just use it as described above and it will store the token for the tenant or site collection you are connecting to.
