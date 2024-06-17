---
Module Name: PnP.PowerShell
title: Grant-PnPSiteDesignRights
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Grant-PnPSiteDesignRights.html
---
 
# Grant-PnPSiteDesignRights

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Grants the specified principals rights to use the site design.

## SYNTAX

```powershell
Grant-PnPSiteDesignRights [-Identity] <TenantSiteDesignPipeBind> -Principals <String[]>
 [-Rights <TenantSiteDesignPrincipalRights>] [-Connection <PnPConnection>]  
 
```

## DESCRIPTION

Allows to grant permission to use the site design for specified principal.

## EXAMPLES

### EXAMPLE 1
```powershell
Grant-PnPSiteDesignRights -Identity 5c73382d-9643-4aa0-9160-d0cba35e40fd -Principals "myuser@mydomain.com","myotheruser@mydomain.com"
```

Grants the specified principals View rights on the site design specified

### EXAMPLE 2
```powershell
Get-PnPSiteDesign -Title "MySiteDesign" -SiteScriptIds 438548fd-60dd-42cf-b843-2db506c8e259 -WebTemplate TeamSite | Grant-PnPSiteDesignRights -Principals "myuser@mydomain.com","myotheruser@mydomain.com"
```

Grants the specified principals View rights on the site design specified

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
The site design to use.

```yaml
Type: TenantSiteDesignPipeBind
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Principals
One or more principals to grant rights to.

```yaml
Type: String[]
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Rights
The rights to set. Defaults to 'View'

```yaml
Type: TenantSiteDesignPrincipalRights
Parameter Sets: (All)
Accepted values: None, View

Required: False
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

