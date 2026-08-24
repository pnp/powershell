---
Module Name: PnP.PowerShell
title: Get-PnPPageCopyProgress
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPPageCopyProgress.html
---
 
# Get-PnPPageCopyProgress

## SYNOPSIS
Allows checking the progress of a site page move or copy operation from one site to another site.

## SYNTAX

```powershell
Get-PnPPageCopyProgress -DestinationSite <SPOSitePipeBind> -WorkItemId <Guid>
```

## DESCRIPTION
This command allows checking the progress of a site page move or copy operation from one site to another site. The destination site can be specified using the -DestinationSite parameter, and the work item ID can be specified using the -WorkItemId parameter.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPPageCopyProgress -DestinationSite https://tenant.sharepoint.com -WorkItemId 12345678-1234-1234-1234-123456789012
```

Retrieves the progress of the page copy operation with the specified work item ID to the destination site.

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

### -DestinationSite
The destination site to which the page is being moved or copied. This can be specified as a URL or a site object.

```yaml
Type: SPOSitePipeBind
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -WorkItemId
The work item ID of the page copy operation. This is a GUID that uniquely identifies the copy operation and is returned when the copy or move operation is initiated using [Copy-PnPPage](Copy-PnPPage.md) and [Move-PnPPage](Move-PnPPage.md).

```yaml
Type: Guid
Parameter Sets: (All)

Required: True
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[SharePoint Online Management Shell equivallent](https://learn.microsoft.com/powershell/module/sharepoint-online/get-spopersonalsitepagecopyprogress)
[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)