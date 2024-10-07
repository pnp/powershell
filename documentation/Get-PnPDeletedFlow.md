---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPDeletedFlow.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPDeletedFlow
---
  
# Get-PnPDeletedFlow

## SYNOPSIS

**Required Permissions**

* Azure: management.azure.com

**Information**

* To use this command, you must be a Global or Power Platform administrator.

**Note**

* A Power Automate flow is soft-deleted when:
* It's a non-solution flow.
* It's been deleted less than 21 days ago.

Returns all soft-deleted Power Automate flows within an environment

## SYNTAX

### All (Default)
```powershell
Get-PnPDeletedFlow [-Environment <PowerAutomateEnvironmentPipeBind>] [-Connection <PnPConnection>] [-Verbose]
```


## DESCRIPTION
This cmdlet returns Deleted Power Automate Flows meeting the specified criteria.

## EXAMPLES

### Example 1
```powershell
Get-PnPDeletedFlow
```
Returns all the deleted flows in the default Power Platform environment belonging to any user

### Example 2
```powershell
Get-PnPPowerPlatformEnvironment -Identity "MyOrganization (default)" | Get-PnPDeletedFlow
```
Returns all the deleted  flows for a given Power Platform environment belonging to the any user


## PARAMETERS

### -Environment
The name of the Power Platform environment or an Environment object to retrieve the available flows for.

```yaml
Type: PowerAutomateEnvironmentPipeBind
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: The default environment
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