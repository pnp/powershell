---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPPowerAppByPassConsent.html
external help file: PnP.PowerShell.dll-Help.xml
title: Set-PnPPowerAppByPassConsent
---
  
# Set-PnPPowerAppByPassConsent

## SYNOPSIS

**Required Permissions**

* Azure: management.azure.com

Sets the consent bypass flag of a Power Apps for a given environment


## SYNTAX

```powershell
Set-PnPPowerAppByPassConsent [-Environment <PowerPlatformEnvironmentPipeBind>] [-Identity <PowerAppPipeBind> -ByPassConsent <Boolean>] 
[-Connection <PnPConnection>] [-Verbose]
```

## DESCRIPTION
This command can be used to set the bypassConsent flag of an PowerApps to true or false. Set the value as true so users aren't required to authorize API connections for the targeted app. To Remove the consent set the value false so users are required to authorize API connections for the targeted app

## EXAMPLES

### Example 1
```powershell
$environment = Get-PnPPowerPlatformEnvironment
Set-PnPPowerAppByPassConsent -Environment $environment -Identity fba63225-baf9-4d76-86a1-1b42c917a182 -ByPassConsent true
```
This sets the bypassConsent flag on the specified Power App to true

### Example 2
```powershell
Set-PnPPowerAppByPassConsent -Identity fba63225-baf9-4d76-86a1-1b42c917a182 -ByPassConsent false
```
This sets the bypassConsent flag on the specified Power App to flag on the default environment

## PARAMETERS

### -Environment
The name of the Power Platform environment or an Environment object to retrieve the available Power Apps for.

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

### -Identity
The Id of the app to retrieve.

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

### -ByPassConsent
The value to set for the bypassConsent flag of the app.

```yaml
Type: Boolean
Parameter Sets: (All)
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