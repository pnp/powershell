---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPContainerTypeConfiguration.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPContainerTypeConfiguration
---
  
# Get-PnPContainerTypeConfiguration

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Returns container type configuration of a SharePoint repository services application.

## SYNTAX

```powershell
Get-PnPContainerTypeConfiguration [[-Identity] <GUID>] [-Connection <PnPConnection>] 
```

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPContainerTypeConfiguration -Identity a187e399-0c36-4b98-8f04-1edc167a0996
```

Returns a container type configuration data of the application created under the specified SharePoint repository services application.


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

### -Identity

Specify container type GUID

```yaml
Type: ContainerPipeBind
Parameter Sets: (All)

Required: False
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)