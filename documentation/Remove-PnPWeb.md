---
Module Name: PnP.PowerShell
title: Remove-PnPWeb
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPWeb.html
---

# Remove-PnPWeb

## SYNOPSIS

Removes a subsite.

## SYNTAX

```powershell
Remove-PnPWeb -Identity <WebPipeBind> [-Force] [-Connection <PnPConnection>] 
```

## DESCRIPTION
This cmdlet removes the specified subsite.

## EXAMPLES

### EXAMPLE 1

```powershell
Remove-PnPWeb -Identity projectA
```

Removes the subsite called projectA and will ask for confirmation before doing so.

### EXAMPLE 2

```powershell
Remove-PnPWeb -Identity 5fecaf67-6b9e-4691-a0ff-518fc9839aa0
```

Removes the subsite with the provided Id and will ask for confirmation before doing so.

### EXAMPLE 3

```powershell
Get-PnPSubWeb | Remove-PnPWeb -Force
```

Removes all subsites while not asking for confirmation to do so.

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

Do not ask for confirmation to delete the subweb.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Identity

The identifier of a subsite, the subsite instance or name of the subsite.

```yaml
Type: WebPipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
