---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Invoke-PnPGraphMethod.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Invoke-PnPGraphMethod
---

# Invoke-PnPGraphMethod

## SYNOPSIS

Invokes a REST request towards the Microsoft Graph API

## SYNTAX

### Out to console (Default)

```
Invoke-PnPGraphMethod [[-Method] <HttpRequestMethod>] -Url <String> [-Content <Object>]
 [-ContentType <String>] [-ConsistencyLevelEventual] [-Raw] [-All] [-Connection <PnPConnection>]
 [-Verbose]
```

### Out to file

```
Invoke-PnPGraphMethod [[-Method] <HttpRequestMethod>] -Url <String> [-Content <Object>]
 [-ContentType <String>] [-ConsistencyLevelEventual] [-Connection <PnPConnection>]
 [-OutFile <String>] [-Verbose]
```

### Out to stream

```
Invoke-PnPGraphMethod [[-Method] <HttpRequestMethod>] -Url <String> [-Content <Object>]
 [-ContentType <String>] [-ConsistencyLevelEventual] [-Connection <PnPConnection>] [-OutStream]
 [-Verbose]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Invokes a REST request towards the Microsoft Graph API. It will take care of potential throttling retries that are needed to retrieve the data.

## EXAMPLES

### Example 1

```powershell
Invoke-PnPGraphMethod -Url "groups?`$filter=startsWith(displayName,'ZZ')&`$select=displayName"
Invoke-PnPGraphMethod -Url 'groups/{id}?`$select=hideFromOutlookClients'
```

Execute a GET request to get groups by filter and select.

### Example 2

```powershell
Invoke-PnPGraphMethod -Url "groups/{id}" -Method Delete
```

Delete the group with the specified id.

### Example 3

```powershell
Invoke-PnPGraphMethod -Url "groups/{id}" -Method Patch -Content @{ displayName = "NewName" }
```

Set the new displayName of the group with a Patch request.

### Example 4

```powershell
Invoke-PnPGraphMethod -Url "v1.0/users?$filter=accountEnabled ne true&$count=true" -Method Get -ConsistencyLevelEventual
```

Get users with advanced query capabilities. Use of -ConsistencyLevelEventual.

### Example 5

```powershell
Invoke-PnPGraphMethod -Url "https://graph.microsoft.com/v1.0/users"
```

Performs a GET request to retrieve users from the Microsoft Graph API using the full URL.

### Example 6

```powershell
Invoke-PnPGraphMethod -Url "https://graph.microsoft.com/v1.0/users/user@contoso.com/photo/`$value" -OutFile c:\temp\photo.jpg
```

Downloads the user profile photo of the specified user to the specified file.

### Example 7

```powershell
Invoke-PnPGraphMethod -Url "https://graph.microsoft.com/v1.0/users/user@contoso.com/photo/`$value" -OutStream | Add-PnPFile -FileName user.jpg -Folder "Shared Documents"
```

Takes the user profile photo of the specified user and uploads it to the specified library in SharePoint Online.

### Example 8

```powershell
$task = Invoke-PnPGraphMethod -Url "https://graph.microsoft.com/v1.0/planner/tasks/23fasefxcvzvsdf32e" # retrieve the task so we can figure out the etag which is needed to update the task
$etag = $task.'@odata.etag'
$headers = @{"If-Match"=$etag}
$content = @{"title"="My new task title"}
Invoke-PnPGraphMethod -Url "https://graph.microsoft.com/v1.0/planner/tasks/23fasefxcvzvsdf32e" -Method PATCH -Content $content -AdditionalHeaders $headers
```

This example retrieves a Planner task to find the etag value which is required to update the task. In order to update the task through call to the Microsoft Graph API we need to include an If-Match header with the value of the etag. It then creates the content to update, in this case the title of the task, and calls the PATCH method on the Graph end-point to update the specific task.

## PARAMETERS

### -AdditionalHeaders

Additional request headers, either by providing a Dictionary<string,string> or a Hastable, .e.g -AdditionalHeaders @{"If-Match"="234567tysfssdvsadf"}

```yaml
Type: GraphAdditionalHeaderPipeBind
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

### -All

Retrieve all pages of results. This will loop through all @odata.nextLink. This flag will only be respected if the request is a GET request.

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Out to console
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Connection

Optional connection to be used by the cmdlet.
Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.

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

### -ConsistencyLevelEventual

Set the ConsistencyLevel header to eventual for advanced query capabilities on Azure AD directory objects

```yaml
Type: SwitchParameter
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

### -Content

A string or object to send

```yaml
Type: Object
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

### -ContentType

The content type of the object to send. Defaults to 'application/json'.

```yaml
Type: String
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

### -Method

The HTTP method to execute. Defaults to GET.

```yaml
Type: HttpRequestMethod
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: 0
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues:
- Default
- Get
- Head
- Post
- Put
- Delete
- Trace
- Options
- Merge
- Patch
HelpMessage: ''
```

### -OutFile

The full path including filename to write the output to, i.e. c:\temp\myfile.txt. Existing files will be overwritten.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Out to file
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -OutStream

Indicates that the result of the request should be returned as a memory stream.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Out to stream
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Raw

If specified the returned data will not be converted to an object but returned as a JSON string.

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Out to console
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Url

The Graph endpoint to invoke.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: 0
  IsRequired: true
  ValueFromPipeline: true
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
