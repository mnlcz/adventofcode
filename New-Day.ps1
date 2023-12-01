Param
(
    [Parameter(Mandatory = $true, Position = 0)]
    [String]$Year,
    
    [Parameter(Mandatory = $true, Position = 1)]
    [String]$Day
)

$Root = Get-Location
$Path = Join-Path $Root "$Year\csharp"
$ProjName = "Day$Day"
$TestName = "TestingDay$Day"
$ProjPath = Join-Path $Path "$ProjName\$ProjName.csproj"
$TestPath = Join-Path $Path "$TestName\$TestName.csproj"
$UtilsPath = Join-Path $Root "utils\csharp\Utils\Utils.csproj"

Set-Location $Path

dotnet new console -o $ProjName
dotnet new xunit -o $TestName

Set-Location $Root

dotnet sln add $ProjPath
dotnet sln add $TestPath

dotnet add $ProjPath reference $UtilsPath
dotnet add $TestPath reference $UtilsPath