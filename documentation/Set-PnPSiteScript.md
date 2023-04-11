---
Module Name: PnP.PowerShell
title: Set-PnPSiteScript
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPSiteScript.html
---
 
# Set-PnPSiteScript

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Updates an existing site script on the current tenant.

## SYNTAX

```powershell
Set-PnPSiteScript -Identity <TenantSiteScriptPipeBind> [-Title <String>] [-Description <String>]
 [-Content <String>] [-Version <Int32>] [-Connection <PnPConnection>]   
```

## DESCRIPTION
This cmdlet updates an existing site script.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPSiteScript -Identity f1d55d9b-b116-4f54-bc00-164a51e7e47f -Title "My Site Script"
```

Updates an existing site script and changes the title.

### EXAMPLE 2
```powershell
$script = Get-PnPSiteScript -Identity f1d55d9b-b116-4f54-bc00-164a51e7e47f 
Set-PnPSiteScript -Identity $script -Title "My Site Script"
```

Updates an existing site script and changes the title.

### EXAMPLE 3
```powershell
$content = Get-PnPSiteScriptFromWeb -Url https://contoso.sharepoint.com/sites/SampleSite -IncludeAll 
Set-PnPSiteScript -Identity f1d55d9b-b116-4f54-bc00-164a51e7e47f -Content $content
```

Updates an existing site script and its components.

## PARAMETERS

### -Connection
Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.

```yaml
Type: PnPConnection
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Content
A JSON string containing the site script.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Description
The description of the site script.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Identity
The guid or an object representing the site script.

```yaml
Type: TenantSiteScriptPipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Title
The title of the site script.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Version
Specifies the version of the site script.

```yaml
Type: Int32
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

