---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPContainerUser.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPContainerUser
---

# Get-PnPContainerUser

## SYNOPSIS

**Required Permissions**

* SharePoint Embedded Administrator or Global Administrator role is required

Returns the users of a Container in SharePoint Embedded and their roles.

## SYNTAX

```powershell
Get-PnPContainerUser [-Identity] <ContainerPipeBind> [-Connection <PnPConnection>]
```

## DESCRIPTION

Returns one entry for every user with the Owner, Manager, Writer or Reader role on the Container.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPContainerUser -Identity "b!aBrXSxKDdUKZsaK3Djug6C5rF4MG3pRBomypnjOHiSrjkM_EBk_1S57U3gD7oW-1"
```

Returns the users of the specified Container.

### EXAMPLE 2
```powershell
Get-PnPContainer -OwningApplicationId a187e399-0c36-4b98-8f04-1edc167a0996 | Get-PnPContainerUser | Where-Object Role -eq Owner
```

Returns the owners of every Container of the specified SharePoint Embedded application.

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

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
