---
Module Name: PnP.PowerShell
title: Get-PnPRoleDefinition
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPRoleDefinition.html
---
 
# Get-PnPRoleDefinition

## SYNOPSIS
Retrieves a Role Definitions of a site

## SYNTAX

```powershell
Get-PnPRoleDefinition [[-Identity] <RoleDefinitionPipeBind>] [-Connection <PnPConnection>] 
```

## DESCRIPTION

Allows to retrieve Role Definitions of a site.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPRoleDefinition
```

Retrieves the Role Definitions (Permission Levels) settings of the current site

### EXAMPLE 2
```powershell
Get-PnPRoleDefinition -Identity Read
```

Retrieves the specified Role Definition (Permission Level) settings of the current site

### EXAMPLE 3
```powershell
Get-PnPRoleDefinition | Where-Object { $_.RoleTypeKind -eq "Administrator" }
```

Retrieves the Role Definition (Permission Level) settings with the Administrator type, regardless of its name

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
The name of a role definition to retrieve.

```yaml
Type: RoleDefinitionPipeBind
Parameter Sets: (All)

Required: False
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

