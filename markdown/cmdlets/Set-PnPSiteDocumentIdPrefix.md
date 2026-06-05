---
Module Name: PnP.PowerShell
title: Set-PnPSiteDocumentIdPrefix
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPSiteDocumentIdPrefix.html
---
 
# Set-PnPSiteDocumentIdPrefix

## SYNOPSIS
Allows the document Id prefix for a site to be changed.

## SYNTAX

```powershell
Set-PnPSiteDocumentIdPrefix -DocumentIdPrefix <string> [-ScheduleAssignment <boolean>] [-OverwriteExistingIds <boolean>] [-Verbose] [-Connection <PnPConnection>]
```

## DESCRIPTION
This cmdlet allows changing of the document Id prefix that has been assigned to a site. It essentially does what you can also do using the page /_layouts/15/DocIdSettings.aspx in the SharePoint Online web interface.

It also offers an option to reset the currently assigned document Ids of all files within a site.

You need to be connected to the sitecollection of which you want to change the document Id prefix.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPSiteDocumentIdPrefix "TEST"
```

This will change the document Id prefix to TEST for all new documents created in the current site collection.

### EXAMPLE 2
```powershell
Set-PnPSiteDocumentIdPrefix "TEST" -ScheduleAssignment $true -OverwriteExistingIds $true
```

This will change the document Id prefix to TEST for all existing and new documents in the current site collection. Note that this will take a while (possibly up to 48 hours) to complete as SharePoint Online will need to recalculate and reassign the unique document Id of all files in the site collection.

## PARAMETERS

### -DocumentIdPrefix
The new prefix you would like to start using for the Document ID feature in the current site collection.

The Document ID prefix must be 4 to 12 characters long, and contain only digits (0-9) and letters.

```yaml
Type: string
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: True
Accept wildcard characters: False
```

### -OverwriteExistingIds
Boolean indicating whether the document Ids on existing documents within the site collection should be reassigned a new document Id using the new prefix. If set to true, all existing documents will be assigned a new document Id using the new prefix. If set to false, only new documents will be assigned a document Id using the new prefix.

```yaml
Type: boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -ScheduleAssignment
Boolean indicating whether the change should be scheduled in a timerjob or not. If set to true, the change will be scheduled and will take effect within 48 hours. This might make the rename process more reliable, especially on sites with a lot of files. If set to false, the change might be applied sooner, though still could take some time to be processed.

```yaml
Type: boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)