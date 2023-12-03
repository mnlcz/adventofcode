Param
(
    [Parameter(Mandatory = $true, Position = 0)]
    [String]$Year,
    
    [Parameter(Mandatory = $true, Position = 1)]
    [String]$Day
)

$Root = Get-Location
$ProjName = "Day$Day"
$Path = Join-Path $Root "$Year\csharp\$ProjName\$ProjName.csproj"

dotnet run --project $Path
