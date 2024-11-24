---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/New-PnPPersonalSite.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: New-PnPPersonalSite
---

# New-PnPPersonalSite

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

## SYNTAX

### Default (Default)

```
New-PnPPersonalSite [-Email] <String[]> [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Creates a OneDrive For Business site for the provided user(s)

If you want to use this cmdlet in an automated script not requiring manual authentication, you *must* assign the following permission to your application registration from either Azure Active Directory or done through https://tenant-admin.sharepoint.com/_layouts/appregnew.aspx with the following permission through https://tenant-admin.sharepoint.com/_layouts/appinv.aspx:

`
<AppPermissionRequests AllowAppOnlyPolicy="true">
  <AppPermissionRequest Scope="http://sharepoint/social/tenant" Right="FullControl" />
</AppPermissionRequests>
`

You then *must* connect using:

`
Connect-PnPOnline -Url https://tenant-admin.sharepoint.com -ClientId <clientid> -ClientSecret <clientsecret>
`

Authenticating using a certificate is *not* possible and will throw an unauthorized exception. It does not require assigning any permissions in Azure Active Directory.

If you want to run this cmdlet using an interactive login, you *must* connect using:

`
Connect-PnPOnline -Url https://tenant-admin.sharepoint.com -UseWebLogin
`

## EXAMPLES

### EXAMPLE 1

```powershell
New-PnPPersonalSite -Email @('katiej@contoso.onmicrosoft.com','garth@contoso.onmicrosoft.com')
```

Creates a OneDrive For Business site for the provided two users

## PARAMETERS

### -Connection

Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.

```yaml
Type: PnPConnection
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Email

The UserPrincipalName (UPN) of the users

```yaml
Type: String[]
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: 0
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
