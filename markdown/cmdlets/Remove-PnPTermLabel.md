---
Module Name: PnP.PowerShell
title: Remove-PnPTermLabel
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPTermLabel.html
---
 
# Remove-PnPTermLabel

## SYNOPSIS
Removes a single label/translation from a term.

## SYNTAX

### By Term Id
```
Remove-PnPTermLabel 
    -Label <String> 
    -Term <Guid> 
    -Lcid <Int32> 
    [-TermStore <Guid>]
    [-Connection <PnPConnection>]    
    [-Force] 
    
```

### By Name
```
Remove-PnPTermLabel 
    -Label <String> 
    -Term <String> 
    -Lcid <Int32> 
    -TermSet <Name|Guid> 
    -TermGroup <Name|Guid> 
    [-TermStore <Guid>]
    [-Connection <PnPConnection>]    
    [-Force]  
    
```

## DESCRIPTION
This cmdlet removes a label/translation from a term.

## EXAMPLES

### Example 1
```powershell
Remove-PnPTermLabel -Label "Marknadsföring" -Lcid 1053 -Term 2d1f298b-804a-4a05-96dc-29b667adec62
```

Removes the Swedish label from the specified term.

### Example 2
```powershell
Remove-PnPTermLabel -Label "Marknadsföring" -Lcid 1053 -Term "Marketing" -TermSet "Departments" -TermGroup "Corporate"
```
Removes the Swedish label from the specified term.

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

### -Label
The label to remove.

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

### -Lcid
Language code identifier of the term label.

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

### -Term
Identity of term to update. Either a name or a GUID.

```yaml
Type: Guid
Parameter Sets: By Term Id
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

```yaml
Type: String
Parameter Sets: By Name
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TermGroup
The term group containing the term set.

```yaml
Type: Guid or String
Parameter Sets: By Name
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -TermSet
The term set containing the term.

```yaml
Type: Guid or String
Parameter Sets: By Name
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -TermStore
The Term store containing the term group.

```yaml
Type: Guid
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
