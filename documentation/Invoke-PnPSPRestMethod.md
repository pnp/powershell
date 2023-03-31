---
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Invoke-PnPSPRestMethod.html
external help file: PnP.PowerShell.dll-Help.xml
title: Invoke-PnPSPRestMethod
---
 
# Invoke-PnPSPRestMethod

## SYNOPSIS
Invokes a REST request towards a SharePoint site

## SYNTAX 

```powershell
Invoke-PnPSPRestMethod -Url <String>
                       [-Method <HttpRequestMethod>]
                       [-Content <Object>]
                       [-ContentType <String>]
                       [-Raw]
                       [-Connection <PnPConnection>]
                       [-ResponseHeadersVariable <String>]
```

## DESCRIPTION
Invokes a REST request towards a SharePoint site

## EXAMPLES

### ------------------EXAMPLE 1------------------
```powershell
Invoke-PnPSPRestMethod -Url /_api/web
```

This example executes a GET request towards the current site collection and returns the properties of the current web

### ------------------EXAMPLE 2------------------
```powershell
$output = Invoke-PnPSPRestMethod -Url '/_api/web/lists?$select=Id,Title'
$output.value
```

This example executes a GET request towards the current site collection and returns the id and title of all the lists and outputs them to the console. Notice the use of single quotes. If you want to use double quotes (") then you will have to escape the $ character with a backtick: `$

### ------------------EXAMPLE 3------------------
```powershell
$item = @{Title="Test"}
Invoke-PnPSPRestMethod -Method Post -Url "/_api/web/lists/GetByTitle('Test')/items" -Content $item
```

This example creates a new item in the list 'Test' and sets the title field to 'Test'

### ------------------EXAMPLE 4------------------
```powershell
$item = "{'Title':'Test'}"
Invoke-PnPSPRestMethod -Method Post -Url "/_api/web/lists/GetByTitle('Test')/items" -Content $item
```

This example creates a new item in the list 'Test' and sets the title field to 'Test'

### ------------------EXAMPLE 5------------------
```powershell
$item = "{ '__metadata': { 'type': 'SP.Data.TestListItem' }, 'Title': 'Test'}"
Invoke-PnPSPRestMethod -Method Post -Url "/_api/web/lists/GetByTitle('Test')/items" -Content $item -ContentType "application/json;odata=verbose"
```

This example creates a new item in the list 'Test' and sets the title field to 'Test'

### ------------------EXAMPLE 6------------------
```powershell
$output = Invoke-PnPSPRestMethod -Url '/_api/web/lists?$select=Id,Title' -ResponseHeadersVariable headers
$output.value
$headers
```

This example executes a GET request towards the current site collection and returns the id and title of all the lists and outputs them to the console. Notice the use of single quotes. If you want to use double quotes (") then you will have to escape the $ character with a backtick: `$

It will also store the response headers values in the PowerShell variable name that you specify. Enter a variable name without the dollar sign ($) symbol.

## PARAMETERS

### -Content
A string or object to send

```yaml
Type: Object
Parameter Sets: (All)

Required: False
Position: Named
Accept pipeline input: False
```

### -ContentType
The content type of the object to send. Defaults to 'application/json'.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Accept pipeline input: False
```

### -Method
The Http method to execute. Defaults to GET.

```yaml
Type: HttpRequestMethod
Parameter Sets: (All)

Required: False
Position: 0
Accept pipeline input: False
```

### -Url
The url to execute

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: 0
Accept pipeline input: False
```

### -Raw
If specified the returned data will not be converted to an object but returned as a JSON string.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: True
Position: 0
Accept pipeline input: False
```

### -Accept
The Accept HTTP request header. Defaults to 'application/json;odata=nometadata'.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Accept pipeline input: False
```

### -Connection
Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.

```yaml
Type: PnPConnection
Parameter Sets: (All)

Required: False
Position: Named
Accept pipeline input: False
```

### -ResponseHeadersVariable
Creates a variable containing a Response Headers Dictionary. Enter a variable name without the dollar sign ($) symbol. The keys of the dictionary contain the field names and values of the Response Header returned by the web server.

```yaml
Type: String
Parameter Sets: (All)
Required: False
Position: Named
Accept pipeline input: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

