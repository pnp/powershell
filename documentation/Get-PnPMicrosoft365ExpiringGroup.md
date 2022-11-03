---
Module Name: PnP.PowerShell
title: Get-PnPMicrosoft365ExpiringGroup
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPMicrosoft365ExpiringGroup.html
---
 
# Get-PnPMicrosoft365ExpiringGroup

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : One of Directory.Read.All, Directory.ReadWrite.All, Group.Read.All, Group.ReadWrite.All, GroupMember.Read.All, GroupMember.ReadWrite.All

Gets all expiring Microsoft 365 Groups.

## SYNTAX

```powershell
Get-PnPMicrosoft365ExpiringGroup [-Limit <Int32>]  [<CommonParameters>]
```

## DESCRIPTION
This command returns all expiring Microsoft 365 Groups within certain time. By default, groups expiring in the next 31 days are return (in accordance with SharePoint/OneDrive's retention period's 31-day months).

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPMicrosoft365ExpiringGroup
```

Returns all Groups expiring within 31 days (roughly 1 month).

### EXAMPLE 2
```powershell
Get-PnPMicrosoft365ExpiringGroup -Limit 93
```

Returns all Microsoft 365 Groups expiring in 93 days (roughly 3 months)


## PARAMETERS

### -Limit

Limits Groups to be returned to Groups expiring in as many days.

```yaml
Type: Int32
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
