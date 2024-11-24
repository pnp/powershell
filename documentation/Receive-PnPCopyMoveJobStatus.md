---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Receive-PnPCopyMoveJobStatus.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Receive-PnPCopyMoveJobStatus
---

# Receive-PnPCopyMoveJobStatus

## SYNOPSIS

This cmdlets receives Copy or Move job status which is being returned by Copy-PnPFile or Move-PnPFile when using the -NoWait parameter

## SYNTAX

### Default (Default)

```
Receive-PnPCopyMoveJobStatus -Job <CopyMigrationInfo> [-Wait]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This cmdlets outputs the results of a pending/finished copy or move job.

## EXAMPLES

### Example 1

```powershell
$job = Copy-PnPFile -SourceUrl "Shared Documents/company.docx" -TargetUrl "SubSite2/Shared Documents" -NoWait
$jobStatus = Receive-PnPCopyMoveJobStatus -Job $job
if($jobStatus.JobState == 0)
{
  Write-Host "Job finished"
}
```

Copies a file named company.docx from the current document library to the documents library in SubSite2. It will not wait for the action to return but returns job information instead. The Receive-PnPCopyMoveJobStatus cmdlet will return the job status.

## PARAMETERS

### -Connection

Optional connection to be used by the cmdlet.
Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.

```yaml
Type: PnPConnection
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Job

The job which is returned from Copy-PnPFile or Move-PnPFile

```yaml
Type: CopyMigrationInfo
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Wait

If specified the cmdlet will continue to poll the job to be finished.

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
