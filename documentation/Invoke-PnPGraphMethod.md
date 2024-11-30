---
Module Name: PnP.PowerShell
title: Invoke-PnPGraphMethod
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Invoke-PnPGraphMethod.html
---

# Invoke-PnPGraphMethod

## SYNOPSIS
Invokes a REST request towards the Microsoft Graph API

## SYNTAX

### Out to console (Default)
```powershell
Invoke-PnPGraphMethod -Url <String>
                      [-AdditionalHeaders GraphAdditionalHeadersPipeBind]
                      [[-Method] <HttpRequestMethod>] 
                      [-Content <Object>] 
                      [-ContentType <String>] 
                      [-ConsistencyLevelEventual] 
                      [-Raw]
                      [-All] 
                      [-Connection <PnPConnection>]
                      [-Verbose]
```

### Out to file
```powershell
Invoke-PnPGraphMethod -Url <String>
                      [-AdditionalHeaders GraphAdditionalHeadersPipeBind]
                      [[-Method] <HttpRequestMethod>] 
                      [-Content <Object>] 
                      [-ContentType <String>] 
                      [-ConsistencyLevelEventual] 
                      [-Connection <PnPConnection>]
                      [-OutFile <String>]
                      [-Verbose]
```

### Out to stream
```powershell
Invoke-PnPGraphMethod -Url <String>
                      [-AdditionalHeaders GraphAdditionalHeadersPipeBind]
                      [[-Method] <HttpRequestMethod>] 
                      [-Content <Object>] 
                      [-ContentType <String>] 
                      [-ConsistencyLevelEventual] 
                      [-Connection <PnPConnection>]
                      [-OutStream]
                      [-Verbose]
```

### Batch
```powershell
Invoke-PnPGraphMethod -Url <String>
                      [-AdditionalHeaders GraphAdditionalHeadersPipeBind]
                      [[-Method] <HttpRequestMethod>] 
                      [-Content <Object>] 
                      [-ContentType <String>] 
                      [-ConsistencyLevelEventual] 
                      [-Connection <PnPConnection>]
                      [-Batch <PnPBatch>]                      
                      [-Verbose]
```

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

### EXAMPLE 9
```powershell
$batch = New-PnPBatch -RetainRequests
Invoke-PnPSPRestMethod -Method Get -Url "https://graph.microsoft.com/v1.0/users" -Batch $batch
Invoke-PnPSPRestMethod -Method Get -Url "https://graph.microsoft.com/v1.0/groups" -Batch $batch
$response = Invoke-PnPBatch $batch -Details
$response
```

This example executes a GET request to get all users and a groups in a single batch request.
It is necessary to create and invoke batch requests in the manner specified here if you want to process something later on with the response object.

## PARAMETERS

### -AdditionalHeaders
Additional request headers, either by providing a Dictionary<string,string> or a Hastable, .e.g -AdditionalHeaders @{"If-Match"="234567tysfssdvsadf"}

```yaml
Type: GraphAdditionalHeaderPipeBind
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -All
Retrieve all pages of results. This will loop through all @odata.nextLink. This flag will only be respected if the request is a GET request.

```yaml
Type: SwitchParameter
Parameter Sets: Out to console
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Connection
Optional connection to be used by the cmdlet.
Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.

```yaml
Type: PnPConnection
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ConsistencyLevelEventual
Set the ConsistencyLevel header to eventual for advanced query capabilities on Azure AD directory objects


```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Content
A string or object to send

```yaml
Type: Object
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ContentType
The content type of the object to send. Defaults to 'application/json'.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Method
The HTTP method to execute. Defaults to GET.

```yaml
Type: HttpRequestMethod
Parameter Sets: (All)
Aliases:
Accepted values: Default, Get, Head, Post, Put, Delete, Trace, Options, Merge, Patch

Required: False
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -OutFile
The full path including filename to write the output to, i.e. c:\temp\myfile.txt. Existing files will be overwritten.

```yaml
Type: String
Parameter Sets: Out to file
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -OutStream
Indicates that the result of the request should be returned as a memory stream.

```yaml
Type: String
Parameter Sets: Out to stream
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Raw
If specified the returned data will not be converted to an object but returned as a JSON string.

```yaml
Type: SwitchParameter
Parameter Sets: Out to console
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Url
The Graph endpoint to invoke.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Batch

The batch to add this request to.

```yaml
Type: PnPBatch
Parameter Sets: Batched

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)