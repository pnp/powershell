---
external help file:
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/enable-pnpinplacerecordsmanagementforsite
applicable: SharePoint Online
schema: 2.0.0
title: Enable-PnPInPlaceRecordsManagementForSite
---

# Enable-PnPInPlaceRecordsManagementForSite

## SYNOPSIS
Enables in place records management for a site.

## SYNTAX 

```powershell
Enable-PnPInPlaceRecordsManagementForSite [-Connection <PnPConnection>]
```

## EXAMPLES

### ------------------EXAMPLE 1------------------
```powershell
Enable-PnPInPlaceRecordsManagementForSite
```

The in place records management feature will be enabled and the in place record management will be enabled in all locations with record declaration allowed by all contributors and undeclaration by site admins

## PARAMETERS

### -Connection
Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.

```yaml
Type: PnPConnection
Parameter Sets: (All)

Required: False
Position: Named
Accept pipeline input: False
```

## RELATED LINKS

[SharePoint Developer Patterns and Practices](https://aka.ms/sppnp)