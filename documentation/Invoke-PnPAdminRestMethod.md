---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Invoke-PnPAdminRestMethod.html
external help file: PnP.PowerShell.dll-Help.xml
title: Invoke-PnPAdminRestMethod
---
 
# Invoke-PnPAdminRestMethod

## SYNOPSIS
Invokes a REST request towards a SharePoint admin site.

## SYNTAX 

```powershell
Invoke-PnPAdminRestMethod -Url <String>
                          [-Method <HttpRequestMethod>]
                          [-Content <Object>]
                          [-ContentType <String>]
                          [-Raw]
                          [-Connection <PnPConnection>]
                          [-ResponseHeadersVariable <String>]
                          [-Batch <PnPBatch>]
```

## DESCRIPTION
Invokes a REST request towards a SharePoint admin site.

## EXAMPLES

### EXAMPLE 1
```powershell
Invoke-PnPAdminRestMethod -Url "/_api/StorageQuotas()?api-version=1.3.2"
```

This example retrieves the storage quota from the tenant admin site.

## PARAMETERS

### -Content
A string or object to send.

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

