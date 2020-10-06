---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/set-pnpminimaldownloadstrategy
schema: 2.0.0
title: Set-PnPMinimalDownloadStrategy
---

# Set-PnPMinimalDownloadStrategy

## SYNOPSIS
Activates or deactivates the minimal downloading strategy.

## SYNTAX

### On
```
Set-PnPMinimalDownloadStrategy [-On] [-Force] [-Web <WebPipeBind>] [-Connection <PnPConnection>]
 [<CommonParameters>]
```

### Off
```
Set-PnPMinimalDownloadStrategy [-Off] [-Force] [-Web <WebPipeBind>] [-Connection <PnPConnection>]
 [<CommonParameters>]
```

## DESCRIPTION
Activates or deactivates the minimal download strategy feature of a site

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
Aliases:

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
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Off
Turn minimal download strategy off

```yaml
Type: SwitchParameter
Parameter Sets: Off
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -On
Turn minimal download strategy on

```yaml
Type: SwitchParameter
Parameter Sets: On
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Web
This parameter allows you to optionally apply the cmdlet action to a subweb within the current web. In most situations this parameter is not required and you can connect to the subweb using Connect-PnPOnline instead. Specify the GUID, server relative url (i.e. /sites/team1) or web instance of the web to apply the command to. Omit this parameter to use the current web.

```yaml
Type: WebPipeBind
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[SharePoint Developer Patterns and Practices](https://aka.ms/sppnp)