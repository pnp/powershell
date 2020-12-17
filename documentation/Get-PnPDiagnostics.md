---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/get-pnpdiagnostics
schema: 2.0.0
title: Get-PnPDiagnostics
---

# Get-PnPDiagnostics

## SYNOPSIS
Returns diagnostic information

## SYNTAX

```powershell
Get-PnPDiagnostics
```

## DESCRIPTION
This cmdlet returns diagnostic information which can help to determine issues with PnP PowerShell

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPDiagnostic
```

Returns basic diagnostic information, including the last error, the stacktrace, if applicable, etc.

### EXAMPLE 2
```powershell
Add-PnPApp -Path ./myapp.sppkg -Publish
```

This will upload the specified app package to the app catalog and deploy/trust it at the same time.

### EXAMPLE 3
```powershell
Add-PnPApp -Path ./myapp.sppkg -Scope Site -Publish
```

This will upload the specified app package to the site collection app catalog and deploy/trust it at the same time.

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

### -Overwrite
Overwrites the existing app package if it already exists

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Path
Specifies the Id or an actual app metadata instance

```yaml
Type: String
Parameter Sets: (All)
Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Publish
This will deploy/trust an app into the app catalog

```yaml
Type: SwitchParameter
Parameter Sets: Add and Publish
Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Scope
Defines which app catalog to use. Defaults to Tenant

```yaml
Type: AppCatalogScope
Parameter Sets: (All)
Accepted values: Tenant, Site
Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SkipFeatureDeployment

```yaml
Type: SwitchParameter
Parameter Sets: Add and Publish
Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Timeout
Specifies the timeout in seconds. Defaults to 200.

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