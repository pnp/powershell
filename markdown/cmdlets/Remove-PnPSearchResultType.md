---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPSearchResultType.html
external help file: PnP.PowerShell.dll-Help.xml
title: Remove-PnPSearchResultType
---

# Remove-PnPSearchResultType

## SYNOPSIS

**Required Permissions**

  * [Graph Connector Service (GCS) API](#prerequisites)
  * Site scope: Site Administrator
  * Organization scope: Search Administrator or Global Administrator

Removes a Microsoft Search result type from the currently connected site or at the organization level.

## SYNTAX

```powershell
Remove-PnPSearchResultType -Identity <String> [-Scope <SearchVerticalScope>] [-Force] [-WhatIf] [-Confirm] [-Verbose] [-Connection <PnPConnection>]
```

## DESCRIPTION

This cmdlet removes a Microsoft Search result type from the site you are currently connected to, or at the organization (tenant) level when using `-Scope Organization`. It uses the Graph Connector Service (GCS) API at gcs.office.com.

> [!WARNING]
> This cmdlet uses the Graph Connector Service (GCS) API, which is an internal Microsoft API that is not publicly documented or officially supported. It may change without notice.

### Prerequisites

Your Entra app registration must have the `ExternalConnection.ReadWrite.All` delegated permission from the Graph Connector Service (GCS) API. To add this permission using Azure CLI:

```bash
az ad app permission add --id <your-app-id> --api 56c1da01-2129-48f7-9355-af6d59d42766 --api-permissions d44774bd-e26c-43b1-996d-51bb90a9078e=Scope
az ad app permission admin-consent --id <your-app-id>
```

> [!NOTE]
> This cmdlet requires a **delegated (interactive)** connection. App-only (certificate-based) connections are not supported by the GCS API and will result in a 403 Forbidden error.

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPSearchResultType -Identity "1770839639348_FYXB8XQI5"
```

Removes the specified search result type from the current site.

### EXAMPLE 2
```powershell
Remove-PnPSearchResultType -Identity "1770839639348_FYXB8XQI5" -Scope Organization
```

Removes the specified organization-level search result type.

### EXAMPLE 3
```powershell
Get-PnPSearchResultType | Where-Object { $_.Payload.IsActive -eq $false } | Remove-PnPSearchResultType
```

Removes all inactive result types from the current site. The `LogicalId` property from each piped `SearchResultType` object binds automatically to `-Identity`.

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

### -Identity
The logical ID or name of the search result type to remove.

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

### -Scope
Specifies whether to remove a site-level or organization-level result type. Defaults to Site.

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

### -Force
When provided, no confirmation prompt will be shown before removing the result type.

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
