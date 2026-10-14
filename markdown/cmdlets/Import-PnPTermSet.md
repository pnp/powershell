---
Module Name: PnP.PowerShell
title: Import-PnPTermSet
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Import-PnPTermSet.html
---
 
# Import-PnPTermSet

## SYNOPSIS
Imports a taxonomy term set from a file in the standard format.

## SYNTAX

```powershell
Import-PnPTermSet -GroupName <String> -Path <String> [-TermSetId <Guid>] [-SynchronizeDeletions]
 [-IsOpen <Boolean>] [-Contact <String>] [-Owner <String>] [-TermStoreName <String>]
 [-Connection <PnPConnection>] 
```

## DESCRIPTION
The format of the file is the same as that used by the import function in the web interface. A sample file can be obtained from the web interface.

This is a CSV file, with the following headings:

  Term Set Name,Term Set Description,LCID,Available for Tagging,Term Description,Level 1 Term,Level 2 Term,Level 3 Term,Level 4 Term,Level 5 Term,Level 6 Term,Level 7 Term

The first data row must contain the Term Set Name, Term Set Description, and LCID, and should also contain the first term. 

It is recommended that a fixed GUID be used as the termSetId, to allow the term set to be easily updated (so do not pass Guid.Empty).

In contrast to the web interface import, this is not a one-off import but runs synchronization logic allowing updating of an existing Term Set. When synchronizing, any existing terms are matched (with Term Description and Available for Tagging updated as necessary), any new terms are added in the correct place in the hierarchy, and (if synchronizeDeletions is set) any terms not in the imported file are removed.

The import file also supports an expanded syntax for the Term Set Name and term names (Level 1 Term, Level 2 Term, etc). These columns support values with the format 'Name | GUID', with the name and GUID separated by a pipe character (note that the pipe character is invalid to use within a taxonomy item name). This expanded syntax is not required, but can be used to ensure all terms have fixed IDs.

## EXAMPLES

### EXAMPLE 1
```powershell
Import-PnPTermSet -GroupName 'Standard Terms' -Path 'C:\\Temp\\ImportTermSet.csv' -SynchronizeDeletions
```

Creates (or updates) the term set specified in the import file, in the group specified, removing any existing terms not in the file.

### EXAMPLE 2
```powershell
Import-PnPTermSet -TermStoreName 'My Term Store' -GroupName 'Standard Terms' -Path 'C:\\Temp\\ImportTermSet.csv' -TermSetId '{15A98DB6-D8E2-43E6-8771-066C1EC2B8D8}'
```

Creates (or updates) the term set specified in the import file, in the term store and group specified, using the specified ID.

### EXAMPLE 3
```powershell
Import-PnPTermSet -GroupName 'Standard Terms' -Path 'C:\\Temp\\ImportTermSet.csv' -IsOpen $true -Contact 'user@example.org' -Owner 'user@example.org'
```

Creates (or updates) the term set specified in the import file, setting the IsOpen, Contact, and Owner properties as specified.

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
Contact for the term set; if not specified, the existing setting is retained.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -GroupName
Group to import the term set to; an error is returned if the group does not exist.

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IsOpen
Whether the term set should be marked open; if not specified, then the existing setting is not changed.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Owner
Owner for the term set; if not specified, the existing setting is retained.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Path
Local path to the file containing the term set to import, in the standard format (as the 'sample import file' available in the Term Store Administration).

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SynchronizeDeletions
If specified, the import will remove any terms (and children) previously in the term set but not in the import file; default is to leave them.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TermSetId
GUID to use for the term set; if not specified, or the empty GUID, a random GUID is generated and used.

```yaml
Type: Guid
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TermStoreName
Term store to import into; if not specified the default term store is used.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

