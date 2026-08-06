---
tags: Available in the current Nightly Release only.
external help file: PnP.PowerShell.dll-Help.xml
schema: 2.0.0
online version: https://pnp.github.io/powershell/cmdlets/Import-PnPFlow.html
title: Import-PnPFlow
Module Name: PnP.PowerShell
applicable: SharePoint Online
---
   
# Import-PnPFlow

## SYNOPSIS

**Required Permissions**

* Azure: management.azure.com
* PowerApps: service.powerapps.com

Imports a Microsoft Power Automate Flow.

## SYNTAX

### With Zip Package
```powershell
Import-PnPFlow [-Environment <PowerAutomateEnvironmentPipeBind>] [-PackagePath <String>] [-Name <String>] [-RetryCount <Int32>] [-Delay <Int32>] [-Connection <PnPConnection>]
 
```

## DESCRIPTION
This cmdlet imports a Microsoft Power Automate Flow from a ZIP package. At present, only flows originating from the same tenant are supported.

Importing a Flow might fail due to stale connections, SharePoint sites that no longer exist, or other configuration errors in the Flow. A failed import raises a terminating error describing the cause, so the cmdlet no longer completes silently. Use `-Verbose` to display progress details while the package is processed.

## EXAMPLES

### Example 1
```powershell
Import-PnPFlow -Environment (Get-PnPPowerPlatformEnvironment -Identity "myenvironment") -PackagePath C:\Temp\Export-ReEnableFlow_20250414140636.zip -Name NewFlowName
```

This will Import the specified Microsoft Power Automate Flow from the specified Power Platform environment as an output to the current output of PowerShell

### Example 2
```powershell
Import-PnPFlow -Environment (Get-PnPPowerPlatformEnvironment -IsDefault) -PackagePath C:\Temp\Export-ReEnableFlow_20250414140636.zip -Name NewFlowName
```

This will Import the specified Microsoft Power Automate Flow from the default Power Platform environment as an output to the current output of PowerShell

### Example 3
```powershell
Import-PnPFlow -PackagePath C:\Temp\Export-ReEnableFlow_20250414140636.zip -Name NewFlowName
```

This will Import a flow to the default environment. The flow will be imported as a zip package. The name of the flow will be set to NewFlowName.

### Example 4
```powershell
Import-PnPFlow -PackagePath C:\Temp\Export-ReEnableFlow_20250414140636.zip -Name NewFlowName -Verbose
```

This will Import a flow to the default environment. The flow will be imported as a zip package. The name of the flow will be set to NewFlowName. The `-Verbose` flag displays progress details during the import.

### Example 5
```powershell
Import-PnPFlow -PackagePath C:\Temp\Export-ReEnableFlow_20250414140636.zip -Name NewFlowName -RetryCount 15 -Delay 3000
```

This will Import a flow to the default environment with a custom retry count and delay between polling attempts.

## PARAMETERS

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

### -Delay
Delay in milliseconds between attempts to poll for the import parameters. Accepts a value between 500 and 300000. If omitted, the cmdlet uses its default delay policy which is 5000ms.

```yaml
Type: Int32
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

### -Name
The new name of the flow.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: true
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -PackagePath
Local path of the .zip package to import. The path must be a valid path on the local file system.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: true
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -RetryCount
Number of times to poll for the import parameters to become available. Accepts a value between 1 and 100. If omitted, the cmdlet uses its default retry policy which is 10.

```yaml
Type: Int32
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp) 

