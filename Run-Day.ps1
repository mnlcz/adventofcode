Param
(
    [Parameter(Mandatory = $true, Position = 0)]
    [String]$Year,
    
    [Parameter(Mandatory = $true, Position = 1)]
    [String]$Day,

    [Parameter(Mandatory = $true, Position = 2)]
    [String]$Lang
)

$Root = Get-Location

if ($Lang.ToUpper() -eq "CS") {
    $ProjName = "Day$Day"
    $Path = Join-Path $Root "$Year\csharp\$ProjName\$ProjName.csproj"

    dotnet run --project $Path
} elseif ($Lang.ToUpper() -eq "PHP") {
    $PhpPath = Join-Path $Root "$Year\php"
    
    Set-Location $PhpPath
    
    php ".\src\Day$Day.php"

    Set-Location $Root
}
