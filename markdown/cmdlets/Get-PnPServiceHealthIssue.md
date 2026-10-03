---
Module Name: PnP.PowerShell
title: Get-PnPServiceHealthIssue
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPServiceHealthIssue.html
---
 
# Get-PnPServiceHealthIssue

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : ServiceHealth.Read.All

Gets service health issues of the Office 365 Services from the Microsoft Graph API

## SYNTAX

```powershell
Get-PnPServiceHealthIssue [-Identity <Id>] 
```

## DESCRIPTION

Allows to retrieve current service health issues of the Office 365 Services from the Microsoft Graph API.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPServiceHealthIssue
```

Retrieves all the available service health issues

### EXAMPLE 2
```powershell
Get-PnPServiceHealthIssue -Identity "EX123456"
```

Retrieves the details of the service health issue with the Id EX123456

## PARAMETERS

### -Identity
Allows retrieval of a particular service health issue with the provided Id
```yaml
Type: Identity
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)