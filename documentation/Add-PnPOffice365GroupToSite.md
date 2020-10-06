---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/add-pnpoffice365grouptosite
schema: 2.0.0
title: Add-PnPOffice365GroupToSite
---

# Add-PnPOffice365GroupToSite

## SYNOPSIS
Groupifies a classic team site by creating an Office 365 group for it and connecting the site with the newly created group

## SYNTAX

## DESCRIPTION
This command allows you to add an Office 365 Unified group to an existing classic site collection.

## EXAMPLES

### EXAMPLE 1
```powershell
Add-PnPOffice365GroupToSite -Url "https://contoso.sharepoint.com/sites/FinanceTeamsite" -Alias "FinanceTeamsite" -DisplayName = "My finance team site group"
```

This will add a group called MyGroup to the current site collection

## PARAMETERS

## RELATED LINKS

[SharePoint Developer Patterns and Practices](https://aka.ms/sppnp)