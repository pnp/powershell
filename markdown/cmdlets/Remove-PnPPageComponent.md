---
Module Name: PnP.PowerShell
title: Remove-PnPPageComponent
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPPageComponent.html
---
 
# Remove-PnPPageComponent

## SYNOPSIS
Removes a page component from a page.

## SYNTAX

```powershell
Remove-PnPPageComponent [-Page] <PagePipeBind> -InstanceId <Guid> [-Force] [-Connection <PnPConnection>]
```

## DESCRIPTION
This cmdlet removes specified page component from a page.

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPPageComponent -Page Home -InstanceId a2875399-d6ff-43a0-96da-be6ae5875f82
```

Removes the specified control from the page.

### EXAMPLE 2
```powershell
$webpart = Get-PnPPageComponent -Page "Home" | Where-Object { $_.Title -eq "Site activity" }
Remove-PnPPageComponent -Page "Home" -InstanceId $webpart.InstanceId -Force
```

Finds a web part with the title "Site activity" on the Home.aspx page, then removes it from the page.

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
If specified you will not receive the confirmation question.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -InstanceId
The instance id of the component.

```yaml
Type: Guid
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Page
The name of the page.

```yaml
Type: PagePipeBind
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```



## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

