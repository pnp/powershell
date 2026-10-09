---
Module Name: PnP.PowerShell
tags: Available in the current Nightly Release only.
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Add-PnPContainerUser.html
external help file: PnP.PowerShell.dll-Help.xml
title: Add-PnPContainerUser
---
 
# Add-PnPContainerUser

## SYNOPSIS

**Required Permissions**

* SharePoint Embedded Administrator or Global Administrator role is required

Adds a user to a Container in SharePoint Embedded with the specified role.

## SYNTAX

```powershell
Add-PnPContainerUser [-Identity] <ContainerPipeBind> -LoginName <String> -Role <ContainerRole> [-WhatIf] [-Confirm] [-Connection <PnPConnection>]
```

## DESCRIPTION

Gives the user the specified role on the Container. A user who already has a role on the Container cannot be added again; use Set-PnPContainerUser to change their role instead. Such a failure is reported for that Container only, so other Containers piped in are still processed.

## EXAMPLES

### EXAMPLE 1
```powershell
Add-PnPContainerUser -Identity "b!aBrXSxKDdUKZsaK3Djug6C5rF4MG3pRBomypnjOHiSrjkM_EBk_1S57U3gD7oW-1" -LoginName "john@contoso.com" -Role Writer
```

Gives john@contoso.com the Writer role on the specified Container.

## PARAMETERS

### -Confirm

Prompts you for confirmation before running the cmdlet.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: cf

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

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

The id, the site url or the api url of the Container.

```yaml
Type: ContainerPipeBind
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -LoginName

The login name of the user, such as their user principal name.

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Role

The role to give the user:
- Reader: can read the metadata and contents of the Container.
- Writer: can also modify the metadata and contents of the Container.
- Manager: can also manage the members of the Container.
- Owner: can also delete and restore the Container.

```yaml
Type: ContainerRole
Parameter Sets: (All)
Accepted values: Reader, Writer, Manager, Owner

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -WhatIf

Shows what would happen if the cmdlet runs. The cmdlet is not run.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: wi

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

