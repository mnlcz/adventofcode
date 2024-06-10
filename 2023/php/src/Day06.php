<?php

namespace Aoc;

use Illuminate\Support\Collection;
use Illuminate\Support\Str;

require 'vendor/autoload.php';
require 'utils.php';

class Day06
{
    public static function part1(bool $testing): int
    {
        [$times, $distances] = Str::of(input($testing ? '06Sample1.txt' : '06.txt'))->trim()->explode(PHP_EOL);
        $times = Str::of($times)->explode(' ')->reject(fn ($i) => empty($i))->skip(1)->map(fn ($s) => intval($s));
        $distances = Str::of($distances)->explode(' ')->reject(fn ($i) => empty($i))->skip(1)->map(fn ($s) => intval($s));

        $races = $times->combine($distances);
        $options = collect();

        foreach ($races as $time_lasting => $distance_record) {
            $options->push(static::waysToWin($time_lasting, $distance_record)->count());
        }

        $ans = $options->reduce(fn ($carry, $i) => $carry * $i, 1);

        return $ans;
    }

    public static function part2(bool $testing): int
    {
        [$time, $distance] = Str::of(input($testing ? '06Sample1.txt' : '06.txt'))->trim()->explode(PHP_EOL);
        $time = intval(Str::of($time)->remove(' ')->after(':')->toString());
        $distance = intval(Str::of($distance)->remove(' ')->after(':')->toString());

        $ans = static::optimizedWaysToWin($time, $distance);

        return $ans;
    }

    private static function waysToWin(int $t, int $d): Collection
    {
        $ways = collect();
        $time = $t - 1; // Time could never be its original value, because we need at least 1ms to charge the boat's speed
        $won = false;

        for ($speed = 1; $speed < $t; $speed++) {
            $traveled = $time * $speed;
            $time--;

            // If we already won a race and we lose now it means we wasted too much time on charging the speed
            if ($won && $traveled < $d) {
                break;
            }

            if ($traveled > $d) {
                $ways->push($traveled); // We only care about the times we win
                $won = true;
            }
        }

        return $ways;
    }

    private static function optimizedWaysToWin(int $t, int $d): int
    {
        $ways = 0;
        $time = $t - 1;
        $won = false;

        for ($speed = 1; $speed < $t; $speed++) {
            $traveled = $time * $speed;
            $time--;

            if ($won && $traveled < $d) {
                break;
            }

            if ($traveled > $d) {
                $ways++;
                $won = true;
            }
        }

        return $ways;
    }
}

echo 'Part 1: '.Day06::part1(false).PHP_EOL;
echo 'Part 2: '.Day06::part2(false).PHP_EOL;
