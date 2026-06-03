param(
	[string]$SourceRoot = (Split-Path -Parent $PSScriptRoot),
	[string]$PublishPath,
	[switch]$SkipPublish
)

$SourceRoot = (Resolve-Path $SourceRoot).Path
$PagesPath = Join-Path $SourceRoot "pages"
$ArticlesPath = Join-Path $PagesPath "articles"
$DocumentationPath = Join-Path $SourceRoot "documentation"
$CmdletsPath = Join-Path $PagesPath "cmdlets"
$DocFxConfigPath = Join-Path $PagesPath "docfx.json"
$SitePath = Join-Path $PagesPath "_site"
$MarkdownOutputPath = Join-Path $SitePath "markdown"
$CmdletIndexPath = Join-Path $CmdletsPath "index.md"
$AliasTemplatePath = Join-Path $CmdletsPath "alias.template"

if (!(Test-Path $DocumentationPath)) {
	throw "Unable to find documentation folder at $DocumentationPath"
}

if (!(Test-Path $DocFxConfigPath)) {
	throw "Unable to find DocFX configuration at $DocFxConfigPath"
}

if (!(Test-Path $CmdletIndexPath)) {
	throw "Unable to find cmdlet index template at $CmdletIndexPath"
}

if (!(Test-Path $AliasTemplatePath)) {
	throw "Unable to find alias template at $AliasTemplatePath"
}

function Clear-GeneratedCmdletPages {
	if (Test-Path $CmdletsPath) {
		Get-ChildItem -Path $CmdletsPath -File | Where-Object { $_.Name -notin @("index.md", "alias.template") } | Remove-Item -Force
	}
}

function Copy-MarkdownFiles {
	param(
		[string]$SourcePath,
		[string]$DestinationPath
	)

	if (!(Test-Path $SourcePath)) {
		return
	}

	Get-ChildItem -Path $SourcePath -Filter "*.md" -File -Recurse | ForEach-Object {
		$relativePath = [System.IO.Path]::GetRelativePath($SourcePath, $_.FullName)
		$destinationFilePath = Join-Path $DestinationPath $relativePath
		$destinationFolderPath = Split-Path -Parent $destinationFilePath
		New-Item -Path $destinationFolderPath -ItemType Directory -Force | Out-Null
		Copy-Item -Path $_.FullName -Destination $destinationFilePath -Force
	}
}

function Copy-MarkdownSourceFiles {
	Write-Host "Copying markdown source files to generated site"

	Remove-Item -Path $MarkdownOutputPath -Recurse -Force -ErrorAction SilentlyContinue
	New-Item -Path $MarkdownOutputPath -ItemType Directory -Force | Out-Null

	Get-ChildItem -Path $PagesPath -Filter "*.md" -File | ForEach-Object {
		Copy-Item -Path $_.FullName -Destination (Join-Path $MarkdownOutputPath $_.Name) -Force
	}

	Copy-MarkdownFiles -SourcePath $ArticlesPath -DestinationPath (Join-Path $MarkdownOutputPath "articles")
	Copy-MarkdownFiles -SourcePath $CmdletsPath -DestinationPath (Join-Path $MarkdownOutputPath "cmdlets")
}

function Clear-PublishPath {
	param(
		[string]$Path
	)

	$resolvedPath = (Resolve-Path $Path).Path
	$protectedPaths = @($SourceRoot, $PagesPath, $SitePath, [System.IO.Path]::GetPathRoot($resolvedPath))

	if ($protectedPaths -contains $resolvedPath) {
		throw "Refusing to clear protected publish path $resolvedPath"
	}

	Write-Host "Clearing existing published site at $resolvedPath"

	Get-ChildItem -Path $resolvedPath -Force | Where-Object { $_.Name -ne ".git" } | Remove-Item -Recurse -Force
}

New-Item -Path $CmdletsPath -ItemType Directory -Force | Out-Null
$cmdletIndexTemplateBytes = [System.IO.File]::ReadAllBytes($CmdletIndexPath)

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

