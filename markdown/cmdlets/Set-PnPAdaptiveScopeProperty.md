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
Sets an indexed value to the current web property bag.

## SYNTAX

### Web
```powershell
Set-PnPAdaptiveScopeProperty -Key <String> -Value <String>
 [-Connection <PnPConnection>] 
```

## DESCRIPTION

This cmdlet is used to set or create an indexed property bag value for use in [SharePoint site scopes](https://learn.microsoft.com/microsoft-365/compliance/retention-settings?view=o365-worldwide#configuration-information-for-adaptive-scopes) with [adaptive policy scopes](https://learn.microsoft.com/microsoft-365/compliance/retention?view=o365-worldwide#adaptive-or-static-policy-scopes-for-retention). Executing this cmdlet is similar to setting or adding an indexed value to the current web property bag using `Set-PnPPropertyBagValue` with the `-Indexed` parameter with the addition that it will also ensure the noscript is temporarily disabled to allow for this to happen. It will revert its state back to what it was after adding or updating the property bag value. Using this cmdlet does therefore require having the SharePoint Online Admin role or equivalent app permissions.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPAdaptiveScopeProperty -Key MyKey -Value MyValue
```

This sets or adds an indexed value to the current web property bag.

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
Key of the property to set.

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
Value to set.

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

[Microsoft 365 Information Governance](https://learn.microsoft.com/microsoft-365/compliance/manage-information-governance?view=o365-worldwide)

[Adaptive policy scopes](https://learn.microsoft.com/microsoft-365/compliance/retention?view=o365-worldwide#adaptive-or-static-policy-scopes-for-retention)
