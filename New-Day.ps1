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

Set-Location $ProjName
New-Item "$ProjName.cs"

$Template = @"
using Utils;

namespace $ProjName;

public sealed class Solution$Day : ISolution
{
    public string Part1(string filename)
    {
        throw new NotImplementedException();
    }

    public string Part2(string filename)
    {
        throw new NotImplementedException();
    }
}
"@

$TestTemplate = @"
using $ProjName;

namespace TestingDay$Day;

public class PartsTest
{
    private readonly Solution$Day _sol = new();

    [Fact]
    public void SamplePart1Test() => Assert.Equal("", _sol.Part1("${Day}Sample1"));

    [Fact]
    public void InputPart1Test() => Assert.Equal("", _sol.Part1("$Day"));

    [Fact]
    public void SamplePart2Test() => Assert.Equal("", _sol.Part2("${Day}Sample1"));

    [Fact]
    public void InputPart2Test() => Assert.Equal("", _sol.Part2("$Day"));
}
"@

Add-Content -Path "$ProjName.cs" -Value $Template -Encoding utf8

Set-Location "$Path\$TestName"
Remove-Item -Path "UnitTest1.cs"
Add-Content -Path "PartsTest.cs" -Value $TestTemplate -Encoding utf8

Set-Location $Root

dotnet sln add $ProjPath
dotnet sln add $TestPath

dotnet add $ProjPath reference $UtilsPath
dotnet add $TestPath reference $UtilsPath
dotnet add $TestPath reference $ProjPath

dotnet build