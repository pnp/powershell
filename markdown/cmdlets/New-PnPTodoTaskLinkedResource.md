---
schema: 2.0.0
tags: Available in the current Nightly Release only.
Module Name: PnP.PowerShell
title: New-PnPTodoTaskLinkedResource
online version: https://pnp.github.io/powershell/cmdlets/New-PnPTodoTaskLinkedResource.html
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
---
 
# New-PnPTodoTaskLinkedResource

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : Tasks.ReadWrite

Creates a linked resource on a Todo task.

## SYNTAX

```powershell
New-PnPTodoTaskLinkedResource [-List] <String> [-Task] <TodoTaskPipeBind> [-ApplicationName] <String> [-DisplayName] <String> [-ExternalId] <String> [-WebUrl] <TodoTaskPipeBind> [-User <EntraIDUserPipeBind>]
```

## DESCRIPTION
Use the cmdlet to create a linked resource on a Todo task.

## EXAMPLES

### EXAMPLE 1
```powershell
New-PnPTodoTaskLinkedResource -List "AQMkADAwATM0MDAAMS0yMDkyLTllN2QtMDACLTAwCgAuAAAD" -Task "AAMkAGU4MGE1OTRiLTUzMGEtNDRjZi05ZmVm" -ApplicationName "Contoso Travel" -DisplayName "Travel booking" -ExternalId "booking-123" -WebUrl "https://contoso.example/bookings/123"
```

This will create a linked resource on the specified Todo task.

## PARAMETERS

### -ApplicationName
Name of the application associated with the linked resource.

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

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

### -DisplayName
Display name of the linked resource.

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ExternalId
External identifier of the linked resource.

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -List
Id or display name of the Todo list.

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Task
Id of the Todo task or an instance returned by `Get-PnPTodoTask`.

```yaml
Type: TodoTaskPipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -User
The UPN, Id or instance of an Entra ID user for which you would like to create the linked resource.

```yaml
Type: EntraIDUserPipeBind
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -WebUrl
Web URL of the linked resource.

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



