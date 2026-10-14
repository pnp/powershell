---
Module Name: PnP.PowerShell
title: Remove-PnPSiteCollectionAdmin
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPSiteCollectionAdmin.html
---
 
# Remove-PnPSiteCollectionAdmin

## SYNOPSIS
Removes one or more users as site collection administrators from the site collection in the current context

## SYNTAX

```powershell
Remove-PnPSiteCollectionAdmin
 -Owners <System.Collections.Generic.List`1[PnP.PowerShell.Commands.Base.PipeBinds.UserPipeBind]>
 [-Connection <PnPConnection>] 
```

## DESCRIPTION
This command allows removing one to many users as site collection administrators from the site collection in the current context. All existing site collection administrators not included in this command will remain site collection administrator.

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPSiteCollectionAdmin -Owners "user@contoso.onmicrosoft.com"
```

This will remove user@contoso.onmicrosoft.com as a site collection owner from the site collection in the current context

### EXAMPLE 2
```powershell
Remove-PnPSiteCollectionAdmin -Owners @("user1@contoso.onmicrosoft.com", "user2@contoso.onmicrosoft.com")
```

This will remove user1@contoso.onmicrosoft.com and user2@contoso.onmicrosoft.com as site collection owners from the site collection in the current context

### EXAMPLE 3
```powershell
Get-PnPUser | ? Title -Like "*Doe" | Remove-PnPSiteCollectionAdmin
```

This will remove all users with their title ending with "Doe" as site collection owners from the site collection in the current context

### EXAMPLE 4
```powershell
Get-PnPSiteCollectionAdmin | Remove-PnPSiteCollectionAdmin
```

This will remove all existing site collection administrators from the site collection in the current context

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

### -Owners
Specifies owner(s) to remove as site collection administrators. Can be both users and groups.

```yaml
Type: System.Collections.Generic.List`1[PnP.PowerShell.Commands.Base.PipeBinds.UserPipeBind]
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

