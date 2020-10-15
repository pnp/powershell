---
external help file: PnP.PowerShell.dll-Help.xml
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/disable-pnpinplacerecordsmanagementforsite
applicable: SharePoint Online
schema: 2.0.0
title: Disable-PnPInPlaceRecordsManagementForSite
---

# Disable-PnPInPlaceRecordsManagementForSite

## SYNOPSIS
Disables in place records management for a site.

## SYNTAX 

```powershell
Disable-PnPInPlaceRecordsManagementForSite [-Connection <PnPConnection>]
```

## EXAMPLES

### ------------------EXAMPLE 1------------------
```powershell
Disable-PnPInPlaceRecordsManagementForSite
```

The in place records management feature will be disabled

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

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)