try {
	Clear-GeneratedCmdletPages

	$nightlycmdlets = Get-ChildItem (Join-Path $DocumentationPath "*.md") | ForEach-Object { $_ | Select-Object -ExpandProperty BaseName }
	$fm = New-Object -TypeName FrontMatters

	$aliasCmdletsCount = 0
	$aliasCmdlets = @()
	$stableReleaseCmdlets = @()
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
			Write-Host "Retrieving PnP PowerShell cmdlets from latest stable release"
			$cmdlets = (Find-Module -Name PnP.PowerShell).AdditionalMetadata.Cmdlets.Split(" ")
			$cmdlets
			Write-Host "$($cmdlets.Length) cmdlets retrieved"
		}
		$stableReleaseCmdlets = Start-ThreadJob -ScriptBlock $scriptBlockStableRelease | Receive-Job -Wait
	}
	Catch {
		Write-Host "Error: Cannot generate alias documentation files"
		Write-Host $_
	}

	Write-Host "Copying documentation files to page cmdlets"

	Copy-Item -Path (Join-Path $DocumentationPath "*.md") -Destination $CmdletsPath -Force

	if ($aliasCmdletsCount -gt 0) {
		Write-Host "- Retrieving alias template page"
		$aliasTemplatePageContent = Get-Content -Path $AliasTemplatePath -Raw

		ForEach($aliasCmdlet in $aliasCmdlets)
		{
			$destinationFileName = Join-Path $CmdletsPath "$($aliasCmdlet.Alias).md"

			Write-Host "- Creating page for $($aliasCmdlet.Alias) being an alias for $($aliasCmdlet.ReferencedCommand) as $destinationFileName" -ForegroundColor Yellow
			$aliasTemplatePageContent.Replace("%%cmdletname%%", $aliasCmdlet.Alias).Replace("%%referencedcmdletname%%", $aliasCmdlet.ReferencedCommand) | Out-File $destinationFileName -Force
		}
	}

	foreach ($nightlycmdlet in $nightlycmdlets) {
		if (!($stableReleaseCmdlets -like $nightlycmdlet)) {
			Copy-Item (Join-Path $DocumentationPath "$nightlycmdlet.md") -Destination $CmdletsPath -Force | Out-Null
			# update the document to state it's only available in the nightly build
			$cmdletPagePath = Join-Path $CmdletsPath "$nightlycmdlet.md"
			$header = $fm.GetHeader($cmdletPagePath)
			$header["tags"] = "Available in the current Nightly Release only."
			#Write-Host "Writing $nightlycmdlet.md"
			$fm.WriteHeader($cmdletPagePath,$header)
		}
	}

	# Generate cmdlet toc
	Write-Host "Retrieving all cmdlet pages"

	$cmdletPages = Get-ChildItem -Path (Join-Path $CmdletsPath "*.md") -Exclude "index.md","alias.template"
	$toc = ""
	foreach ($cmdletPage in $cmdletPages) {
		$toc = $toc + "- name: $($cmdletPage.BaseName)`n  href: $($cmdletPage.Name)`n"
	}

	$toc | Out-File (Join-Path $CmdletsPath "toc.yml") -Force

	# Generate cmdlet index page

	Write-Host "Creating cmdlets index page"

	$cmdletIndexPageContent = Get-Content -Path $CmdletIndexPath -Raw
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
		if (!($stableReleaseCmdlets -like $cmdletPage.BaseName))
		{
			# Add a 1 to the cmdlet name if it's only available in the nightly build
			$cmdletIndexPageList = $cmdletIndexPageList + " <sup>1</sup>"

			Write-Host "  - Nightly only"
		}

		# Check if the cmdlet is an alias
		if ($aliasCmdlets.Alias -contains $cmdletPage.BaseName)
		{
			# Add a 2 to the cmdlet name if it's an alias
			$cmdletIndexPageList = $cmdletIndexPageList + " <sup>2</sup>"

			Write-Host "  - Alias"
		}

		$cmdletIndexPageList = $cmdletIndexPageList + "`n"

		if($cmdletVerb -ne "")
		{
			# Track the last verb so we know if we need to add a new heading for the next cmdlet
			$previousCmdletVerb = $cmdletVerb
		}
	}

	$cmdletIndexPageContent = $cmdletIndexPageContent.Replace("%%cmdletlisting%%", $cmdletIndexPageList)
	$cmdletIndexPageContent | Out-File $CmdletIndexPath -Force

	& docfx build $DocFxConfigPath
	if ($LASTEXITCODE -ne 0) {
		throw "DocFX build failed with exit code $LASTEXITCODE"
	}

	Copy-MarkdownSourceFiles

	if (!$SkipPublish) {
		if ([string]::IsNullOrWhiteSpace($PublishPath)) {
			$publishCandidate = Join-Path (Split-Path -Parent $SourceRoot) "gh-pages"
			if (Test-Path $publishCandidate) {
				$PublishPath = $publishCandidate
			}
		}

		if (![string]::IsNullOrWhiteSpace($PublishPath)) {
			Write-Host "Copying generated site to $PublishPath"
			Clear-PublishPath -Path $PublishPath
			Copy-Item -Path (Join-Path $SitePath "*") -Destination $PublishPath -Force -Recurse
		}
		else {
			Write-Host "No publish path found. Skipping copy to gh-pages."
		}
	}
}
finally {
	[System.IO.File]::WriteAllBytes($CmdletIndexPath, $cmdletIndexTemplateBytes)
	Clear-GeneratedCmdletPages
}