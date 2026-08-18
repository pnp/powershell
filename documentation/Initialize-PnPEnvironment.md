---
Module Name: PnP.PowerShell
title: Initialize-PnPEnvironment
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Initialize-PnPEnvironment.html
---
 
# Initialize-PnPEnvironment

## SYNOPSIS
Sets up PnP PowerShell against a tenant in one command: registers an Entra ID application with the requested permissions, stores its client id for the site, connects with it and verifies the permissions arrived.

## SYNTAX

```powershell
Initialize-PnPEnvironment -Url <String> [-Tenant <String>] [-ApplicationName <String>]
 [-GraphApplicationPermissions <String[]>] [-GraphDelegatePermissions <String[]>]
 [-SharePointApplicationPermissions <String[]>] [-SharePointDelegatePermissions <String[]>]
 [-O365ManagementApplicationPermissions <String[]>] [-O365ManagementDelegatePermissions <String[]>]
 [-ExchangeApplicationPermissions <String[]>] [-ExchangeDelegatePermissions <String[]>]
 [-PowerBIApplicationPermissions <String[]>] [-PowerBIDelegatePermissions <String[]>]
 [-DataverseDelegatePermissions <String[]>] [-PowerAppsDelegatePermissions <String[]>]
 [-AzureServiceManagementDelegatePermissions <String[]>] [-ResourcePermissions <Hashtable[]>]
 [-ValidateCommand <String[]>] [-DeviceLogin] [-CertificateStore <StoreLocation>] [-OutPath <String>]
 [-CertificatePassword <SecureString>] [-AzureEnvironment <AzureEnvironment>] [-PersistLogin]
 [-SkipConnect] [-WhatIf] [-Confirm]
 
```

## DESCRIPTION
Performs the whole first time setup that otherwise takes four separate commands, in this order:

1. `Register-PnPEntraIDApp` signs you in interactively, creates the application registration with the permissions you asked for, and runs its admin consent flow.
2. `Set-PnPManagedAppId` stores the resulting client id against the URL, so it can be looked up rather than remembered.
3. `Connect-PnPOnline` connects with the new registration.
4. `Test-PnPConnectionPermission` checks the connection really holds the permissions the cmdlets in `-ValidateCommand` declare.

A certificate is only created when application permissions are requested, because that is the only case which needs one. Requesting delegated permissions alone connects interactively instead, and passes `-SkipCertCreation` to `Register-PnPEntraIDApp` so no unused key is left on the registration.

On Microsoft Windows the certificate goes into the certificate store, which lets every later connection authenticate by thumbprint with no file and no password. On Linux and macOS, and whenever `-OutPath` is specified, a PFX file is written instead and later connections need `-CertificatePath` and `-CertificatePassword`.

The same permission parameters as `Register-PnPEntraIDApp` are offered, for Microsoft Graph, SharePoint, the Office 365 Management APIs, Exchange, Power BI, Dataverse, Power Apps and Azure Resource Manager, plus `-ResourcePermissions` for any other API. The permissions of Microsoft Graph, SharePoint and the Office 365 Management APIs ship with the module, so those parameters tab complete and reject an unknown permission before anything is created. The permissions of the other resources are known only to the tenant, so those are validated by `Register-PnPEntraIDApp` once it signs in.

When no permission parameter is given, the delegated set of `Register-PnPEntraIDAppForInteractiveLogin` is requested: SharePoint `TermStore.ReadWrite.All`, `AllSites.FullControl` and `User.ReadWrite.All`, plus Microsoft Graph `Group.ReadWrite.All` and `User.ReadWrite.All`. Being delegated, the application can never do more than the account signing in already may, and no certificate is created. The fallback of `Register-PnPEntraIDApp` is deliberately not used, because that one requests app-only tenant wide full control.

The returned object carries the client id, the certificate location, the outcome of each permission check, and a one step admin consent URL to fall back on if consent was declined or the account that signed in could not grant it.

Once the application has been registered it exists in the tenant, so if a later step fails the object is still returned before the error is raised. Without it the registration that was just created could neither be used nor removed. `Connected` and `PermissionChecks` show how far the setup got, and `ConsentRequired` is `$null` rather than `$false` when no check ran, so an unverified setup is never read as one where consent is known to be in order.

Parameters that cannot take effect are reported rather than ignored: `-OutPath`, `-CertificateStore` and `-CertificatePassword` describe a certificate that is only created for application permissions, and `-PersistLogin` applies only to the interactive sign in used for delegated ones.

The `NextStep` command always names `-ClientId` explicitly rather than relying on the stored value being found. The app-only parameter sets of `Connect-PnPOnline` require it outright, and while `-Interactive` will look a client id up, it reads the persisted login cache in `settings.json` before the credential store. That cache holds one entry per URL and client id with no deduplication and the lookup takes the first match, so on a machine that has used `-PersistLogin` against the same tenant with a different application, an older entry wins and the connection would quietly use that other registration.

