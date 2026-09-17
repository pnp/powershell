---
Module Name: PnP.PowerShell
title: Remove-PnPStoredCredential
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPStoredCredential.html
---
 
# Remove-PnPStoredCredential

## SYNOPSIS
Removes a stored credential

## SYNTAX

```powershell
Remove-PnPStoredCredential -Name <String> [-Force] 
```

## DESCRIPTION
Removes a stored credential. If a default vault has been registered through `Microsoft.PowerShell.SecretManagement`, the credential is removed from that vault. If not, it is removed from the credential store native to the operating system: the Windows Credential Manager on Windows, the Keychain on macOS and the Secret Service on Linux. See [Credential Management](https://pnp.github.io/powershell/articles/credentialmanagement.html) for the details.

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPStoredCredential -Name "https://tenant.sharepoint.com"
```

Removes the specified credential from the credential store

## PARAMETERS

### -Force
If specified you will not be asked for confirmation

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Name
The credential to remove

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

