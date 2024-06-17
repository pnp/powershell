---
Module Name: PnP.PowerShell
title: Set-PnPMinimalDownloadStrategy
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPMinimalDownloadStrategy.html
---
 
# Set-PnPMinimalDownloadStrategy

## SYNOPSIS
Activates or deactivates the minimal downloading strategy.

## SYNTAX

### On
```powershell
Set-PnPMinimalDownloadStrategy [-On] [-Force] [-Connection <PnPConnection>]
 
```

### Off
```powershell
Set-PnPMinimalDownloadStrategy [-Off] [-Force] [-Connection <PnPConnection>]
 
```

## DESCRIPTION
Activates or deactivates the minimal download strategy feature of a site.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPMinimalDownloadStrategy -Off
```

Will deactivate minimal download strategy (MDS) for the current web.

### EXAMPLE 2
```powershell
Set-PnPMinimalDownloadStrategy -On
```

Will activate minimal download strategy (MDS) for the current web.

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
Specifies whether to overwrite (when activating) or continue (when deactivating) an existing feature with the same feature identifier. This parameter is ignored if there are no errors.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Off
Turn minimal download strategy off.

```yaml
Type: SwitchParameter
Parameter Sets: Off

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -On
Turn minimal download strategy on.

```yaml
Type: SwitchParameter
Parameter Sets: On

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```



## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

