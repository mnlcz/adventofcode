Param
(
    [Parameter(Mandatory = $true, Position = 0)]
    [String]$Year,
    
    [Parameter(Mandatory = $true, Position = 1)]
    [String]$Day,

    [Parameter(Mandatory = $true, Position = 2)]
    [String]$Lang
)

if ($Lang.ToUpper() -eq "CS") {
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
} elseif ($Lang.ToUpper() -eq "PHP") {
    $Root = Get-Location
    $PhpPath = Join-Path $Root "$Year\php"
    $SrcPath = Join-Path $PhpPath "\src"
    $TestPath = Join-Path $PhpPath "\tests\Unit"
    $Name = "Day$Day"
    $Template = @"
<?php

namespace Aoc;

use Illuminate\Support\Str;

require 'vendor/autoload.php';
require 'utils.php';

class $Name
{
    public static function part1(bool `$testing): int
    {
        `$in = Str::of(input(`$testing ? '5Sample1.txt' : '5.txt'))->trim()->explode(PHP_EOL);

        return PHP_INT_MIN;
    }

    public static function part2(bool `$testing): int
    {
        return PHP_INT_MIN;
    }
}

// echo 'Part 1: '.$Name::part1(false).PHP_EOL;
// echo 'Part 2: '.$Name::part2(false).PHP_EOL;
"@

    $TestTemplate = @"
<?php

describe('Part 1', function () {
    test('with sample', function () {
        expect(Aoc\$Name::part1(true))->toBe(PHP_INT_MIN);
    });

    test('with real input', function () {
        expect(Aoc\$Name::part1(false))->toBe(PHP_INT_MIN);
    });
});

describe('Part 2', function () {
    test('with part of the sample', function () {
        expect(Aoc\$Name::part2(true))->toBe(PHP_INT_MIN);
    });

    test('with real input', function () {
        expect(Aoc\$Name::part2(false))->toBe(PHP_INT_MIN);
    });
});
"@

    Set-Location $SrcPath
    Add-Content -Path "$Name.php" -Value $Template -Encoding utf8

    Set-Location $TestPath
    Add-Content -Path "${Name}Test.php" -Value $TestTemplate -Encoding utf8

    Set-Location $Root
}

