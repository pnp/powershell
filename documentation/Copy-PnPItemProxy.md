---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Copy-PnPItemProxy.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Copy-PnPItemProxy
---

# Copy-PnPItemProxy

## SYNOPSIS

Copies an item from one location to another. It is an alias of the `Copy-Item` cmdlet.

## SYNTAX

### Default (Default)

```
Copy-Item
 [-Path] <String[]> [[-Destination] <String>] [-Container] [-Force] [-Filter <String>]
 [-Include <String[]>] [-Exclude <String[]>] [-Recurse] [-PassThru] [-Credential <PSCredential>]
 [-Confirm] [-FromSession <PSSession>] [-ToSession <PSSession>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

**This cmdlet is an alias of the `Copy-Item` cmdlet that is natively available with PowerShell**.

The `Copy-PnPItemProxy` cmdlet copies an item from one location to another location in the same namespace. For instance, it can copy a file to a folder, but it can't copy a file to a certificate drive.

This cmdlet doesn't cut or delete the items being copied. The particular items that the cmdlet can copy depend on the PowerShell provider that exposes the item. For instance, it can copy files and directories in a file system drive and registry keys and entries in the registry drive.

This cmdlet can copy and rename items in the same command. To rename an item, enter the new name in the value of the Destination parameter. To rename an item and not copy it, use the Rename-Item cmdlet.

For more information and details, please refer to the official PowerShell documentation [here](https://learn.microsoft.com/powershell/module/microsoft.powershell.management/copy-item).

## EXAMPLES

## PARAMETERS

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
