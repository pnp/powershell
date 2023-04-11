---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Export-PnPFlow.html
external help file: PnP.PowerShell.dll-Help.xml
title: Export-PnPFlow
---
  
# Export-PnPFlow

## SYNOPSIS

**Required Permissions**

* Azure: management.azure.com

Exports a Microsoft Power Automate Flow

## SYNTAX

### As ZIP Package
```powershell
Export-PnPFlow -Environment <PowerAutomateEnvironmentPipeBind> -Identity <PowerAutomateFlowPipeBind>
 [-AsZipPackage] [-PackageDisplayName <String>] [-PackageDescription <String>] [-PackageCreatedBy <String>]
 [-PackageSourceEnvironment <String>] [-OutPath <String>] [-Force] [-Connection <PnPConnection>]
 [<CommonParameters>]
```

### As Json
```powershell
Export-PnPFlow -Environment <PowerAutomateEnvironmentPipeBind> -Identity <PowerAutomateFlowPipeBind>
 [-Connection <PnPConnection>] [<CommonParameters>]
```

## DESCRIPTION
This cmdlet exports a Microsoft Power Automate Flow either as a json file or as a zip package.

Many times exporting a Microsoft Power Automate Flow will not be possible due to various reasons such as connections having gone stale, SharePoint sites referenced no longer existing or other configuration errors in the Flow. To display these errors when trying to export a Flow, provide the -Verbose flag with your export request. If not provided, these errors will silently be ignored.

## EXAMPLES

### Example 1
```powershell
$environment = Get-PnPPowerPlatformEnvironment -IsDefault $true
Export-PnPFlow -Environment $environment -Identity fba63225-baf9-4d76-86a1-1b42c917a182
```

This will export the specified Microsoft Power Automate Flow from the default Power Platform environment as an output to the current output of PowerShell

### Example 2
```powershell
Get-PnPPowerPlatformEnvironment | foreach { Get-PnPFlow -Environment $_.Name } | foreach { Export-PnPFlow -Environment $_.Properties.EnvironmentDetails.Name -Identity $_ -OutPath "c:\flows\$($_.Name).zip" -AsZipPackage }
```

This will export all the Microsoft Power Automate Flows available within the tenant from all users from all the available Power Platform environments as a ZIP package for each of them to a local folder c:\flows

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
The environment which contains the flow.

```yaml
Type: PowerAutomateEnvironmentPipeBind
Parameter Sets: (All)
Aliases:

Required: True
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

### -OutPath
Optional file name of the file to export to. If not provided, it will store the ZIP package to the current location from where the cmdlet is being run.

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