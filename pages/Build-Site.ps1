# copy documentation to output folder
# make cmdlets folder
New-Item -Path "./dev/pages/cmdlets" -ItemType Directory
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

# generate cmdlet toc

$items = Get-ChildItem "./dev/pages/cmdlets/*.md"
$toc = ""
foreach ($item in $items) {
    $toc = $toc + "- name: $($item.Name -replace '.md','')`n  href: $($item.Name)`n"
}

$toc | Out-File "./dev/pages/cmdlets/toc.yml" -Force

docfx build ./dev/pages/docfx.json

Copy-Item -Path "./dev/pages/_site/*" -Destination "./gh-pages" -Force -Recurse

