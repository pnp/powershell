---
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPContainerUser.html
external help file: PnP.PowerShell.dll-Help.xml
title: Remove-PnPContainerUser
schema: 2.0.0
Module Name: PnP.PowerShell
tags: Available in the current Nightly Release only.
applicable: SharePoint Online
---
 
# Remove-PnPContainerUser

## SYNOPSIS

**Required Permissions**

* SharePoint Embedded Administrator or Global Administrator role is required

Removes a user from a Container in SharePoint Embedded.

## SYNTAX

```powershell
Remove-PnPContainerUser [-Identity] <ContainerPipeBind> -LoginName <String> -Role <ContainerRole> [-WhatIf] [-Confirm] [-Connection <PnPConnection>]
```

## DESCRIPTION

Removes the specified role of the user from the Container. Use Get-PnPContainerUser to see the role a user has; the users it returns can be piped in. Asks for confirmation for every user unless `-Confirm:$false` is used. SharePoint Online does not let you remove your own role from a Container.

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPContainerUser -Identity "b!aBrXSxKDdUKZsaK3Djug6C5rF4MG3pRBomypnjOHiSrjkM_EBk_1S57U3gD7oW-1" -LoginName "john@contoso.com" -Role Writer
```

Removes john@contoso.com, who has the Writer role, from the specified Container.

### EXAMPLE 2
```powershell
Get-PnPContainer -OwningApplicationId a187e399-0c36-4b98-8f04-1edc167a0996 | Get-PnPContainerUser | Where-Object LoginName -eq "john@contoso.com" | Remove-PnPContainerUser -Confirm:$false
```

Removes john@contoso.com from every Container of the specified SharePoint Embedded application, whatever their role, without asking for confirmation.

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

The role the user has on the Container, to remove:
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

