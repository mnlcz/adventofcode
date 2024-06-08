<?php

namespace Aoc;

require 'vendor/autoload.php';
require 'utils.php';

use Illuminate\Support\Str;

class Day05
{
    public static function part1(bool $testing): int
    {
        [$seeds, $rest] = $testing ? static::input('5Sample1.txt') : static::input('5.txt');

        $out = [];

        foreach ($seeds as $seed) {
            $out[] = static::cycle($rest, $seed);
        }

        return min($out);
    }

    // FIXME: bruteforce solution takes too long. Works on the example tho
    public static function part2(bool $testing): int
    {
        [$seeds, $rest] = $testing ? static::input('5Sample1.txt') : static::input('5.txt');
        $seeds = $testing ? [79, 14, 55, 13] : $seeds; // Example given as a hint
        $out = [];

        for ($i = 0; $i < count($seeds); $i+=2) {
            $start = $seeds[$i];
            $len = $seeds[$i + 1];

            while ($len-- > 0) {
                $seed = $start + $len;
                $out[] = static::cycle($rest, $seed);
            }
        }

        return min($out);
    }

    public static function input(string $file): array
    {
        $in = Str::of(input($file))->explode(PHP_EOL.PHP_EOL);

        $seeds = Str::of($in->first())->after('seeds: ')->explode(' ')->map(fn ($s) => intval($s))->toArray();
        $rest = $in->skip(1)->map(fn ($s) => Str::trim($s))->map(fn ($s) => Str::of($s)->explode(PHP_EOL)->skip(1))->map(fn ($m) => $m->map(fn ($s) => Str::of($s)->explode(' ')->map(fn ($s) => intval($s))))->toArray();

        return [array_values($seeds), array_values($rest)];
    }

    public static function cycle(array $maps, int $n): int
    {
        foreach ($maps as $m) {
            $n = static::match($m, $n);
        }

        return $n;
    }

    public static function match(array $maps, int $n): int
    {
        $out = $n;

        foreach ($maps as $numbers) {
            [$destination_start, $origin_start, $length] = $numbers;

            // Check if $n can match with the range
            if ($n >= $origin_start && $n <= $origin_start + $length) {
                return $destination_start - $origin_start + $n;
            }
        }

        return $out;
    }
}

// echo 'Part 1: '.Day05::part1(false).PHP_EOL;
echo 'Part 2: '.Day05::part2(false).PHP_EOL;
