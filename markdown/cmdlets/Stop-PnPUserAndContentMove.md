---
external help file: PnP.PowerShell.dll-Help.xml
title: Stop-PnPUserAndContentMove
online version: https://pnp.github.io/powershell/cmdlets/Stop-PnPUserAndContentMove.html
applicable: SharePoint Online
tags: Available in the current Nightly Release only.
schema: 2.0.0
Module Name: PnP.PowerShell
---
 
# Stop-PnPUserAndContentMove

## SYNOPSIS
Stops a SharePoint Online multi-geo user and OneDrive content move job.

## SYNTAX

```powershell
Stop-PnPUserAndContentMove [-UserPrincipalName] <String> [-Connection <PnPConnection>]
```

## DESCRIPTION
Stops a SharePoint Online multi-geo move job for a user and the user's OneDrive content.

## EXAMPLES

### EXAMPLE 1

```powershell
Stop-PnPUserAndContentMove -UserPrincipalName user@contoso.com
```

Stops the move job for the specified user.

## PARAMETERS

### -Connection
Optional connection to be used by the cmdlet. Retrieve the value for this parameter by specifying `-ReturnConnection` on `Connect-PnPOnline` or by executing `Get-PnPConnection`.

```yaml
Type: PnPConnection
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -UserPrincipalName
The user principal name of the user whose user and OneDrive content move job should be stopped.

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## OUTPUTS

### System.String
Returns `The given move job has been stopped. Please run start cmdlet to restart the move.` when the move job has been stopped.

## RELATED LINKS

[Get-PnPUserAndContentMoveState](Get-PnPUserAndContentMoveState.md)

[Start-PnPUserAndContentMove](Start-PnPUserAndContentMove.md)

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

