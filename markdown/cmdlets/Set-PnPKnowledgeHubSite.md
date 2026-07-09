---
Module Name: PnP.PowerShell
title: Set-PnPKnowledgeHubSite
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPKnowledgeHubSite.html
---
 
# Set-PnPKnowledgeHubSite

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Sets the Knowledge Hub Site for your tenant.

## SYNTAX

```powershell
Set-PnPKnowledgeHubSite -KnowledgeHubSiteUrl <String> [-Connection <PnPConnection>] 
```

## DESCRIPTION

Allows to set Knowledge Hub Site of the current tenant.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPKnowledgeHubSite -KnowledgeHubSiteUrl "https://yoursite.sharepoint.com/sites/knowledge"
```

Sets the Knowledge Hub Site for your tenant.

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

### -KnowledgeHubSiteUrl
Specifies the URL of the site to be set as the Knowledge Hub Site.

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

