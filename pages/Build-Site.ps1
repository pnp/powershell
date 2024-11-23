# copy documentation to output folder
#New-Item -Path "./dev/pages/cmdlets/released" -ItemType Directory
#New-Item -Path "./dev/pages/cmdlets/nightly" -ItemType Directory

$nightlycmdlets = Get-ChildItem "./dev/documentation/*.md" | ForEach-Object { $_ | Select-Object -ExpandProperty BaseName }

class FrontMatters {
    [hashtable] GetHeader($path) {
    
        $c = get-content $path
        $header = @{}
        if ($c[0].equals("---")) {
            for ($q = 1; $q -lt $c.Length; $q++) {
                if ($c[$q] -eq "---") {
                    # front-matter ended
                    $q = $c.Length;
                }
                else {
                    $colonIndex = $c[$q].IndexOf(":");
                    $key = $c[$q].Substring(0, $colonIndex).Trim()
                    $value = $c[$q].Substring($colonIndex + 1).Trim()
                    $header[$key] = $value;
                }
            }
        }
        return $header
    }

    [string] WriteHeader($path, $header) {

        $c = get-content $path
    
    
        if ($c[0].equals("---")) {
            $newFile = [System.Collections.ArrayList]@()

            $frontMatterEnded = $false
            for ($q = 1; $q -lt $c.Length; $q++) {
                if ($c[$q] -eq "---") {
                    $frontMatterEnded = $true
                    $q++;
                }
                if ($frontMatterEnded -ne $false) {
                    $newFile.Add($c[$q])
                }
            }
            $contents = ""
            foreach ($line in $newFile) {
                $contents += "$line`n"
            }

            $newHeader = "---`n";
            $header.Keys.ForEach({ $newHeader += "$($_): $($header.Item($_))`n" });
           
            $newHeader += "---`n"
            Set-Content -Path $path -Value "$newHeader $contents" -Force
        }
        return $null
    }
}

$fm = New-Object -TypeName FrontMatters

$aliasCmdletsCount = 0
$aliasCmdlets = @()
Try {
	Write-Host "Generating documentation files for alias cmdlets" -ForegroundColor Yellow
	# Load the Module in a new PowerShell session
	$scriptBlockNightlyRelease = {
		Write-Host "Installing latest nightly release of PnP PowerShell"
  		Install-Module PnP.PowerShell -AllowPrerelease -Force

  		Write-Host "Retrieving PnP PowerShell alias cmdlets"
		$cmdlets = Get-Command -Module PnP.PowerShell | Where-Object CommandType -eq "Alias" | Select-Object -Property @{N="Alias";E={$_.Name}}, @{N="ReferencedCommand";E={$_.ReferencedCommand.Name}}
		$cmdlets
  		Write-Host "$($cmdlets.Length) alias cmdlets retrieved"
	}
	$aliasCmdlets = Start-ThreadJob -ScriptBlock $scriptBlockNightlyRelease | Receive-Job -Wait

    $aliasCmdletsCount = $aliasCmdlets.Length

    $scriptBlockStableRelease = {
		Write-Host "Installing latest stable release of PnP PowerShell"
  		Install-Module PnP.PowerShell -AllowPrerelease -Force

  		Write-Host "Retrieving PnP PowerShell cmdlets"
		$cmdlets = Get-Command -Module PnP.PowerShell | Select-Object Name
		$cmdlets
  		Write-Host "$($cmdlets.Length) cmdlets retrieved"
	}
	$stableReleaseCmdlets = Start-ThreadJob -ScriptBlock $scriptBlockStableRelease | Receive-Job -Wait

  	Write-Host "- Retrieving alias template page"
	$aliasTemplatePageContent = Get-Content -Path "./dev/pages/cmdlets/alias.template" -Raw

	ForEach($aliasCmdlet in $aliasCmdlets)
	{
		$destinationFileName = "./dev/documentation/$($aliasCmdlet.Alias).md"

		Write-Host "- Creating page for $($aliasCmdlet.Alias) being an alias for $($aliasCmdlet.ReferencedCommand) as $destinationFileName" -ForegroundColor Yellow
		$aliasTemplatePageContent.Replace("%%cmdletname%%", $aliasCmdlet.Alias).Replace("%%referencedcmdletname%%", $aliasCmdlet.ReferencedCommand) | Out-File $destinationFileName -Force
	}
}
Catch {
	Write-Host "Error: Cannot generate alias documentation files"
	Write-Host $_
}

