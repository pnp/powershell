---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Add-PnPSiteCollectionAdmin.html
external help file: PnP.PowerShell.dll-Help.xml
title: Add-PnPSiteCollectionAdmin
---
  
# Add-PnPSiteCollectionAdmin

## SYNOPSIS
Adds one or more users as site collection administrators to the site collection in the current context

## SYNTAX

```powershell
Add-PnPSiteCollectionAdmin [-Owners <System.Collections.Generic.List<[PnP.PowerShell.Commands.Base.PipeBinds.UserPipeBind]>>] [-PrimarySiteCollectionAdmin <PnP.PowerShell.Commands.Base.PipeBinds.UserPipeBind>] [-Verbose] [-Connection <PnPConnection>]
```

## DESCRIPTION
This command allows adding one to many users as site collection administrators to the site collection in the current context. It does not replace or remove existing site collection administrators. You must be a Site Collection Admin to run this command. Use `Set-PnPTenantSite -Owners` if you are not an Admin for the site but have the SharePoint Online admin role. 

## EXAMPLES

### EXAMPLE 1
```powershell
Add-PnPSiteCollectionAdmin -Owners "user@contoso.onmicrosoft.com"
```

This will add user@contoso.onmicrosoft.com as an additional secondary site collection administrator to the site collection in the current context

### EXAMPLE 2
```powershell
Add-PnPSiteCollectionAdmin -Owners @("user1@contoso.onmicrosoft.com", "user2@contoso.onmicrosoft.com")
```

This will add user1@contoso.onmicrosoft.com and user2@contoso.onmicrosoft.com as additional secondary site collection administrator to the site collection in the current context

### EXAMPLE 3
```powershell
Get-PnPUser | Where-Object Title -Like "*Doe" | Add-PnPSiteCollectionAdmin
```

This will add all users with their title ending with "Doe" as additional secondary site collection administrators to the site collection in the current context

### EXAMPLE 4
```powershell
Add-PnPSiteCollectionAdmin -PrimarySiteCollectionAdmin "user@contoso.onmicrosoft.com"
```

This will set user@contoso.onmicrosoft.com as the primary site collection administrator of the site collection in the current context

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
Specifies owner(s) to add as site collection administrators. They will be added as additional secondary site collection administrators to the site in the current context. Existing administrators will stay. Can be both users and groups.

```yaml
Type: System.Collections.Generic.List`1[PnP.PowerShell.Commands.Base.PipeBinds.UserPipeBind]
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -PrimarySiteCollectionAdmin
The user to set as the primary site collection administrator. This will replace the current primary site collection administrator. To add additional site collection administrators, use the -Owners parameter.

```yaml
Type: PnP.PowerShell.Commands.Base.PipeBinds.UserPipeBind
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)