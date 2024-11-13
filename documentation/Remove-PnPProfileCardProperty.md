---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPProfileCardProperty.html
external help file: PnP.PowerShell.dll-Help.xml
title: Remove-PnPProfileCardProperty
---
  
# Remove-PnPProfileCardProperty

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : PeopleSettings.ReadWrite.All

Removes a specific property from user profile card

## SYNTAX

```powershell
Remove-PnPProfileCardProperty -PropertyName <ProfileCardPropertyName> [-Verbose] [-Connection <PnPConnection>] 
```

## DESCRIPTION

This cmdlet can be used to remove a property from user profile card

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPProfileCardProperty -PropertyName CustomAttribute1
```

This will remove the connection to the external datasource with the specified identity from Microsoft Search.

## PARAMETERS

### -Connection
Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing [Get-PnPConnection](Get-PnPConnection.md).

```yaml
Type: PnPConnection
Parameter Sets: (All)
Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Identity
Unique identifier or an instance of the external connection in Microsoft Search that needs to be removed.

```yaml
Type: SearchExternalConnectionPipeBind
Parameter Sets: (All)
Required: True
Position: Named
Default value: None
Accept pipeline input: True
Accept wildcard characters: False
```

### -Verbose
When provided, additional debug statements will be shown while executing the cmdlet.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
[Microsoft Graph documentation](https://learn.microsoft.com/en-us/graph/add-properties-profilecard)