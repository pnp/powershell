---
Module Name: PnP.PowerShell
title: Get-PnPMicrosoft365Roadmap
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPMicrosoft365Roadmap.html
---
 
# Get-PnPMicrosoft365Roadmap

## SYNOPSIS

**Required Permissions**

  * None

Returns all items currently on the Microsoft 365 Roadmap

## SYNTAX

```powershell
Get-PnPMicrosoft365Roadmap [-RoadmapUrl <string>] [-Verbose] 
```

## DESCRIPTION

Allows retrieval of the Microsoft 365 Roadmap. Pipe the output to i.e. a Where-Object to filter it down to the desired items. You do not need to be connected to any tenant to utilize this cmdlet.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPMicrosoft365Roadmap
```

Retrieves all the available items currently on the Microsoft 365 Roadmap

### EXAMPLE 2
```powershell
Get-PnPMicrosoft365Roadmap | Where-Object { $_.Status -eq "Rolling out" }
```

Retrieves the items on the Microsoft 365 Roadmap which are currently rolling out

### EXAMPLE 3
```powershell
Get-PnPMicrosoft365Roadmap | Where-Object { $_.Created -ge (Get-Date).AddDays(-7) -or $_.Modified -ge (Get-Date).AddDays(-7) }
```

Retrieves the items on the Microsoft 365 Roadmap which have been added or modified in the last 7 days

## PARAMETERS

### -RoadmapUrl
Allows passing in a custom URL to retrieve the roadmap from. It will use the default value listed below if it is not provided. In most cases you don't want to provide this parameter yourself.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: https://www.microsoft.com/releasecommunications/api/v1/m365
Accept pipeline input: False
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
[Microsoft 365 Roadmap](https://www.microsoft.com/microsoft-365/roadmap)