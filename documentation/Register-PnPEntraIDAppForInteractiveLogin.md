---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Register-PnPEntraIDAppForInteractiveLogin.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Register-PnPEntraIDAppForInteractiveLogin
---

# Register-PnPEntraIDAppForInteractiveLogin

## SYNOPSIS

Registers an Entra ID App for use with Interactive login

## SYNTAX

### Generate App using Interactive Login

```
Register-PnPEntraIDAppForInteractiveLogin -ApplicationName <String> -Tenant <String> -Interactive
 [-GraphApplicationPermissions <Permission[]>] [-GraphDelegatePermissions <Permission[]>]
 [-SharePointApplicationPermissions <Permission[]>] [-SharePointDelegatePermissions <Permission[]>]
 [-NoPopup] [-LogoFilePath <string>] [-MicrosoftGraphEndPoint <string>]
 [-EntraIDLoginEndPoint <string>] [-SignInAudience <EntraIDSignInAudience>]
 [-LaunchBrowser <SwitchParameter>]
```

### Generate App using Device Login

```
Register-PnPEntraIDAppForInteractiveLogin -ApplicationName <String> -Tenant <String> -DeviceLogin
 [-GraphApplicationPermissions <Permission[]>] [-GraphDelegatePermissions <Permission[]>]
 [-SharePointApplicationPermissions <Permission[]>] [-SharePointDelegatePermissions <Permission[]>]
 [-NoPopup] [-LogoFilePath <string>] [-SignInAudience <EntraIDSignInAudience>]
 [-LaunchBrowser <SwitchParameter>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Registers an Entra ID App for use with the interactive login on Connect-PnPOnline. You will have to specify either -Interactive or -DeviceLogin to authenticate.

## EXAMPLES

### EXAMPLE 1

```powershell
Register-PnPEntraIDAppForInteractiveLogin -ApplicationName TestApp -Tenant yourtenant.onmicrosoft.com -Interactive
```

Creates a new Entra ID Application registration. The application will be setup with the following delegate permissions to consent: AllSites.FullControl, Group.ReadWrite.All, User.ReadWrite.All, TermStore.ReadWrite.All. A browser window will be shown allowing you to authenticate.

### EXAMPLE 2

```powershell
Register-PnPEntraIDAppForInteractiveLogin -ApplicationName TestApp -Tenant yourtenant.onmicrosoft.com -GraphDelegatePermissions "Group.Read.All" -SharePointDelegatePermissions "AllSites.FullControl" -Interactive
```

Creates a new Entra ID Application registration. The application will be setup with the following delegate permissions to consent: Group.Read.All, AllSites.FullControl. A browser window will be shown allowing you to authenticate.

## PARAMETERS

### -ApplicationName

The name of the Azure AD Application to create.

```yaml
Type: String
DefaultValue: ''
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -AzureEnvironment

The Azure environment to use for authentication, the defaults to 'Production' which is the main Azure environment.

```yaml
Type: AzureEnvironment
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
AcceptedValues:
- Production
- PPE
- China
- Germany
- USGovernment
- USGovernmentHigh
- USGovernmentDoD
- Custom
HelpMessage: ''
```

### -DeviceLogin

If specified, a device login flow, supporting Multi-Factor Authentication will be used to authenticate towards the Microsoft Graph.

```yaml
Type: SwitchParameter
DefaultValue: ''
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

### -EntraIDLoginEndPoint

Sets the EntraID login endpoint to be used for creation of the app. This only works if Azure Environment parameter is set to `Custom`

```yaml
Type: String
DefaultValue: ''
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

### -GraphApplicationPermissions

Specify which Microsoft Graph Application permissions to request.

```yaml
Type: Permission[]
DefaultValue: ''
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Generate Certificate
  Position: 0
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -GraphDelegatePermissions

Specify which Microsoft Graph Delegate permissions to request.

```yaml
Type: Permission[]
DefaultValue: ''
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Generate Certificate
  Position: 0
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Interactive

If specified, an interactive authentication flow will be started, allowing your to authenticate with username, password and an optional second factor from your phone or other device.

```yaml
Type: SwitchParameter
DefaultValue: ''
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

### -LaunchBrowser

Launch a browser automatically and copy the code to enter to the clipboard

```yaml
Type: SwitchParameter
DefaultValue: False
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: DeviceLogin
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -LogoFilePath

Sets the logo for the Azure AD application. Provide a full path to a local image file on your disk which you want to use as the logo.

```yaml
Type: String
DefaultValue: ''
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

### -MicrosoftGraphEndPoint

Sets the Microsoft Graph endpoint to be used for creation of the app. This only works if Azure Environment parameter is set to `Custom`

```yaml
Type: String
DefaultValue: ''
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

### -NoPopup

This switch only applies to Windows and has no effect on Linux and MacOS.

If not specified and running on Windows, all authentication and consent steps will be presented in a popup. If you want to open the URLs manually in a browser, specify this switch.

```yaml
Type: SwitchParameter
DefaultValue: ''
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

### -SharePointApplicationPermissions

Specify which Microsoft SharePoint Application permissions to request.

```yaml
Type: Permission[]
DefaultValue: ''
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Generate Certificate
  Position: 0
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -SharePointDelegatePermissions

Specify which Microsoft SharePoint Delegate permissions to request.

```yaml
Type: Permission[]
DefaultValue: ''
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Generate Certificate
  Position: 0
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -SignInAudience

Sets the sign in audience. Use this to make the app support Single tenant accounts, Multi-tenant accounts, Multi-tenant + personal accounts & personal accounts only.

```yaml
Type: String
DefaultValue: ''
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Generate Certificate
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Tenant

The identifier of your tenant, e.g. mytenant.onmicrosoft.com

```yaml
Type: String
DefaultValue: ''
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
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
