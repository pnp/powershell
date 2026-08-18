---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/New-PnPSearchResultTypeRule.html
external help file: PnP.PowerShell.dll-Help.xml
title: New-PnPSearchResultTypeRule
---

# New-PnPSearchResultTypeRule

## SYNOPSIS

Creates a rule object for use with Microsoft Search result types.

## SYNTAX

```powershell
New-PnPSearchResultTypeRule -PropertyName <String> -Operator <SearchResultTypeRuleOperatorType> -Values <String[]>
```

## DESCRIPTION

This cmdlet creates a `SearchResultTypeRule` object that can be passed to `New-PnPSearchResultType -Rules` or used when constructing result type payloads. Rules define conditions that determine which search results match a result type.

## EXAMPLES

### EXAMPLE 1
```powershell
New-PnPSearchResultTypeRule -PropertyName "FileType" -Operator Equals -Values "pdf"
```

Creates a rule that matches results where FileType equals "pdf".

### EXAMPLE 2
```powershell
$rules = @(
    New-PnPSearchResultTypeRule -PropertyName "FileType" -Operator Equals -Values "docx","xlsx"
    New-PnPSearchResultTypeRule -PropertyName "IsListItem" -Operator Equals -Values "true"
)
New-PnPSearchResultType -Name "Office List Items" -Rules $rules
```

Creates multiple rules and uses them to create a result type.

### EXAMPLE 3
```powershell
New-PnPSearchResultTypeRule -PropertyName "IconUrl" -Operator StartsWith -Values "https://"
```

Creates a rule using the StartsWith operator.

## PARAMETERS

### -PropertyName
The property name to match against (e.g., "FileType", "IsListItem", "IconUrl").

```yaml
Type: String
Parameter Sets: (All)
Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Operator
The comparison operator for the rule.

```yaml
Type: SearchResultTypeRuleOperatorType
Parameter Sets: (All)
Accepted values: Equals, NotEquals, Contains, DoesNotContain, LessThan, GreaterThan, StartsWith
Required: True
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Values
One or more values to compare against.

```yaml
Type: String[]
Parameter Sets: (All)
Required: True
Position: 2
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
