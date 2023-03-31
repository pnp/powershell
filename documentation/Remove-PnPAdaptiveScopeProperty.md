---
Module Name: PnP.PowerShell
title: Remove-PnPAdaptiveScopeProperty
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPAdaptiveScopeProperty.html
---
 
# Remove-PnPAdaptiveScopeProperty

## SYNOPSIS
Removes a value from the current web property bag

## SYNTAX

```powershell
Remove-PnPAdaptiveScopeProperty [-Key] <String> [-Force] 
 [-Connection <PnPConnection>]   [<CommonParameters>]
```

## DESCRIPTION

This cmdlet is used to remove a property bag value. Executing this cmdlet removes a value from the current web property bag just like  `Remove-PnPPropertyBagValue` would do, but also takes care of toggling the noscript value to allow for this to be possible in one cmdlet. Using this cmdlet does therefore require having the SharePoint Online Admin role or equivalent app permissions.

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPAdaptiveScopeProperty -Key MyKey
```

This will remove the value with key MyKey from the current web property bag

### EXAMPLE 2
```powershell
Remove-PnPAdaptiveScopeProperty -Key MyKey -Force
```

This will remove the value with key MyKey from the current web property bag without prompting for confirmation

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

### -Force

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Key
Key of the property bag value to be removed

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

[Microsoft 365 Information Governance](https://learn.microsoft.com/en-us/microsoft-365/compliance/manage-information-governance?view=o365-worldwide)

[Adaptive policy scopes](https://learn.microsoft.com/en-us/microsoft-365/compliance/retention?view=o365-worldwide#adaptive-or-static-policy-scopes-for-retention)