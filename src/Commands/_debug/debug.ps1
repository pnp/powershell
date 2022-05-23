$ProjectPath = $PSScriptRoot | Split-Path -Parent
$BinPath = "$ProjectPath\bin\Debug"

$dlls = @("PnP.PowerShell.ALC.dll", "PnP.PowerShell.dll")

$netversion = "netcoreapp3.1"

if ($PSEdition -eq 'Core') {
  $netversion = "netcoreapp3.1"
}
else {
  $netversion = "net461"
}

$BinPath = "$BinPath\$netversion"

foreach ($dll in $dlls) {
  try {
    Import-Module "$BinPath\$dll" -Force -Global
  }
  catch {
    Write-Warning -Message "load.ps1: Import Modules -- $($_.Exception.Message)"
  }
}