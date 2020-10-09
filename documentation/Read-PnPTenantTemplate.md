---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/read-pnptenanttemplate
schema: 2.0.0
title: Read-PnPTenantTemplate
---

# Read-PnPTenantTemplate

## SYNOPSIS
Loads/Reads a PnP tenant template from the file system and returns an in-memory instance of this template.

## SYNTAX

```powershell
Read-PnPTenantTemplate [-Path] <String> [-TemplateProviderExtensions <ITemplateProviderExtension[]>]
 [<CommonParameters>]
```

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Read-PnPTenantTemplate -Path template.pnp
```

Reads a PnP tenant template file from the file system and returns an in-memory instance

## PARAMETERS

### -Path
Filename to read from, optionally including full path.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TemplateProviderExtensions
Allows you to specify ITemplateProviderExtension to execute while loading the template.

```yaml
Type: ITemplateProviderExtension[]
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[SharePoint Developer Patterns and Practices](https://aka.ms/sppnp)