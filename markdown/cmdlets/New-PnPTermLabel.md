---
Module Name: PnP.PowerShell
title: New-PnPTermLabel
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/New-PnPTermLabel.html
---
 
# New-PnPTermLabel

## SYNOPSIS
Creates a localized label for a taxonomy term

## SYNTAX


```powershell
New-PnPTermLabel -Term <TaxonomyTermPipeBind> -Name <String> -Lcid <Int32> [-IsDefault] 
```

## DESCRIPTION
Creates a localized label for a taxonomy term. Use Get-PnPTerm -Include Labels to request the current labels on a taxonomy term. Setting `-IsDefault:$false`, will make a new label a synonym. 

## EXAMPLES

### EXAMPLE 1
```powershell
New-PnPTermLabel -Name "Finanzwesen" -Lcid 1031 -Term (Get-PnPTerm -Identity "Finance" -TermSet "Departments" -TermGroup "Corporate")
```

Creates a new localized taxonomy label in German (LCID 1031) named "Finanzwesen" for the term "Finance" in the termset Departments which is located in the "Corporate" termgroup

### EXAMPLE 2
```powershell
Get-PnPTerm -Identity "Finance" -TermSet "Departments" -TermGroup "Corporate" | New-PnPTermLabel -Name "Finanzwesen" -Lcid 1031
```

Creates a new localized taxonomy label in German (LCID 1031) named "Finanzwesen" for the term "Finance" in the termset Departments which is located in the "Corporate" termgroup

### EXAMPLE 3
```powershell
Get-PnPTerm -Identity "Finance" -TermSet "Departments" -TermGroup "Corporate" | New-PnPTermLabel -Name "Finanzwesen" -Lcid 1031 -IsDefault:$false
```

Creates a new localized taxonomy label in German (LCID 1031) named "Finanzwesen" for the term "Finance" in the termset Departments which is located in the "Corporate" termgroup and make it a synonym.

## PARAMETERS

### -IsDefault
Makes this new label the default label

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Lcid
The locale id to use for the localized term

```yaml
Type: Int32
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Name
The localized name of the term

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Term
The term to add the localized label to.

```yaml
Type: TaxonomyTermPipeBind
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

