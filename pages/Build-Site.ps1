# copy documentation to output folder
#New-Item -Path "./dev/pages/cmdlets/released" -ItemType Directory
#New-Item -Path "./dev/pages/cmdlets/nightly" -ItemType Directory

$nightlycmdlets = Get-ChildItem "./dev/documentation/*.md" | ForEach-Object { $_ | Select-Object -ExpandProperty Name }
$releasedcmdlets = Get-ChildItem "./master/documentation/*.md" | ForEach-Object { $_ | Select-Object -ExpandProperty Name }

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

Copy-Item -Path "./dev/documentation/*.md" -Destination "./dev/pages/cmdlets" -Force

foreach ($nightlycmdlet in $nightlycmdlets) {
    if (!$releasedcmdlets.Contains($nightlycmdlet)) {
        Copy-Item "./dev/documentation/$nightlycmdlet" -Destination "./dev/pages/cmdlets" -Force
        # update the document to state it's only available in the nightly build
        $header = $fm.GetHeader("./dev/pages/cmdlets/$nightlycmdlet")
        $header["tags"] = "Available in the current Nightly Release only."
        Write-Host "Writing $nightlycmdlet"
        $fm.WriteHeader("./dev/pages/cmdlets/$nightlycmdlet",$header)
    }
}

# Generate cmdlet toc

$cmdletPages = Get-ChildItem -Path "./dev/pages/cmdlets/*.md" -Exclude "index.md"
$toc = ""
foreach ($cmdletPage in $cmdletPages) {
    $toc = $toc + "- name: $($cmdletPage.BaseName)`n  href: $($cmdletPage.Name)`n"
}

$toc | Out-File "./dev/pages/cmdlets/toc.yml" -Force

# Generate cmdlet index page

$cmdletIndexPageContent = Get-Content -Path "./dev/pages/cmdlets/index.md" -Raw
$cmdletIndexPageContent = $cmdletIndexPageContent.Replace("%%cmdletcount%%", $cmdletPages.Length)

$cmdletIndexPageList = ""
$previousCmdletVerb = ""
foreach ($cmdletPage in $cmdletPages)
{
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
    if (!$releasedcmdlets.Contains($cmdletPage.Name))
    {
        # Add an asterisk to the cmdlet name if it's only available in the nightly build
        $cmdletIndexPageList = $cmdletIndexPageList + "*"
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
