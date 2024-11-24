---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Remove-PnPTenantSyncClientRestriction.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Remove-PnPTenantSyncClientRestriction
---

# Remove-PnPTenantSyncClientRestriction

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Removes organization-level OneDrive synchronization restrictions

## SYNTAX

### Default (Default)

```
Remove-PnPTenantSyncClientRestriction
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

The Remove-PnPTenantSyncClientRestriction cmdlet disables the feature for the tenant, but does not remove any present domain GUID entries from the safe sender recipient list. After the Remove-PnPTenantSyncClientRestriction cmdlet is run it can take up to 24 hours for change to take effect. This parameter will also remove any values set from the GrooveBlockOption parameter for syncing.

## EXAMPLES

## PARAMETERS

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
