$ErrorActionPreference = "Stop"
Set-StrictMode -Version 2.0

function CleanPackage {
    param([string] $Package , [int] $VersionsToKeep, [string] $key)

    $xml = Invoke-WebRequest -Uri "https://www.powershellgallery.com/api/v2/Search()?`$orderby=Id&`$skip=0&`$top=50&searchTerm='$Package'&targetFramework=''&includePrerelease=true"

    $result = [xml] $xml

    $entries = $result.feed.entry | ?{$_.properties.Id -eq "PnP.PowerShell"} # There are other packages not owned by us that contain this string.

    $sortedEntries = $entries | Sort-Object -Property @{Expression = {[System.Management.Automation.SemanticVersion]::Parse($_.properties.version)}; Descending=$false} | Where-Object {[System.Management.Automation.SemanticVersion]::Parse($_.properties.version).PreReleaseLabel -eq "nightly"} 

    # keep last 10
    $entriesToKeep = $sortedEntries | Select-Object -Last 10
    
    foreach($entry in $entries)
    {
        if(!$entriesToKeep.Contains($entry))
        {
            $entry.properties.Id
            nuget delete "package/$($entry.property.Id)" $entry.property.Version -ApiKey $key -Source https://www.powershellgallery.com/api/v2 -NonInteractive
        }
    }
}

$ApiKey = $("$env:POWERSHELLGALLERY_API_KEY")

CleanPackage "PnP.PowerShell" 10 $ApiKey