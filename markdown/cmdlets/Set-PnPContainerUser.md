---
Module Name: PnP.PowerShell
tags: Available in the current Nightly Release only.
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPContainerUser.html
external help file: PnP.PowerShell.dll-Help.xml
title: Set-PnPContainerUser
---
 
# Set-PnPContainerUser

## SYNOPSIS

**Required Permissions**

* SharePoint Embedded Administrator or Global Administrator role is required

Changes the role of a user of a Container in SharePoint Embedded.

## SYNTAX

```powershell
Set-PnPContainerUser [-Identity] <ContainerPipeBind> -LoginName <String> -Role <ContainerRole> [-WhatIf] [-Confirm] [-Connection <PnPConnection>]
```

## DESCRIPTION

Changes the role the user has on the Container. Users returned by Get-PnPContainerUser can be piped in.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPContainerUser -Identity "b!aBrXSxKDdUKZsaK3Djug6C5rF4MG3pRBomypnjOHiSrjkM_EBk_1S57U3gD7oW-1" -LoginName "john@contoso.com" -Role Manager
```

Changes the role of john@contoso.com on the specified Container to Manager.

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
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -Role

The new role of the user:
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
Accept pipeline input: True (ByPropertyName)
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

