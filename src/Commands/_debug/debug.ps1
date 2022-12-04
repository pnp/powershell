$ProjectPath = $PSScriptRoot | Split-Path -Parent
$BinPath = "$ProjectPath\bin\Debug"

$dlls = @("PnP.PowerShell.ALC.dll", "PnP.PowerShell.dll")

$netversion = "net6.0-windows"

if ($PSEdition -eq 'Core') {    
  $netversion = "net6.0-windows"    
}
else {
  $netversion = "net462"
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