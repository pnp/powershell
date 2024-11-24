---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPAzureADUser.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPAzureADUser
---

# Get-PnPAzureADUser

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : One of Directory.Read.All, Directory.ReadWrite.All, User.Read.All, User.ReadWrite.All

Retrieves users from Azure Active Directory. By default the following properties will be loaded: BusinessPhones, DisplayName, GivenName, JobTitle, Mail, MobilePhone, OfficeLocation, PreferredLanguage, Surname, UserPrincipalName, Id, AccountEnabled

## SYNTAX

### Return a list (Default)

```
Get-PnPAzureADUser [-Filter <String>] [-OrderBy <String>] [-Select <String[]>] [-StartIndex <Int32>]
 [-EndIndex<Int32>] [-Connection <PnPConnection>]
```

### Return by specific ID

```
Get-PnPAzureADUser [-Identity <String>] [-Select <String[]>] [-Connection <PnPConnection>]
```

### Return the delta

```
Get-PnPAzureADUser [-Filter <String>] [-OrderBy <String>] [-Select <String[]>] [-Delta]
 [-DeltaToken <String>] [-StartIndex <Int32>] [-EndIndex<Int32>] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to retrieve a single user or a list of users from Azure Active Directory.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPAzureADUser
```

Retrieves all users from Azure Active Directory.

### EXAMPLE 2

```powershell
Get-PnPAzureADUser -EndIndex 50
```

Retrieves the first 50 users from Azure Active Directory. Notice that you have no control over who will be in this batch of 50 unless you combine it with the `-Filter` and/or `-OrderBy` parameters.

### EXAMPLE 3

```powershell
Get-PnPAzureADUser -Identity 328c7693-5524-44ac-a946-73e02d6b0f98
```

Retrieves the user from Azure Active Directory with the id 328c7693-5524-44ac-a946-73e02d6b0f98

### EXAMPLE 4

```powershell
Get-PnPAzureADUser -Identity john@contoso.com
```

Retrieves the user from Azure Active Directory with the user principal name john@contoso.com.

### EXAMPLE 5

```powershell
Get-PnPAzureADUser -Identity john@contoso.com -Select "DisplayName","extension_3721d05137db455ad81aa442e3c2d4f9_extensionAttribute1"
```

Retrieves only the DisplayName and extensionAttribute1 properties of the user from Azure Active Directory which has the user principal name john@contoso.com.

### EXAMPLE 6

```powershell
Get-PnPAzureADUser -Filter "accountEnabled eq false"
```

Retrieves all the disabled users from Azure Active Directory.

### EXAMPLE 7

```powershell
Get-PnPAzureADUser -Filter "startswith(DisplayName, 'John')" -OrderBy "DisplayName"
```

Retrieves all the users from Azure Active Directory of which their DisplayName starts with 'John' and sort the results by the DisplayName.

### EXAMPLE 8

```powershell
Get-PnPAzureADUser -Delta
```

Retrieves all the users from Azure Active Directory and includes a delta DeltaToken which can be used by providing -DeltaToken `<token>` to query for changes to users in Active Directory since this run.

### EXAMPLE 9

```powershell
Get-PnPAzureADUser -Delta -DeltaToken abcdef
```

Retrieves all the users from Azure Active Directory which have had changes since the provided DeltaToken was given out.

### EXAMPLE 10

```powershell
Get-PnPAzureADUser -StartIndex 10 -EndIndex 20
```

Retrieves the 10th through the 20th user from Azure Active Directory. Notice that you have no control over which users will be in this batch of 10 users.

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

### -Delta

Retrieves all users and provides a SkipToken delta token to allow to query for changes since this run when querying again by adding -DeltaToken to the command.

Note that using -Select and -Filter in combination with this parameter is limited. More information on this can be found [here](https://learn.microsoft.com/graph/api/user-delta?view=graph-rest-1.0&tabs=http#odata-query-parameters).

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Return the delta
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -DeltaToken

The change token provided during the previous run with -Delta to query for the changes to user objects made in Azure Active Directory since that run.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Return the delta
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -EndIndex

Allows defining the last result to return. Useful for i.e. pagination. If omitted, it will return all available users from Azure Active Directory.

```yaml
Type: Int32
DefaultValue: $null
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

### -Filter

Includes a filter to the retrieval of the users. Use OData instructions to construct the filter, i.e. "startswith(DisplayName, 'John')".

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Return a list
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: Return the delta
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Identity

Returns the user with the provided user id.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Return by specific ID
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -OrderBy

Includes a custom sorting instruction to the retrieval of the users. Use OData syntax to construct the orderby, i.e. "DisplayName desc".

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Return a list
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: Return the delta
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Select

Allows providing an array with the property names of specific properties to return. If not provided, the default properties will be returned.

```yaml
Type: String[]
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

### -StartIndex

Allows defining the first result to return. Useful for i.e. pagination.

```yaml
Type: Int32
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

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
