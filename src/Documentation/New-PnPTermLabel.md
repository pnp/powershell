---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/new-pnptermlabel
schema: 2.0.0
title: New-PnPTermLabel
---

# New-PnPTermLabel

## SYNOPSIS
Creates a localized label for a taxonomy term

## SYNTAX

```
New-PnPTermLabel
 [-Term] <PnP.PowerShell.Commands.Base.PipeBinds.TaxonomyItemPipeBind`1[Microsoft.SharePoint.Client.Taxonomy.Term]>
 -Name <String> -Lcid <Int32> [-IsDefault] [-Connection <PnPConnection>] [<CommonParameters>]
```

## DESCRIPTION
Creates a localized label for a taxonomy term. Use Get-PnPTerm -Include Labels to request the current labels on a taxonomy term.

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

## PARAMETERS

### -Connection
Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.

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

### -IsDefault
Makes this new label the default label

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

### -Lcid
The locale id to use for the localized term

```yaml
Type: Int32
Parameter Sets: (All)
Aliases:

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
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Term
The term to add the localized label to

```yaml
Type: PnP.PowerShell.Commands.Base.PipeBinds.TaxonomyItemPipeBind`1[Microsoft.SharePoint.Client.Taxonomy.Term]
Parameter Sets: (All)
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

## RELATED LINKS

[SharePoint Developer Patterns and Practices](https://aka.ms/sppnp)