---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Export-PnPPowerApp.html
external help file: PnP.PowerShell.dll-Help.xml
title: Export-PnPPowerApp
---
  
# Export-PnPPowerApp

## SYNOPSIS

**Required Permissions**

* Azure: management.azure.com

Exports a Microsoft Power App

## SYNTAX

```
Export-PnPPowerApp -Environment <PowerPlatformEnvironmentPipeBind> -Identity <PowerAppPipeBind>
 [-PackageDisplayName <String>] [-PackageDescription <String>] [-PackageCreatedBy <String>]
 [-PackageSourceEnvironment <String>] [-OutPath <String>] [-Force] [-Connection <PnPConnection>]
 [<CommonParameters>]
```

## DESCRIPTION
This cmdlet exports a Microsoft Power App as zip package.

Many times exporting a Microsoft Power App will not be possible due to various reasons such as connections having gone stale, SharePoint sites referenced no longer existing or other configuration errors in the App. To display these errors when trying to export a App, provide the -Verbose flag with your export request. If not provided, these errors will silently be ignored.

## EXAMPLES

### Example 1
```powershell
$environment = Get-PnPPowerPlatformEnvironment -IsDefault $true
Export-PnPPowerApp -Environment $environment -Identity fba63225-baf9-4d76-86a1-1b42c917a182 -OutPath "C:\Users\user1\Downloads\test_20230408152624.zip"
```

This will export the specified Microsoft Power App from the default Power Platform environment as an output to the path specified in the command as -OutPath

### Example 2
```powershell
$environment = Get-PnPPowerPlatformEnvironment -IsDefault $true
Export-PnPPowerApp -Environment $environment -Identity fba63225-baf9-4d76-86a1-1b42c917a182 -OutPath "C:\Users\user1\Downloads\test_20230408152624.zip" -PackageDisplayName "MyAppDisplayName" -PackageDescription "Package exported using PnP Powershell" -PackageCreatedBy "Siddharth Vaghasia" -PackageSourceEnvironment "UAT Environment"
```
This will export the specified Microsoft Power App from the default Power Platform environment with metadata specified

### Example 3
```powershell
Get-PnPPowerPlatformEnvironment | foreach { Get-PnPPowerApp -Environment $_.Name } | foreach { Export-PnPPowerApp -Environment $_.Properties.EnvironmentDetails.Name -Identity $_ -OutPath "C:\Users\user1\Downloads\$($_.Name).zip" }
```

This will export all the Microsoft Power Apps available within the tenant from all users from all the available Power Platform environments as a ZIP package for each of them to a local folder C:\Users\user1\Downloads

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
The environment which contains the App.

```yaml
Type: PowerPlatformEnvironmentPipeBind
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Identity
The value of the Name property of a Microsoft Power App that you wish to export

```yaml
Type: PowerAppPipeBind
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
Parameter Sets: (All)
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
Parameter Sets: (All)
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
Parameter Sets: (All)
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
Parameter Sets: (All)
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
Parameter Sets: (All)
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