---
external help file:
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/reset-pnpmicrosoft365groupexpiration
applicable: SharePoint Online
schema: 2.0.0
title: Reset-PnPMicrosoft365GroupExpiration
---

# Reset-PnPMicrosoft365GroupExpiration

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : One of Directory.ReadWrite.All, Group.ReadWrite.All

Renews the Microsoft 365 Group by extending its expiration with the number of days defined in the group expiration policy set on the Azure Active Directory

## SYNTAX 

```powershell
Reset-PnPMicrosoft365GroupExpiration -Identity <Microsoft365GroupPipeBind>
                                     [-ByPassPermissionCheck [<SwitchParameter>]]
```

## DESCRIPTION
Renews the Microsoft 365 Group by extending its expiration with the number of days defined in the group expiration policy set on the Azure Active Directory

## EXAMPLES

### ------------------EXAMPLE 1------------------
```powershell
Reset-PnPMicrosoft365GroupExpiration
```

Renews the Microsoft 365 Group by extending its expiration with the number of days defined in the group expiration policy set on the Azure Active Directory

## PARAMETERS

### -ByPassPermissionCheck
Allows the check for required permissions in the access token to be bypassed when set to $true

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Accept pipeline input: False
```

### -Identity
The Identity of the Microsoft 365 Group

```yaml
Type: Microsoft365GroupPipeBind
Parameter Sets: (All)

Required: True
Position: Named
Accept pipeline input: True
```

## RELATED LINKS

[SharePoint Developer Patterns and Practices](https://aka.ms/sppnp)[Documentation](https://docs.microsoft.com/graph/api/group-renew)