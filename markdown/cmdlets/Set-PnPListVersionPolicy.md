---
Module Name: PnP.PowerShell
title: Set-PnPListVersionPolicy
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPListVersionPolicy.html
---

# Set-PnPListVersionPolicy

## SYNOPSIS
Sets, synchronizes, or removes file version policy settings on a SharePoint Online document library.

## SYNTAX

### Set Policy

```powershell
Set-PnPListVersionPolicy
 -Identity <ListPipeBind>
 -EnableAutoExpirationVersionTrim <Boolean>
 [-Site <SitePipeBind>]
 [-ExpireVersionsAfterDays <Int32>]
 [-MajorVersionLimit <Int32>]
 [-MajorWithMinorVersionsLimit <Int32>]
 [-FileTypes <String[]>]
 [-NoWait]
 [-Connection <PnPConnection>]
```

### Sync Policy

```powershell
Set-PnPListVersionPolicy
 -Identity <ListPipeBind>
 [-Site <SitePipeBind>]
 -Sync
 [-FileTypes <String[]>]
 [-ExcludeDefaultPolicy]
 [-NoWait]
 [-Connection <PnPConnection>]
```

### Remove File Type Overrides

```powershell
Set-PnPListVersionPolicy
 -Identity <ListPipeBind>
 [-Site <SitePipeBind>]
 -RemoveVersionExpirationFileTypeOverride <String[]>
 [-NoWait]
 [-Connection <PnPConnection>]
```

## DESCRIPTION
Configures the versioning policy for a document library in the currently connected site. The cmdlet resolves the library from the active SharePoint connection and uses the SharePoint Online admin APIs to apply the requested library version policy action. By default it waits for the tenant operation to complete. Use `-NoWait` to return immediately after the operation has been queued.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPListVersionPolicy -Identity "Documents" -EnableAutoExpirationVersionTrim $true
```

Enables automatic version expiration for the specified document library.

### EXAMPLE 2
```powershell
Set-PnPListVersionPolicy -Identity "Documents" -EnableAutoExpirationVersionTrim $false -ExpireVersionsAfterDays 180 -MajorVersionLimit 100 -MajorWithMinorVersionsLimit 20
```

Sets a manual version policy for the specified document library.

### EXAMPLE 3
```powershell
Set-PnPListVersionPolicy -Identity "Documents" -EnableAutoExpirationVersionTrim $false -ExpireVersionsAfterDays 180 -MajorVersionLimit 100 -MajorWithMinorVersionsLimit 20 -FileTypes "pdf","docx"
```

Sets a manual version policy override for the specified file types in the document library.

### EXAMPLE 4
```powershell
Set-PnPListVersionPolicy -Identity "Documents" -Sync -FileTypes "pdf","docx"
```

Synchronizes the version policy for the specified file types in the document library.

### EXAMPLE 5
```powershell
Set-PnPListVersionPolicy -Identity "Documents" -Sync -ExcludeDefaultPolicy
```

Synchronizes the document library version policy while excluding the default policy.

### EXAMPLE 6
```powershell
Set-PnPListVersionPolicy -Identity "Documents" -RemoveVersionExpirationFileTypeOverride "pdf","docx"
```

Removes version policy file type overrides from the specified document library.

### EXAMPLE 7
```powershell
Set-PnPListVersionPolicy -Site "https://contoso.sharepoint.com/sites/project-x" -Identity "Documents" -EnableAutoExpirationVersionTrim $true
```

Enables automatic version expiration for a document library on a specific site.

### EXAMPLE 8
```powershell
Set-PnPListVersionPolicy -Identity "Documents" -Sync -NoWait
```

Queues the library version policy synchronization request and returns immediately without waiting for the tenant operation to complete.

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

### -EnableAutoExpirationVersionTrim
Enables or disables AutoExpiration version trimming on the document library. When set to `$false`, you must also specify `ExpireVersionsAfterDays`, `MajorVersionLimit`, and `MajorWithMinorVersionsLimit`.

```yaml
Type: Boolean
Parameter Sets: Set Policy

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ExcludeDefaultPolicy
Excludes the default policy while synchronizing the document library version policy.

```yaml
Type: SwitchParameter
Parameter Sets: Sync Policy

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ExpireVersionsAfterDays
The number of days after which versions expire. Specify `0` for no expiration, or a value from `30` through `36500`.

```yaml
Type: Nullable`1[Int32]
Parameter Sets: Set Policy

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -FileTypes
The file types for which the library version policy should be applied or synchronized.

```yaml
Type: String[]
Parameter Sets: Set Policy, Sync Policy

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Identity
The document library to update. You can provide the library title, id, url, or a list instance.

```yaml
Type: ListPipeBind
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: True
Accept wildcard characters: False
```

### -MajorVersionLimit
The maximum number of major versions to keep. Specify a value from `1` through `50000`.

```yaml
Type: Nullable`1[Int32]
Parameter Sets: Set Policy

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -MajorWithMinorVersionsLimit
The maximum number of major versions for which minor versions are retained. Specify a value from `0` through `50000`.

```yaml
Type: Nullable`1[Int32]
Parameter Sets: Set Policy

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -NoWait
Queues the version policy operation and returns immediately instead of waiting for the SharePoint Online tenant operation to complete.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Site
Optional target site containing the document library. When omitted, the cmdlet uses the currently connected SharePoint site.

```yaml
Type: SitePipeBind
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -RemoveVersionExpirationFileTypeOverride
Removes version expiration overrides for the specified file types from the document library.

```yaml
Type: String[]
Parameter Sets: Remove File Type Overrides

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Sync
Synchronizes the document library version policy.

```yaml
Type: SwitchParameter
Parameter Sets: Sync Policy

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```
## RELATED LINKS
[Get-PnPListVersionPolicy](Get-PnPListVersionPolicy.md)