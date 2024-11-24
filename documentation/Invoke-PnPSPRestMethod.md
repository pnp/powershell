---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Invoke-PnPSPRestMethod.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Invoke-PnPSPRestMethod
---

# Invoke-PnPSPRestMethod

## SYNOPSIS

Invokes a REST request towards a SharePoint site.

## SYNTAX

### Default (Default)

```
Invoke-PnPSPRestMethod -Url <String> [-Method <HttpRequestMethod>] [-Content <Object>]
 [-ContentType <String>] [-Raw] [-Connection <PnPConnection>] [-ResponseHeadersVariable <String>]
 [-Batch <PnPBatch>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Invokes a REST request towards a SharePoint site.

## EXAMPLES

### EXAMPLE 1

```powershell
Invoke-PnPSPRestMethod -Url /_api/web
```

This example executes a GET request towards the current site collection and returns the properties of the current web.

### EXAMPLE 2

```powershell
$output = Invoke-PnPSPRestMethod -Url '/_api/web/lists?$select=Id,Title'
$output.value
```

This example executes a GET request towards the current site collection and returns the id and title of all the lists and outputs them to the console. Notice the use of single quotes. If you want to use double quotes (") then you will have to escape the $ character with a backtick: `$

### EXAMPLE 3

```powershell
$item = @{Title="Test"}
Invoke-PnPSPRestMethod -Method Post -Url "/_api/web/lists/GetByTitle('Test')/items" -Content $item
```

This example creates a new item in the list 'Test' and sets the title field to 'Test'.

### EXAMPLE 4

```powershell
$item = "{'Title':'Test'}"
Invoke-PnPSPRestMethod -Method Post -Url "/_api/web/lists/GetByTitle('Test')/items" -Content $item
```

This example creates a new item in the list 'Test' and sets the title field to 'Test'.

### EXAMPLE 5

```powershell
$item = "{ '__metadata': { 'type': 'SP.Data.TestListItem' }, 'Title': 'Test'}"
Invoke-PnPSPRestMethod -Method Post -Url "/_api/web/lists/GetByTitle('Test')/items" -Content $item -ContentType "application/json;odata=verbose"
```

This example creates a new item in the list 'Test' and sets the title field to 'Test'.

### EXAMPLE 6

```powershell
$output = Invoke-PnPSPRestMethod -Url '/_api/web/lists?$select=Id,Title' -ResponseHeadersVariable headers
$output.value
$headers
```

This example executes a GET request towards the current site collection and returns the id and title of all the lists and outputs them to the console. Notice the use of single quotes. If you want to use double quotes (") then you will have to escape the $ character with a backtick: `$

It will also store the response headers values in the PowerShell variable name that you specify. Enter a variable name without the dollar sign ($) symbol.

### EXAMPLE 7

```powershell
$batch = New-PnPBatch -RetainRequests
Invoke-PnPSPRestMethod -Method Get -Url "https://tenant.sharepoint.com/sites/mysite/_api/web/lists" -Batch $batch
$item = "{'Title':'Test'}"
Invoke-PnPSPRestMethod -Method Post -Url "https://tenant.sharepoint.com/sites/mysite/_api/web/lists/GetByTitle('Test')/items" -Content $item -Batch $batch
$response = Invoke-PnPBatch $batch -Details
$response
```

This example executes a GET request to get all lists and a POST request to add an item to a list in a single batch request.
It is necessary to create and invoke batch requests in the manner specified here if you want to process something later on with the response object.

## PARAMETERS

### -Accept

The Accept HTTP request header. Defaults to 'application/json;odata=nometadata'.

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

### -Connection

Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.

```yaml
Type: PnPConnection
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

### -Content

A string or object to send.

```yaml
Type: Object
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

### -ContentType

The content type of the object to send. Defaults to 'application/json'.

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

### -Method

The Http method to execute. Defaults to GET.

```yaml
Type: HttpRequestMethod
DefaultValue: ''
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
AcceptedValues: []
HelpMessage: ''
```

### -Raw

If specified the returned data will not be converted to an object but returned as a JSON string.

```yaml
Type: SwitchParameter
DefaultValue: ''
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

### -ResponseHeadersVariable

Creates a variable containing a Response Headers Dictionary. Enter a variable name without the dollar sign ($) symbol. The keys of the dictionary contain the field names and values of the Response Header returned by the web server.

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

### -Url

The url to execute

```yaml
Type: String
DefaultValue: ''
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
