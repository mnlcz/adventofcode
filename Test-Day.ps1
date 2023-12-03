Param
(
    [Parameter(Mandatory = $true, Position = 0)]
    [String]$Year,
    
    [Parameter(Mandatory = $true, Position = 1)]
    [String]$Day
)

$Root = Get-Location
$TestName = "TestingDay$Day"
$Path = Join-Path $Root "$Year\csharp\$TestName\$TestName.csproj"

dotnet test $Path