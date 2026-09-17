---
Module Name: PnP.PowerShell
title: Get-PnPTimeZoneId
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPTimeZoneId.html
---
 
# Get-PnPTimeZoneId

## SYNOPSIS
Returns a time zone ID

## SYNTAX

```powershell
Get-PnPTimeZoneId [[-Match] <String>] 
```

## DESCRIPTION
In order to create a new classic site you need to specify the timezone this site will use. Use the cmdlet to retrieve a list of possible values.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPTimeZoneId
```

This will return all time zone IDs in use by Office 365.

### EXAMPLE 2
```powershell
Get-PnPTimeZoneId -Match Stockholm
```

This will return the time zone IDs for Stockholm

## PARAMETERS

### -Match
A string to search for like 'Stockholm'

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

