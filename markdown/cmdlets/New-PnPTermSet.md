---
Module Name: PnP.PowerShell
title: New-PnPTermSet
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/New-PnPTermSet.html
---
 
# New-PnPTermSet

## SYNOPSIS
Creates a taxonomy term set

## SYNTAX

```powershell
New-PnPTermSet -Name <String> [-Id <Guid>] [-Lcid <Int32>] -TermGroup <TermGroupPipeBind> [-Contact <String>]
 [-Description <String>] [-IsOpenForTermCreation] [-IsNotAvailableForTagging] [-Owner <String>]
 [-StakeHolders <String[]>] [-CustomProperties <Hashtable>]
 [-TermStore <PnP.PowerShell.Commands.Base.PipeBinds.GenericObjectNameIdPipeBind`1[Microsoft.SharePoint.Client.Taxonomy.TermStore]>]
 [-Connection <PnPConnection>] 
```

## DESCRIPTION

Allows to create a taxonomy term set.

## EXAMPLES

### EXAMPLE 1
```powershell
New-PnPTermSet -Name "Department" -TermGroup "Corporate"
```

Creates a new termset named "Department" in the group named "Corporate"

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

### -Contact
An e-mail address for term suggestion and feedback. If left blank the suggestion feature will be disabled.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -CustomProperties

```yaml
Type: Hashtable
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Description
Descriptive text to help users understand the intended use of this term set.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Id
The Id to use for the term set; if not specified, or the empty GUID, a random GUID is generated and used.

```yaml
Type: Guid
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IsNotAvailableForTagging
By default a term set is available to be used by end users and content editors of sites consuming this term set. Specify this switch to turn this off

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IsOpenForTermCreation
When a term set is closed, only metadata managers can add terms to this term set. When it is open, users can add terms from a tagging application. Not specifying this switch will make the term set closed.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Lcid
The locale id to use for the term set. Defaults to the current locale id.

```yaml
Type: Int32
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Name
The name of the termset.

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Owner
The primary user or group of this term set.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -StakeHolders
People and groups in the organization that should be notified before major changes are made to the term set. You can enter multiple users or groups.

```yaml
Type: String[]
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TermGroup
Name, id or actually termgroup to create the termset in.

```yaml
Type: TermGroupPipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -TermStore
Term store to use; if not specified the default term store is used.

```yaml
Type: PnP.PowerShell.Commands.Base.PipeBinds.GenericObjectNameIdPipeBind`1[Microsoft.SharePoint.Client.Taxonomy.TermStore]
Parameter Sets: (All)
Aliases: TermStoreName

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

