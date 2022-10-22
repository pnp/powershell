---
Module Name: PnP.PowerShell
title: Get-PnPDeletedTeam
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPDeletedTeam.html
---
 
# Get-PnPDeletedTeam

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : Team.ReadBasic.All

Gets list of deleted Teams team.

## SYNTAX

```powershell
Get-PnPDeletedTeam
```

## DESCRIPTION

Allows to retrieve list of deleted Microsoft Teams teams

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPDeletedTeam
```

Retrieves all the deleted Microsoft Teams.

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

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)