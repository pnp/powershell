---
Module Name: PnP.PowerShell
title: Remove-PnPKnowledgeHubSite
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPKnowledgeHubSite.html
---
 
# Remove-PnPKnowledgeHubSite

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Removes the Knowledge Hub Site setting for your tenant

## SYNTAX

```powershell
Remove-PnPKnowledgeHubSite [-Connection <PnPConnection>] 
```

## DESCRIPTION

Allows to remove Knowledge Hub Site setting for your tenant.

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPKnowledgeHubSite
```

Removes the Knowledge Hub Site setting for your tenant

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

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

