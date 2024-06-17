---
Module Name: PnP.PowerShell
title: Repair-PnPSite
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Repair-PnPSite.html
---
 
# Repair-PnPSite

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Checks and repairs the site collection and its contents.

## SYNTAX

```powershell
Repair-PnPSite -Identity <SitePipeBind> [-RuleId <Guid>] [-RunAlways <SwitchParameter>]
```

## DESCRIPTION
The Repair-PnPSite cmdlet runs one or all site collection health checks on the site collection and its contents. This cmdlet will make changes if issues are found and automatically repairable.

The cmdlet reports the health check rules with a summary of the results. The rules might not support automatic repair. Tests without repair mode can be initiated by running the Test-PnPSite cmdlet.


## EXAMPLES

### EXAMPLE 1
```powershell
Repair-PnPSite -Identity "https://contoso.sharepoint.com/sites/marketing"
```

This example runs all the site collection health checks in repair mode on the https://contoso.sharepoint.com/sites/marketing site collection.

### EXAMPLE 2
```powershell
Repair-PnPSite -Identity "https://contoso.sharepoint.com/sites/marketing" -RuleID "ee967197-ccbe-4c00-88e4-e6fab81145e1"
```

This example runs the Missing Galleries Check rule in repair mode on the https://contoso.sharepoint.com/sites/marketing site collection.

## PARAMETERS

### -Identity
Specifies the SharePoint Online site collection on which to run the repairs.

```yaml
Type: SitePipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -RuleId
Specifies a health check rule to run.

For example:

* `"ee967197-ccbe-4c00-88e4-e6fab81145e1"` for Missing Galleries.
* `"befe203b-a8c0-48c2-b5f0-27c10f9e1622"` for Conflicting Content Types.
* `"a9a6769f-7289-4b9f-ae7f-5db4b997d284"` for Missing Parent Content Types.
* `"5258ccf5-e7d6-4df7-b8ae-12fcc0513ebd"` for Missing Site Templates.
* `"99c946f7-5751-417c-89d3-b9c8bb2d1f66"` for Unsupported Language Pack References.
* `"6da06aab-c539-4e0d-b111-b1da4408859a"` for Unsupported MUI References.

```yaml
Type: Guid
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -RunAlways
Specifies whether the rules will be run as a result of this call or cached results from a previous run can be returned.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

