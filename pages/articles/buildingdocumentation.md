---
uid: buildingdocumentation
---

# Building the documentation site locally

The documentation site is built with [DocFX](https://dotnet.github.io/docfx/). If you're changing articles, cmdlet documentation, the site template, or the documentation build scripts, it's worth building the site locally before you submit your pull request.

There are two useful ways to test the site locally:

- A quick DocFX build from your current checkout. Use this when you changed articles, images, templates, styles, or `pages/docfx.json`.
- A full build that follows the same folder layout as the GitHub Actions workflow. Use this when you want to test `Build-Site.ps1`, cmdlet documentation copying, generated alias pages, or the final output that gets copied to the `gh-pages` branch.

The GitHub Actions workflows use DocFX 2.78.5 and the .NET SDK 10. The commands below use the same DocFX version.

## Prerequisites

Install the following tools first:

1. [Git](https://git-scm.com/downloads)
1. [PowerShell 7](https://learn.microsoft.com/powershell/scripting/install/installing-powershell)
1. [.NET SDK 10](https://dotnet.microsoft.com/download)

You can validate your installation by running:

```powershell
git --version
pwsh --version
dotnet --version
```

The full build also needs access to the PowerShell Gallery. During that build, `Build-Site.ps1` installs the latest prerelease version of PnP PowerShell in order to discover aliases and generate documentation pages for them.

## Quick build on Windows

Open PowerShell 7 and navigate to your local clone of the repository.

```powershell
cd C:\repos\powershell
```

Install DocFX into a local `.tools` folder. This keeps the version used for this repository separate from any globally installed DocFX version.

```powershell
New-Item -ItemType Directory -Force .\.tools | Out-Null
dotnet tool install docfx --tool-path .\.tools --version 2.78.5
```

If you already have DocFX installed in that folder and want to refresh it to the expected version, run:

```powershell
dotnet tool update docfx --tool-path .\.tools --version 2.78.5
```

Build the documentation site:

```powershell
.\.tools\docfx.exe build .\pages\docfx.json
```

Serve the generated site locally:

```powershell
.\.tools\docfx.exe serve .\pages\_site --port 8080
```

Open http://localhost:8080 in your browser. If port 8080 is already in use, pick another port, for example `--port 8091`.

When you're done, remove the generated output:

```powershell
Remove-Item .\pages\_site, .\pages\obj -Recurse -Force -ErrorAction SilentlyContinue
```

## Quick build on macOS or Linux

Open a terminal and navigate to your local clone of the repository.

```bash
cd ~/repos/powershell
```

Install DocFX into a local `.tools` folder.

```bash
mkdir -p .tools
dotnet tool install docfx --tool-path ./.tools --version 2.78.5
```

If you already have DocFX installed in that folder and want to refresh it to the expected version, run:

```bash
dotnet tool update docfx --tool-path ./.tools --version 2.78.5
```

Build the documentation site:

```bash
./.tools/docfx build ./pages/docfx.json
```

Serve the generated site locally:

```bash
./.tools/docfx serve ./pages/_site --port 8080
```

Open http://localhost:8080 in your browser. If port 8080 is already in use, pick another port, for example `--port 8091`.

When you're done, remove the generated output:

```bash
rm -rf ./pages/_site ./pages/obj
```

## Full build on Windows

The documentation site workflow checks out three branches next to each other: `master`, `dev`, and `gh-pages`. The `Build-Site.ps1` script expects that layout and uses paths such as `./dev/pages` and `./gh-pages`.

From your regular repository checkout, create a temporary worktree layout:

```powershell
$repo = "C:\repos\powershell"
$root = "C:\repos\powershell-docs-local"

git -C $repo fetch origin
New-Item -ItemType Directory -Force $root | Out-Null

git -C $repo worktree add "$root\dev" HEAD
git -C $repo worktree add "$root\master" origin/master
git -C $repo worktree add "$root\gh-pages" origin/gh-pages
```

Install DocFX into the temporary build folder and add it to the current session path so `Build-Site.ps1` can call `docfx`:

```powershell
Push-Location $root

New-Item -ItemType Directory -Force .\.tools | Out-Null
dotnet tool install docfx --tool-path .\.tools --version 2.78.5
$env:PATH = "$(Resolve-Path .\.tools);$env:PATH"
```

Run the same build script used by the workflow:

```powershell
.\dev\pages\Build-Site.ps1
```

Serve the generated site:

```powershell
.\.tools\docfx.exe serve .\dev\pages\_site --port 8080
```

Open http://localhost:8080 in your browser and check the pages you changed. Press `CTRL+C` in the terminal when you're done serving the site.

Clean up the worktrees from your original checkout:

```powershell
Pop-Location

git -C $repo worktree remove "$root\dev" --force
git -C $repo worktree remove "$root\master" --force
git -C $repo worktree remove "$root\gh-pages" --force
Remove-Item $root -Recurse -Force -ErrorAction SilentlyContinue
```

## Full build on macOS or Linux

The same workflow layout can be created with Git worktrees on macOS or Linux.

```bash
repo=~/repos/powershell
root=~/repos/powershell-docs-local

git -C "$repo" fetch origin
mkdir -p "$root"

git -C "$repo" worktree add "$root/dev" HEAD
git -C "$repo" worktree add "$root/master" origin/master
git -C "$repo" worktree add "$root/gh-pages" origin/gh-pages
```

Install DocFX into the temporary build folder and add it to the current session path:

```bash
cd "$root"

mkdir -p .tools
dotnet tool install docfx --tool-path ./.tools --version 2.78.5
export PATH="$(pwd)/.tools:$PATH"
```

Run the same build script used by the workflow:

```bash
pwsh ./dev/pages/Build-Site.ps1
```

Serve the generated site:


```bash
./.tools/docfx serve ./dev/pages/_site --port 8080
```

Open http://localhost:8080 in your browser and check the pages you changed. Press `CTRL+C` in the terminal when you're done serving the site.

Clean up the worktrees from your original checkout:

```bash
git -C "$repo" worktree remove "$root/dev" --force
git -C "$repo" worktree remove "$root/master" --force
git -C "$repo" worktree remove "$root/gh-pages" --force
rm -rf "$root"
```

## What to check in the browser

At minimum, verify the following:

1. The home page loads and is not blank.
1. The navigation on the left opens the article or cmdlet page you changed.
1. Search opens without JavaScript errors.
1. The browser developer tools do not show 404 responses for `docfx.vendor.min.css` or `docfx.vendor.min.js`.

DocFX 2.77 and newer emit the vendor assets as minified files. If the browser shows 404 responses for `docfx.vendor.css` or `docfx.vendor.js`, the template is still using the old asset names and the site can appear as a blank page.

## Build warnings

DocFX can finish successfully while still reporting warnings. Treat warnings in files you touched as something to fix before submitting your pull request. Existing warnings elsewhere in the site should not block you from validating your local change, but do not introduce new ones.

Common warnings are broken file links, broken bookmarks, or links to generated cmdlet pages that are not present in a quick build. If you need to verify generated cmdlet pages, use the full build.