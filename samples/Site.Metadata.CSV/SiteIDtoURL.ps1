## DISCLAIMER:
## Copyright (c) Microsoft Corporation. All rights reserved. This
## script is made available to you without any express, implied or
## statutory warranty, not even the implied warranty of
## merchantability or fitness for a particular purpose, or the
## warranty of title or non-infringement. The entire risk of the
## use or the results from the use of this script remains with you.
##
## Filename:    PNP-SiteIDtoURL.ps1
## Author:      salarson
## Description: Converts unique site IDs from a txt file to URLs for M365 Tenancy and exports to CSV.
#####################################################################

param(
    [Parameter(Mandatory=$true)]
    [string]$SPOAdminURL = $(Read-Host -Prompt "Please enter the SharePoint Online Admin URL"),
   
    [Parameter(Mandatory=$false)]
    [string]$log = ".\SPOSiteURLS.csv"
)

## Load Form Selector
Add-Type -AssemblyName System.Windows.Forms

function Select-FileDialog {
      param([string]$Title,[string]$Directory,[string]$Filter="All Files (*.*)|*.*")
      $objForm = New-Object System.Windows.Forms.OpenFileDialog
      $objForm.InitialDirectory = $Directory
      $objForm.Filter = $Filter
      $objForm.Title = $Title
      $Show = $objForm.ShowDialog()
      If ($Show -eq "OK") {
            Return $objForm.FileName
      } Else {
            Write-Error "Operation cancelled by user."
      }
}

## Check to see if PNP is installed
Write-Host "Please ensure that you are running PowerShell in Admin Mode" -ForegroundColor Yellow
$CheckPNP = Get-Module -Name PnP.PowerShell -ListAvailable
If ($CheckPNP -eq $null) {
    Write-Host "It appears you do not have SharePoint Online PNP installed!" -ForegroundColor Red
    $Force = Read-Host "Would you like to install SharePoint Online PNP Module? Type 'Y' to force or type 'N' to continue"
    if ($Force -like "y") {
        Install-Module -Name PnP.PowerShell -Force
    } elseif ($Force -like "n") {
        Write-Host "Continuing without install of PNP and assuming module was not detected properly" -ForegroundColor Yellow
    }
}

# Select Input File
Write-Host "Please select the input text file which has the site collection GUID's......." -ForegroundColor DarkGreen
$InputFile = Select-FileDialog -Title "Select the input file of site ID's to convert to URL's"

$cnt = Get-Content $InputFile

$starttime = Get-Date

## Connect to PNP PowerShell
Connect-PnPOnline -Url $SPOAdminURL -Interactive

## Create Result Set
[System.Collections.Generic.List[PSCustomObject]] $results = New-Object System.Collections.Generic.List[PSCustomObject]

$count = 0;

foreach($siteid in $cnt) {
    try {
        Write-Progress -Activity 'Processing sites..' -Status $siteid -PercentComplete ($count / $cnt.count * 100)
        $query="SiteId:" + $SiteId + " contentClass:STS_Site"
        $result = Submit-PnPSearchQuery -Query $query
        foreach ($row in $result.ResultRows) {
            $res = New-Object psobject
            foreach ($key in $row.keys) {
                $res | Add-Member Noteproperty $key $row[$key]
            }
            $results.Add($res)
        }
        $count++
    } catch {
        Write-Host "Failed to process $siteid - Please ensure you are using a txt file and have all ID's listed in a row" -ForegroundColor Red
        break
    }
}

$results | Export-Csv -Path $log -NoTypeInformation -Force -Append

$duration = (Get-Date) - $starttime
Write-Host "`nComplete in $($duration)!" -ForegroundColor Green
Write-Host "Total sites processed: $count" -ForegroundColor Cyan
Write-Host "Please review the log at $($log)" -ForegroundColor Cyan
