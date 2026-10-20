---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Copy-PnPItemProxy.html
external help file: PnP.PowerShell.dll-Help.xml
title: Copy-PnPItemProxy
---
  
# Copy-PnPItemProxy

## SYNOPSIS
Copies an item from one location to another. It is an alias of the `Copy-Item` cmdlet.

## SYNTAX

```powershell
Copy-Item
    [-Path] <String[]>
    [[-Destination] <String>]
    [-Container]
    [-Force]
    [-Filter <String>]
    [-Include <String[]>]
    [-Exclude <String[]>]
    [-Recurse]
    [-PassThru]
    [-Credential <PSCredential>]    
    [-Confirm]
    [-FromSession <PSSession>]
    [-ToSession <PSSession>]
    
```

## DESCRIPTION

**This cmdlet is an alias of the `Copy-Item` cmdlet that is natively available with PowerShell**.

The `Copy-PnPItemProxy` cmdlet copies an item from one location to another location in the same namespace. For instance, it can copy a file to a folder, but it can't copy a file to a certificate drive.

This cmdlet doesn't cut or delete the items being copied. The particular items that the cmdlet can copy depend on the PowerShell provider that exposes the item. For instance, it can copy files and directories in a file system drive and registry keys and entries in the registry drive.

This cmdlet can copy and rename items in the same command. To rename an item, enter the new name in the value of the Destination parameter. To rename an item and not copy it, use the Rename-Item cmdlet.

For more information and details, please refer to the official PowerShell documentation [here](https://learn.microsoft.com/powershell/module/microsoft.powershell.management/copy-item).

## EXAMPLES

### EXAMPLE 1
```powershell
Copy-PnPItemProxy "C:\Users\Admin\seattle.master" -Destination "C:\Presentation"
```

This example copies the `seattle.master` file to the `C:\Presentation` directory. The original file isn't deleted.
For more examples, please refer to the link mentioned above.

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)