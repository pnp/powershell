---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Add-PnPTenantSequenceSubSite.html
external help file: PnP.PowerShell.dll-Help.xml
title: Add-PnPTenantSequenceSubSite
---
  
# Add-PnPTenantSequenceSubSite

## SYNOPSIS
Adds a tenant sequence sub site object to a tenant sequence site object

## SYNTAX

```powershell
Add-PnPTenantSequenceSubSite -SubSite <TeamNoGroupSubSite> -Site <SiteCollection>  
 
```

## DESCRIPTION

Allows to add a tenant sequence sub site object to a tenant sequence site object.

## EXAMPLES

### EXAMPLE 1
```powershell
Add-PnPTenantSequenceSubSite -Site $mysite -SubSite $mysubsite
```

Adds an existing subsite object to an existing sequence site object

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

### -Site
The site to add the subsite to

```yaml
Type: SiteCollection
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -SubSite
The subsite to add

```yaml
Type: TeamNoGroupSubSite
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)


