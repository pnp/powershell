---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Receive-PnPCopyMoveJobStatus.html
external help file: PnP.PowerShell.dll-Help.xml
title: Receive-PnPCopyMoveJobStatus
---
 
# Receive-PnPCopyMoveJobStatus

## SYNOPSIS
This cmdlets receives Copy or Move job status which is being returned by Copy-PnPFile or Move-PnPFile when using the -NoWait parameter

## SYNTAX

```powershell
Receive-PnPCopyMoveJobStatus -Job <CopyMigrationInfo> [-Wait]
```

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
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Job
The job which is returned from Copy-PnPFile or Move-PnPFile

```yaml
Type: CopyMigrationInfo
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Wait
If specified the cmdlet will continue to poll the job to be finished.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

