Param(
    [Parameter(Mandatory = $true)]
    [String]
    $Version
)

try {

    Write-Host "Generating PnP.PowerShell predictor commands for version $Version" -ForegroundColor Yellow

    $json = @();

    # get all files in the srcfiles folder
    $files = Get-ChildItem -Path ".\documentation" -Filter "*.md" -Recurse;

    # set id to 1
    $id = 1;

    # loop through each file
    $files | ForEach-Object {
        
        # get file name without extension
        $baseName = $_.BaseName;

        # get the file data
        $fileData = Get-Content $_.FullName -Raw;
        # create a regex pattern to match the example code
        $pattern = "(?s)(?<=### EXAMPLE .*``````powershell)(.*?)(?=``````)"

        if($baseName -eq "connect-pnponline") {
            $pattern = "(?s)(?<=### EXAMPLE .*``````)(.*?)(?=``````)";
        }

        # ignore case for regex
        $options = [Text.RegularExpressions.RegexOptions]'IgnoreCase, CultureInvariant'

        $result = [regex]::Matches($fileData, $pattern, $options);

        $i = 1;
        foreach ($item in $result) {

            $value = $item.Value.Trim();

            # replace \n with a semicolon
            $value = $value.Replace("`n", " ; ");


            # if the item value begins with the name of the file then add it to the json
            if ($value.ToLower() -match "^$($baseName.ToLower()).*") {
                $json += @{
                    "CommandName" = $baseName
                    "Command" = $value
                    "Rank" = $i
                    "Id" = $id
                }
                $i++;
                $id++;
            }
        }
    }

    # check if predictor folder exists in ..\resources folder
    if (!(Test-Path -Path ".\resources\predictor")) {
        # create the folder
        New-Item -ItemType Directory -Path ".\resources\predictor" -Force;
    }

    # write the json to a file
    $json | ConvertTo-Json -Depth 10 | Out-File -FilePath ".\resources\predictor\PnP.PowerShell.Suggestions.$($Version).json" -Encoding UTF8 -Force;   
}
catch {
    Write-Error $_.Exception.Message;
    Write-Error "Error: Cannot generate predictor commands";
    exit 1;
}