Write-Host "Copying documentation files to page cmdlets"

Copy-Item -Path "./dev/documentation/*.md" -Destination "./dev/pages/cmdlets" -Force

foreach ($nightlycmdlet in $nightlycmdlets) {
    if (!$stableReleaseCmdlets.Contains($nightlycmdlet)) {
        Copy-Item "./dev/documentation/$nightlycmdlet.md" -Destination "./dev/pages/cmdlets" -Force | Out-Null
        # update the document to state it's only available in the nightly build
        $header = $fm.GetHeader("./dev/pages/cmdlets/$nightlycmdlet.md")
        $header["tags"] = "Available in the current Nightly Release only."
        #Write-Host "Writing $nightlycmdlet.md"
        $fm.WriteHeader("./dev/pages/cmdlets/$nightlycmdlet.md",$header)
    }
}

# Generate cmdlet toc
Write-Host "Retrieving all cmdlet pages"

$cmdletPages = Get-ChildItem -Path "./dev/pages/cmdlets/*.md" -Exclude "index.md","alias.template"
$toc = ""
foreach ($cmdletPage in $cmdletPages) {
    $toc = $toc + "- name: $($cmdletPage.BaseName)`n  href: $($cmdletPage.Name)`n"
}

$toc | Out-File "./dev/pages/cmdlets/toc.yml" -Force

# Generate cmdlet index page

Write-Host "Creating cmdlets index page"

$cmdletIndexPageContent = Get-Content -Path "./dev/pages/cmdlets/index.md" -Raw
$cmdletIndexPageContent = $cmdletIndexPageContent.Replace("%%cmdletcount%%", $cmdletPages.Length - $aliasCmdletsCount)

$cmdletIndexPageList = ""
$previousCmdletVerb = ""
foreach ($cmdletPage in $cmdletPages)
{
    Write-Host "- $($cmdletPage.Name)"
    
    # Define the verb of the cmdlet
    if($cmdletPage.BaseName.Contains("-"))
    {
        $cmdletVerb = $cmdletPage.BaseName.Remove($cmdletPage.BaseName.IndexOf("-"))
       
        if($cmdletVerb -ne $previousCmdletVerb)
        {
            # Add a new heading for the new verb
            $cmdletIndexPageList += "## $($cmdletVerb)`n"
        }
    }
    else
    {
        $cmdletVerb = ""
    }
    
    # Add a new entry for the verb
    $cmdletIndexPageList += "- [$($cmdletPage.BaseName)]($($cmdletPage.Name))"

    # Check if the cmdlet only exists in the nightly build
    if (!$stableReleaseCmdlets.Contains($cmdletPage.BaseName))
    {
        # Add a 1 to the cmdlet name if it's only available in the nightly build
        $cmdletIndexPageList = $cmdletIndexPageList + " <sup>1</sup>"
    }

    # Check if the cmdlet is an alias
    if ($aliasCmdlets.Alias -contains $cmdletPage.BaseName)
    {
        # Add a 2 to the cmdlet name if it's an alias
        $cmdletIndexPageList = $cmdletIndexPageList + " <sup>2</sup>"
    }
    
    $cmdletIndexPageList = $cmdletIndexPageList + "`n"
    
    if($cmdletVerb -ne "")
    {
        # Track the last verb so we know if we need to add a new heading for the next cmdlet
        $previousCmdletVerb = $cmdletVerb
    }
}

$cmdletIndexPageContent = $cmdletIndexPageContent.Replace("%%cmdletlisting%%", $cmdletIndexPageList)
$cmdletIndexPageContent | Out-File "./dev/pages/cmdlets/index.md" -Force

docfx build ./dev/pages/docfx.json

Copy-Item -Path "./dev/pages/_site/*" -Destination "./gh-pages" -Force -Recurse
