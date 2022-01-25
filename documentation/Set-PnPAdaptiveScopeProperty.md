---
Module Name: PnP.PowerShell
title: Set-PnPAdaptiveScopeProperty
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPAdaptiveScopeProperty.html
---
 
# Set-PnPAdaptiveScopeProperty

## SYNOPSIS
Sets an indexed value to the current web property bag

## SYNTAX

### Web
```powershell
Set-PnPAdaptiveScopeProperty -Key <String> -Value <String>
 [-Connection <PnPConnection>] [<CommonParameters>]
```

## DESCRIPTION

This cmdlet is used to set or create an indexed property bag value for use in [SharePoint site scopes](https://docs.microsoft.com/en-us/microsoft-365/compliance/retention-settings?view=o365-worldwide#configuration-information-for-adaptive-scopes) with [adaptive policy scopes](https://docs.microsoft.com/en-us/microsoft-365/compliance/retention?view=o365-worldwide#adaptive-or-static-policy-scopes-for-retention).  Executing this cmdlet is the equivalent of setting or adding an indexed value to the current web property bag using `Set-PnPPropertyBagValue` with the `-Indexed` parameter.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPAdaptiveScopeProperty -Key MyKey -Value MyValue
```

This sets or adds an indexed value to the current web property bag

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

### -Key

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Value

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

[Microsoft 365 Information Governance](https://docs.microsoft.com/en-us/microsoft-365/compliance/manage-information-governance?view=o365-worldwide)

[Adaptive policy scopes](https://docs.microsoft.com/en-us/microsoft-365/compliance/retention?view=o365-worldwide#adaptive-or-static-policy-scopes-for-retention)