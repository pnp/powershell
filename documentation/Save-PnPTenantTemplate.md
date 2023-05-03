---
Module Name: PnP.PowerShell
title: Save-PnPTenantTemplate
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Save-PnPTenantTemplate.html
---
 
# Save-PnPTenantTemplate

## SYNOPSIS
Saves a PnP provisioning hierarchy to the file system.

## SYNTAX

```powershell
Save-PnPTenantTemplate -Template <ProvisioningHierarchyPipeBind> [-Out] <String>
 [-Schema <XMLPnPSchemaVersion>] [-Force] 
```

## DESCRIPTION
This cmdlet saves the tenant template for provisioning hierarchy to the file system in the latest or specified schema.

## EXAMPLES

### EXAMPLE 1
```powershell
Save-PnPTenantTemplate -Template template.xml -Out .\tenanttemplate.pnp
```

Saves a PnP tenant template to the file system.

### EXAMPLE 2
```powershell
$template = Read-PnPTenantTemplate -Path template.xml
Save-PnPTenantTemplate -Template $template -Out .\template.pnp
```

Saves a PnP tenant template to the file system as a PnP file. The schema used will be the latest released schema when creating the PnP file regardless of the original schema.

### EXAMPLE 3
```powershell
$template = Read-PnPTenantTemplate -Path template.xml
Save-PnPTenantTemplate -Template $template -Out .\template.pnp -Schema V202002
```

Saves a PnP tenant template to the file system as a PnP file and converts the template in the PnP file to the specified schema.

### EXAMPLE 4
```powershell
Read-PnPTenantTemplate -Path template.xml | Save-PnPTenantTemplate -Out .\template.pnp
```

Saves a PnP tenant template to the file system as a PnP file.

## PARAMETERS

### -Force
Specifying the Force parameter will skip the confirmation question.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Out
Filename to write to, optionally including full path.

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Schema
The optional schema to use when creating the PnP file. Always defaults to the latest schema.

```yaml
Type: XMLPnPSchemaVersion
Parameter Sets: (All)
Accepted values: LATEST, V201503, V201505, V201508, V201512, V201605, V201705, V201801, V201805, V201807, V201903, V201909, V202002

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Template
Allows you to provide an in-memory instance of a Tenant Template or a filename of a template file in XML format. When using this parameter, the -Out parameter refers to the path for saving the template and storing any supporting file for the template.

```yaml
Type: ProvisioningHierarchyPipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

