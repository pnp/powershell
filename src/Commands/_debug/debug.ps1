$env:PNP_PS_DEBUG_IN_VISUAL_STUDIO = $true
$env:PNPPOWERSHELL_UPDATECHECK = "Off"
$ProjectPath = $PSScriptRoot | Split-Path -Parent
$BinPath = "$ProjectPath\bin\Debug"

$dlls = @("PnP.PowerShell.ALC.dll", "PnP.PowerShell.dll")
$netversion = "net8.0"

$BinPath = "$BinPath\$netversion"

foreach ($dll in $dlls) {
  try {
    Import-Module "$BinPath\$dll" -Force -Global
  }
  catch {
    Write-Warning -Message "load.ps1: Import Modules -- $($_.Exception.Message)"
  }
}