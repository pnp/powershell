---
tags: Available in the current Nightly Release only.
external help file: PnP.PowerShell.dll-Help.xml
schema: 2.0.0
online version: https://pnp.github.io/powershell/cmdlets/Export-PnPFlow.html
title: Export-PnPFlow
Module Name: PnP.PowerShell
applicable: SharePoint Online
---
   
# Export-PnPFlow

## SYNOPSIS

**Required Permissions**

* Azure: management.azure.com
* PowerApps: service.powerapps.com (when exporting as a ZIP package)

Exports a Microsoft Power Automate Flow

## SYNTAX

### As ZIP Package
```powershell
Export-PnPFlow [-Environment <PowerAutomateEnvironmentPipeBind>] -Identity <PowerAutomateFlowPipeBind>
 [-AsZipPackage] [-PackageDisplayName <String>] [-PackageDescription <String>] [-PackageCreatedBy <String>]
 [-PackageSourceEnvironment <String>] [-OutPath <String>] [-Force] [-Connection <PnPConnection>]
 
```

### As Json
```powershell
Export-PnPFlow [-Environment <PowerAutomateEnvironmentPipeBind>] -Identity <PowerAutomateFlowPipeBind>
 [-Connection <PnPConnection>] 
```

## DESCRIPTION
This cmdlet exports a Microsoft Power Automate Flow either as a json file or as a zip package.

Exporting a Microsoft Power Automate Flow might fail due to stale connections, SharePoint sites that no longer exist, or other configuration errors in the Flow. ZIP package export failures are written to the PowerShell error stream. By default, these errors are non-terminating so batch exports can continue. Use `-ErrorVariable` to capture them, or `-ErrorAction Stop` to handle a failed export with `try`/`catch`.

The cmdlet uses the module's shared HTTP behavior. Throttled requests (HTTP 429) are retried automatically; other failures, such as server errors or timeouts, are reported immediately without a retry. There are no timeout or retry settings specific to this cmdlet.

## EXAMPLES

### Example 1
```powershell
Export-PnPFlow -Environment (Get-PnPPowerPlatformEnvironment -Identity "myenvironment") -Identity fba63225-baf9-4d76-86a1-1b42c917a182
```

This will export the specified Microsoft Power Automate Flow from the specified Power Platform environment as an output to the current output of PowerShell

### Example 2
```powershell
Export-PnPFlow -Environment (Get-PnPPowerPlatformEnvironment -IsDefault) -Identity fba63225-baf9-4d76-86a1-1b42c917a182
```

This will export the specified Microsoft Power Automate Flow from the default Power Platform environment as an output to the current output of PowerShell

### Example 3
```powershell
Get-PnPPowerPlatformEnvironment | foreach { Get-PnPFlow -Environment $_.Name } | foreach { Export-PnPFlow -Environment $_.Properties.EnvironmentDetails.Name -Identity $_ -OutPath "c:\flows\$($_.Name).zip" -AsZipPackage }
```

This will export all the Microsoft Power Automate Flows available within the tenant from all users from all the available Power Platform environments as a ZIP package for each of them to a local folder c:\flows

### Example 4
```powershell
$exportErrors = @()
Export-PnPFlow -Environment "myenvironment" -Identity fba63225-baf9-4d76-86a1-1b42c917a182 -OutPath "c:\flows\flow.zip" -AsZipPackage -ErrorAction Continue -ErrorVariable +exportErrors
$exportErrors | Out-File "c:\flows\export-errors.log"
```

This attempts to export the Flow and captures any export failure in the `$exportErrors` variable for logging.

### Example 5
```powershell
try {
    Export-PnPFlow -Environment "myenvironment" -Identity fba63225-baf9-4d76-86a1-1b42c917a182 -OutPath "c:\flows\flow.zip" -AsZipPackage -ErrorAction Stop
}
catch {
    Write-Host "Flow export failed: $($_.Exception.Message)"
}
```

This turns an export failure into a terminating error so it can be handled with `try`/`catch`.

## PARAMETERS

### -AsZipPackage
If specified the flow will be exported as a zip package

```yaml
Type: SwitchParameter
Parameter Sets: As ZIP Package
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Connection
Optional connection to be used by the cmdlet.
Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.

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

### -Environment
The name of the Power Platform environment or an Environment instance. If omitted, the default environment will be used.

```yaml
Type: PowerPlatformEnvironmentPipeBind
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: The default environment
Accept pipeline input: True
Accept wildcard characters: False
```

### -Force
If specified and the file exported already exists it will be overwritten without confirmation.

```yaml
Type: SwitchParameter
Parameter Sets: As ZIP Package
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Identity
The value of the Name property of a Microsoft Power Automate Flow that you wish to export

```yaml
Type: PowerAutomateFlowPipeBind
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -OutPath
Optional file name of the file to export to. If not provided, it will store the ZIP package to the current location from where the cmdlet is being run, using the filename returned by the service. Either way, when a file with that name already exists you are asked to confirm the overwrite unless `-Force` is specified.

```yaml
Type: String
Parameter Sets: As ZIP Package
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -PackageCreatedBy
The name of the person to be used as the creator of the exported package

```yaml
Type: String
Parameter Sets: As ZIP Package
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -PackageDescription
The description to use in the exported package

```yaml
Type: String
Parameter Sets: As ZIP Package
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -PackageDisplayName
The display name to use in the exported package

```yaml
Type: String
Parameter Sets: As ZIP Package
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -PackageSourceEnvironment
The name of the source environment from which the exported package was taken

```yaml
Type: String
Parameter Sets: As ZIP Package
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp) 

