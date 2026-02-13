---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPSearchVertical.html
external help file: PnP.PowerShell.dll-Help.xml
title: Remove-PnPSearchVertical
---

# Remove-PnPSearchVertical

## SYNOPSIS

**Required Permissions**

  * [Graph Connector Service (GCS) API](#prerequisites)
  * Site scope: Site Administrator
  * Organization scope: Search Administrator or Global Administrator

Removes a Microsoft Search vertical from the currently connected site or at the organization level.

## SYNTAX

```powershell
Remove-PnPSearchVertical -Identity <String> [-Scope <SearchVerticalScope>] [-Force] [-WhatIf] [-Confirm] [-Verbose] [-Connection <PnPConnection>]
```

## DESCRIPTION

This cmdlet removes a Microsoft Search vertical from the site you are currently connected to, or at the organization (tenant) level when using `-Scope Organization`. It uses the Graph Connector Service (GCS) API at gcs.office.com.

> [!WARNING]
> This cmdlet uses the Graph Connector Service (GCS) API, which is an internal Microsoft API that is not publicly documented or officially supported. It may change without notice.

### Prerequisites

Your Entra app registration must have the `ExternalConnection.ReadWrite.All` delegated permission from the Graph Connector Service (GCS) API. To add this permission using Azure CLI:

```bash
az ad app permission add --id <your-app-id> --api 56c1da01-2129-48f7-9355-af6d59d42766 --api-permissions d44774bd-e26c-43b1-996d-51bb90a9078e=Scope
az ad app permission admin-consent --id <your-app-id>
```

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPSearchVertical -Identity "1610383262385_H0RPZO96M"
```

Removes the custom search vertical with the specified logical ID from the currently connected site.

### EXAMPLE 2
```powershell
Remove-PnPSearchVertical -Identity "1644258966832_MFDMSIXCG" -Scope Organization
```

Removes the specified organization-level search vertical.

### EXAMPLE 3
```powershell
Get-PnPSearchVertical | Where-Object { $_.Payload.VerticalType -eq 1 -and $_.Payload.State -eq 0 } | Remove-PnPSearchVertical
```

Removes all disabled custom verticals from the current site. The `LogicalId` property from each piped `SearchVertical` object binds automatically to `-Identity`.

## PARAMETERS

### -Connection
Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing [Get-PnPConnection](Get-PnPConnection.md).

```yaml
Type: PnPConnection
Parameter Sets: (All)
Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Scope
Specifies whether to remove a site-level or organization-level vertical. Defaults to Site.

```yaml
Type: SearchVerticalScope
Parameter Sets: (All)
Accepted values: Site, Organization
Required: False
Position: Named
Default value: Site
Accept pipeline input: False
Accept wildcard characters: False
```

### -Identity
The logical ID of the search vertical to remove. For site scope, built-in IDs include SITEALL, SITEFILES, SITESITES, SITENEWS, and SITEIMAGES. For organization scope, built-in IDs include ALL, FILES, SITES, NEWS, PEOPLE, IMAGES, MESSAGES, and MICROSOFTPOWERBI. Custom verticals have either a user-chosen ID or an auto-generated ID in the format `{timestamp}_{randomId}`.

```yaml
Type: String
Parameter Sets: (All)
Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue, ByPropertyName)
Accept wildcard characters: False
Aliases: LogicalId
```

### -Force
When provided, no confirmation prompt will be shown before removing the vertical.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
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
Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Confirm
Prompts you for confirmation before running the cmdlet.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Verbose
When provided, additional debug statements will be shown while executing the cmdlet.

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
