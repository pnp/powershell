---
Module Name: PnP.PowerShell
title: Remove-PnPRoleDefinition
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPRoleDefinition.html
---
 
# Remove-PnPRoleDefinition

## SYNOPSIS
Remove a Role Definition from a site

## SYNTAX

```powershell
Remove-PnPRoleDefinition [-Identity] <RoleDefinitionPipeBind> [-Force] [-Connection <PnPConnection>]
 [<CommonParameters>]
```

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPRoleDefinition -Identity MyRoleDefinition
```

Removes the specified Role Definition (Permission Level) from the current site

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

### -Force
Do not ask for confirmation to delete the role definition

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Identity
The identity of the role definition, either a RoleDefinition object or a the name of roledefinition

```yaml
Type: RoleDefinitionPipeBind
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