Note that Entra ID does not apply a permission grant instantly. The cmdlet therefore waits 30 seconds before connecting. When application permissions were requested and one still reads as not held, it waits and reconnects once more, which builds a new confidential client and so asks for a genuinely new token. A delegated connection is not retried, because its access token stays cached for the process and a second attempt would re-read the same one while risking another sign-in prompt.

The verification checks the permission claims in the access token, which is not the same as effective access. `Sites.Selected` is the case where the two diverge: the claim is present as soon as consent is granted, so the check reports success, while every call fails with an access denied until the application is granted access to a specific site with `Grant-PnPEntraIDAppSitePermission`. A warning is emitted whenever `Sites.Selected` is requested, because no check performed here can detect it.

## EXAMPLES

### EXAMPLE 1
```powershell
Initialize-PnPEnvironment -Url "https://contoso.sharepoint.com"
```
The minimum invocation. Resolves the tenant from the URL, registers an application with the default delegated permissions, stores its client id, connects interactively and confirms `Get-PnPSite` can run. No certificate is created, since the default permissions are delegated. Add `-PersistLogin` to make every later interactive connection silent.

### EXAMPLE 2
```powershell
$environment = Initialize-PnPEnvironment -Url "https://contoso.sharepoint.com" -SharePointApplicationPermissions "Sites.Selected"

# Sites.Selected grants nothing until the application is granted access to a specific site.
# This needs a connection holding the delegated Microsoft Graph Sites.FullControl.All permission,
# so it cannot be done by the application that was just registered.
Connect-PnPOnline -Url "https://contoso.sharepoint.com" -Interactive -ClientId <an app that may grant site permissions>
Grant-PnPEntraIDAppSitePermission -AppId $environment.ClientId -DisplayName "PnP PowerShell" -Site "https://contoso.sharepoint.com/sites/marketing" -Permissions Read
```
Registers an application with a certificate holding the SharePoint `Sites.Selected` application permission, connects app-only by thumbprint, then grants it access to one site.

The grant is a required second step, and the permission check cannot tell you that it is missing: `Get-PnPSite` accepts `Sites.Selected` as one of its permissions, so `Test-PnPConnectionPermission` finds the claim in the token and reports success while every call still fails with an access denied until the site level grant exists. The cmdlet emits a warning saying so whenever `Sites.Selected` is requested.

### EXAMPLE 3
```powershell
Initialize-PnPEnvironment -Url "https://contoso.sharepoint.com" -Tenant "contoso.onmicrosoft.com" -SharePointApplicationPermissions "Sites.FullControl.All" -ValidateCommand "Get-PnPTenantSite","Set-PnPListItem" -WhatIf
```
Reports what would be registered, stored and connected without creating anything. Drop `-WhatIf` to run it, and the two named cmdlets are then checked against the permissions the connection actually received.

### EXAMPLE 4
```powershell
$environment = Initialize-PnPEnvironment -Url "https://contoso.sharepoint.com" -Tenant "contoso.onmicrosoft.com" -SharePointApplicationPermissions "Sites.FullControl.All" -SkipConnect
$environment.ConsentUrl
```
Registers the application and stores its client id, but does not connect. Useful when a different person has to grant admin consent first: send them `ConsentUrl`, then connect with the command in `NextStep`.

## PARAMETERS

### -ApplicationName
Display name of the Entra ID application registration to create. Defaults to `PnP PowerShell`. An application with this name already existing is an error naming this parameter, so re-running the cmdlet against a tenant it has already been run on needs a different name, or the existing registration removed with `Remove-PnPEntraIDApp`.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: PnP PowerShell
Accept pipeline input: False
Accept wildcard characters: False
```

### -AzureEnvironment
The cloud to register the application in and connect to. Defaults to `Production`.

```yaml
Type: AzureEnvironment
Parameter Sets: (All)
Accepted values: Production, PPE, China, Germany, USGovernment, USGovernmentHigh, USGovernmentDoD, BleuCloud, DelosCloud, GovSGCloud, Custom

Required: False
Position: Named
Default value: Production
Accept pipeline input: False
Accept wildcard characters: False
```

### -AzureServiceManagementDelegatePermissions
Azure Resource Manager delegated permissions to request. Validated against the tenant rather than up front, so this parameter does not tab complete.

```yaml
Type: String[]
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -CertificatePassword
Password to protect the generated certificate with. Only used when application permissions are requested.

```yaml
Type: SecureString
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -CertificateStore
The Windows certificate store location to place the generated certificate in, so that later connections can authenticate by thumbprint. Ignored on Linux and macOS, and when `-OutPath` is specified.

```yaml
Type: StoreLocation
Parameter Sets: (All)
Accepted values: CurrentUser, LocalMachine

