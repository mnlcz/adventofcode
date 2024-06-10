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
    $TestName = "TestingDay$Day"
    $Path = Join-Path $Root "$Year\csharp\$TestName\$TestName.csproj"

    dotnet test $Path
} elseif($Lang.ToUpper() -eq "PHP") {
    $PhpPath = Join-Path $Root "$Year\php"
    
    Set-Location $PhpPath
    
    $Pest = ".\vendor\bin\pest"
    $TestPath = Join-Path $PhpPath "tests\Unit\Day${Day}Test.php"
    
    & $Pest $TestPath
    
    Set-Location $Root
}
