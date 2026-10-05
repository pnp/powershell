---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Reset-PnPMicrosoft365GroupExpiration.html
external help file: PnP.PowerShell.dll-Help.xml
title: Reset-PnPMicrosoft365GroupExpiration
---
 
# Reset-PnPMicrosoft365GroupExpiration

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : One of Directory.ReadWrite.All, Group.ReadWrite.All

Renews the Microsoft 365 Group by extending its expiration with the number of days defined in the group expiration policy set on the Azure Active Directory.

## SYNTAX 

```powershell
Reset-PnPMicrosoft365GroupExpiration -Identity <Microsoft365GroupPipeBind>
```

## DESCRIPTION

Allows to extend the Microsoft 365 Group expiration date by the number of days defined in the group expiration policy.

## EXAMPLES

### EXAMPLE 1
```powershell
Reset-PnPMicrosoft365GroupExpiration
```

Renews the Microsoft 365 Group by extending its expiration with the number of days defined in the group expiration policy set on the Azure Active Directory.

## PARAMETERS

### -Identity
The Identity of the Microsoft 365 Group.

```yaml
Type: Microsoft365GroupPipeBind
Parameter Sets: (All)

Required: True
Position: Named
Accept pipeline input: True
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
[Microsoft Graph documentation](https://learn.microsoft.com/graph/api/group-renew)