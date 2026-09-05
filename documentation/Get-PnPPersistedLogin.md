---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPPersistedLogin.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPPersistedLogin
---

# Get-PnPPersistedLogin

## SYNOPSIS
Lists persisted login cache registrations

## SYNTAX

```powershell
Get-PnPPersistedLogin
```

## DESCRIPTION
Returns the SharePoint tenant URL, client ID and authentication type for every login registered to use the local token cache by `Connect-PnPOnline -PersistLogin`. The encrypted tokens themselves are not returned.

The `AuthenticationType` property identifies delegated and certificate-based app-only logins. The `Enabled` property is `True` for every returned registration; disabled registrations are not returned. An empty result means no persisted logins are registered for the current operating-system user.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPPersistedLogin
```

Lists all persisted login cache registrations for the current operating-system user.

## RELATED LINKS

[Persisted Login](https://pnp.github.io/powershell/articles/persistedlogin.html)

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
