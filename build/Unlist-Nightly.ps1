$ErrorActionPreference = "Stop"
Set-StrictMode -Version 2.0

function CleanPackage {
    param([string] $Package)

    $xml = Invoke-WebRequest -Uri "https://www.powershellgallery.com/api/v2/Search()?`$orderby=Id&`$skip=0&`$top=50&searchTerm='$Package'&targetFramework=''&includePrerelease=true"

    $result = [xml] $xml

    $entries = $result.feed.entry | Where-Object{$_.properties.Id -eq "PnP.PowerShell"} # There are other packages not owned by us that contain this string.

    $sortedEntries = $entries | Sort-Object -Property @{Expression = {[System.Management.Automation.SemanticVersion]::Parse($_.properties.version)}; Descending=$false} | Where-Object {[System.Management.Automation.SemanticVersion]::Parse($_.properties.version).PreReleaseLabel -eq "nightly"} 
    $releasedEntries = $entries.Where({[System.Management.Automation.SemanticVersion]::Parse($_.properties.version).PreReleaseLabel -ne "nightly"} );

    # keep last 10
    $entriesToKeep = ($sortedEntries | Select-Object -Last 10) + $releasedEntries
    $key = $("$env:POWERSHELLGALLERY_API_KEY")   
    foreach($entry in $entries)
    {
        if(!$entriesToKeep.Contains($entry))
        {
            Write-Host "Removing version $($entry.properties.Version)"
            nuget delete "package/$($entry.properties.Id)" $entry.properties.Version -ApiKey $key -Source https://www.powershellgallery.com/api/v2 -NonInteractive
        }
    }
}

Write-host "Starting cleanup old nightlies job"
CleanPackage "PnP.PowerShell"
