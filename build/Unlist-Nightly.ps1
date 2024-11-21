$ErrorActionPreference = "Stop"
Set-StrictMode -Version 2.0

function UnlistNightlies {
    param([string] $Package)
    [Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12 -bor [Net.SecurityProtocolType]::Tls13
 
    $entries = GetEntries "https://www.powershellgallery.com/api/v2/FindPackagesById()?id='PnP.PowerShell'"
    
    $sorted = $entries | Sort-Object -Property Version -Descending
   
    # Get the nightly releases
    $nightlies = $sorted | Where-Object { $null -ne $_.Version.PreReleaseLabel }
    
    # mark the latest 50 nightlies as unlisted
    $tounlist = $nightlies | Select-Object -First 50 -Skip 10

    $key = $("$env:POWERSHELLGALLERY_API_KEY")   

    foreach ($entry in $tounlist) {        
        Write-host "Entry to be deleted - $($entry.Version.ToString())"            
        nuget delete "package/PnP.PowerShell" $entry.version.ToString() -ApiKey $key -Source https://www.powershellgallery.com/api/v2 -NonInteractive
    }
}

function GetEntries([string] $url) {
    $entries = New-Object System.Collections.Generic.List[System.Object]

    $result = Invoke-WebRequest -Uri $url
    $xml = [xml]$result

    foreach ($entry in $xml.feed.entry) {
        $newEntry = [PSCustomObject]@{
            Id      = $entry.id
            Version = [System.Management.Automation.SemanticVersion]$entry.properties.version
        }
        $entries.Add($newEntry)
    }


    $nextLink = $xml.feed.link | Where-Object { $_.rel -eq "next" }

    if ($null -ne $nextLink) {
        $extraEntries = GetEntries $nextLink.href
        $entries.AddRange($extraEntries)
    }
    return $entries
}

Write-host "Starting cleanup old nightlies job"
UnlistNightlies "PnP.PowerShell"
