---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Import-PnPFlow.html
external help file: PnP.PowerShell.dll-Help.xml
title: Import-PnPFlow
---
  
# Import-PnPFlow

## SYNOPSIS

**Required Permissions**

* Azure: management.azure.com

Imports a Microsoft Power Automate Flow.

## SYNTAX

### With Zip Package
```powershell
Import-PnPFlow [-Environment <PowerAutomateEnvironmentPipeBind>] [-PackagePath <String>] [-Name <String>] [-Connection <PnPConnection>]
 
```

## DESCRIPTION
This cmdlet Imports a Microsoft Power Automate Flow from a zip package.

Many times Importing a Microsoft Power Automate Flow will not be possible due to various reasons such as connections having gone stale, SharePoint sites referenced no longer existing or other configuration errors in the Flow. To display these errors when trying to Import a Flow, provide the -Verbose flag with your Import request. If not provided, these errors will silently be ignored.

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

This will Import a flow to the default environment. The flow will be imported as a zip package. The name of the flow will be set to NewFlowName. With the -Verbose flag, any errors that occur during the import process will be displayed in the console.

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

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp) 