Required: False
Position: Named
Default value: CurrentUser
Accept pipeline input: False
Accept wildcard characters: False
```

### -Confirm
Prompts you for confirmation before running the cmdlet.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: cf

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DataverseDelegatePermissions
Dataverse delegated permissions to request. Validated against the tenant rather than up front, so this parameter does not tab complete.

```yaml
Type: String[]
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DeviceLogin
Sign in with a device code instead of a browser window. Use on a machine with no browser available: it applies to registering the application and, when delegated permissions were requested, to the connection made afterwards, so no step falls back to an interactive sign in.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ExchangeApplicationPermissions
Exchange application permissions to request. Validated against the tenant rather than up front, so this parameter does not tab complete. Requesting any application permission causes a certificate to be created and the connection to be made app-only.

```yaml
Type: String[]
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ExchangeDelegatePermissions
Exchange delegated permissions to request. Validated against the tenant rather than up front, so this parameter does not tab complete.

```yaml
Type: String[]
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -GraphApplicationPermissions
Microsoft Graph application permissions to request. Requesting any application permission causes a certificate to be created and the connection to be made app-only.

```yaml
Type: String[]
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -GraphDelegatePermissions
Microsoft Graph delegated permissions to request.

```yaml
Type: String[]
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -O365ManagementApplicationPermissions
Office 365 Management APIs application permissions to request. Requesting any application permission causes a certificate to be created and the connection to be made app-only.

```yaml
Type: String[]
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -O365ManagementDelegatePermissions
Office 365 Management APIs delegated permissions to request.

```yaml
Type: String[]
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -OutPath
Folder to write the certificate PFX and CER files to. The folder has to exist: a certificate is only written into one that does, so a folder that does not would leave a registered application whose key material exists nowhere, and this is rejected before anything is created. Specifying it uses a PFX file rather than the Windows certificate store, so later connections need `-CertificatePath` and `-CertificatePassword`.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -PersistLogin
Cache the refresh token so later interactive connections do not prompt. Only applies when connecting with delegated permissions.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -PowerAppsDelegatePermissions
Power Apps delegated permissions to request. Validated against the tenant rather than up front, so this parameter does not tab complete.

```yaml
Type: String[]
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -PowerBIApplicationPermissions
Power BI application permissions to request. Validated against the tenant rather than up front, so this parameter does not tab complete. Requesting any application permission causes a certificate to be created and the connection to be made app-only.

```yaml
Type: String[]
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -PowerBIDelegatePermissions
Power BI delegated permissions to request. Validated against the tenant rather than up front, so this parameter does not tab complete.

```yaml
Type: String[]
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ResourcePermissions
Permissions on any other API, as hashtables holding a `Resource` key and an `ApplicationPermissions` or `DelegatePermissions` key. Passed through to `Register-PnPEntraIDApp`.

```yaml
Type: Hashtable[]
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SharePointApplicationPermissions
SharePoint application permissions to request. Requesting any application permission causes a certificate to be created and the connection to be made app-only.

```yaml
Type: String[]
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SharePointDelegatePermissions
SharePoint delegated permissions to request.

```yaml
Type: String[]
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SkipConnect
Register the application and store its client id, but do not connect or check permissions. Use when admin consent still has to be granted by someone else.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Tenant
The tenant to register the application in, such as `contoso.onmicrosoft.com` or a tenant id. Resolved from `-Url` when omitted, through the same unauthenticated realm lookup `Connect-PnPOnline` and `Get-PnPTenantId` use, which works for vanity domains too. Specify it explicitly when that lookup cannot reach the tenant.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Url
The site collection to set the environment up for. The client id is stored against this URL, so connecting to it or to any site beneath it resolves the client id automatically.

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ValidateCommand
The PnP PowerShell cmdlets to check the resulting connection against with `Test-PnPConnectionPermission`. Defaults to `Get-PnPSite`, which declares both application and delegated permissions and is therefore verifiable whichever way the connection was made. A cmdlet that declares no permissions can only report as indeterminate, which is surfaced as a warning but is not treated as a failure. An empty array skips verification altogether and says so in a warning, so that an unverified setup is never reported as a verified one. `$null` is rejected.

```yaml
Type: String[]
Parameter Sets: (All)

Required: False
Position: Named
Default value: Get-PnPSite
Accept pipeline input: False
Accept wildcard characters: False
```

### -WhatIf
Reports what would be registered, stored and connected without changing anything.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: wi

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

[Register-PnPEntraIDApp](Register-PnPEntraIDApp.md)

[Set-PnPManagedAppId](Set-PnPManagedAppId.md)

[Test-PnPConnectionPermission](Test-PnPConnectionPermission.md)

[Grant-PnPEntraIDAppSitePermission](Grant-PnPEntraIDAppSitePermission.md)
