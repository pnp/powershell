---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPAppErrors.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPAppErrors
---
  
# Get-PnPAppErrors

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Returns application errors.

## SYNTAX

```powershell
Get-PnPAppErrors -ProductId <Guid> [-StartTimeInUtc <DateTime>] [-EndTimeInUtc <DateTime>]
```

## DESCRIPTION
The Get-PnPAppErrors cmdlet returns application monitoring errors (if available) for the application that is specified by ProductId between StartTimeInUtc in Coordinated Universal Time (UTC) and EndTimeInUtc in UTC.

Based on server configuration, errors are available for a limited time. The default is seven days. Older errors are purged. Date time values that are older than 50 years or later than 20 years from today are considered invalid

Each error includes the error message, time in UTC that error happened, the site where the error happened, and the error type. Values for error type are as follows: 0 - None, 1 - Install Error, 2 - Upgrade Error, 3 - Runtime Error.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPAppErrors -ProductId a2681b0c-84fe-41bf-9a8e-d480ab81ba7b
```

This example returns a collection of monitoring error messages for the application with the specified id.

### EXAMPLE 2
```powershell
Get-PnPAppErrors -ProductId a2681b0c-84fe-41bf-9a8e-d480ab81ba7b -StartTimeInUtc (Get-Date).AddHours(-1).ToUniversalTime()
```

This example returns a collection of monitoring error messages for the last hour for the application with the specified id.

## PARAMETERS

### -ProductId
Specifies the application id

```yaml
Type: Guid
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -StartTimeInUtc
Specifies the start time in UTC to search for monitoring errors. If not start time is given the default value of 72 hours before the current time is used.

```yaml
Type: DateTime
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -EndTimeInUtc
Specifies the end time in UTC to search for monitoring errors. If not start time is given the current time is used.

```yaml
Type: DateTime
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)


