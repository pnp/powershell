---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Add-PnPSiteScript.html
external help file: PnP.PowerShell.dll-Help.xml
title: Add-PnPSiteScript
---
  
# Add-PnPSiteScript

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Creates a new Site Script on the current tenant

## SYNTAX

```powershell
Add-PnPSiteScript -Title <String> [-Description <String>] -Content <String> [-Connection <PnPConnection>] 
```

## DESCRIPTION

Allows to add a Site Script on the current tenant.

## EXAMPLES

### EXAMPLE 1
```powershell
Add-PnPSiteScript -Title "My Site Script" -Description "A more detailed description" -Content $script
```

Adds a new Site Script, where $script variable contains the script.

## PARAMETERS

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
A JSON string containing the site script. See the [Microsoft Learn site](https://learn.microsoft.com/sharepoint/dev/declarative-customization/site-design-json-schema) for documentation on how to create such a JSON schema.

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Description
The description of the site script

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Title
The title of the site script

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
[Site template JSON schema documentation](https://learn.microsoft.com/sharepoint/dev/declarative-customization/site-design-json-schema)
