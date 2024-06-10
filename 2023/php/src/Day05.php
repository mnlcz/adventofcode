<?php

namespace Aoc;

use Illuminate\Support\Collection;
use Illuminate\Support\Str;

require 'vendor/autoload.php';
require 'utils.php';

class Day05
{
    public static function part1(bool $testing): int
    {
        $in = Str::of(input($testing ? '5Sample1.txt' : '5.txt'))->trim()->explode(PHP_EOL);
        $seeds = Str::of($in->first())->after(': ')->explode(' ')->map(fn ($s) => intval($s));
        $maps = static::parseMaps($in);
        $res = [];

        foreach ($seeds as $seed) {
            $res[] = static::process($seed, $maps);
        }

        return min($res);
    }

    public static function part2(bool $testing): int
    {
        $in = Str::of(input($testing ? '5Sample1.txt' : '5.txt'))->trim()->explode(PHP_EOL);
        $seeds = Str::of($in->first())->after(': ')->explode(' ')->map(fn ($s) => intval($s))->chunk(2)->map(fn ($a) => array_values($a->toArray()))->toArray();
        $maps = static::parseMaps2($in);
        $aux = INF;
        $res = [];

        foreach ($seeds as $seed) {
            [$start, $r] = $seed;
            $cur_intervals = [[$start, $start + $r - 1]];
            $new_intervals = [];

            foreach ($maps as $m) {
                foreach ($cur_intervals as $ci) {
                    [$lo, $hi] = $ci;
                    foreach (static::remap($lo, $hi, $m) as $new_interval) {
                        $new_intervals[] = $new_interval;
                    }
                }
                $cur_intervals = $new_intervals;
                $new_intervals = [];
            }

            foreach ($cur_intervals as $ci) {
                [$lo, $hi] = $ci;
                $aux = min($aux, $lo);
                if ($aux > 0) {
                    $res[] = $aux;
                }
            }
        }

        return min($res);
    }

    private static function parseMaps(Collection $in): array
    {
        $maps = [];
        $i = 2;

        while ($i < count($in)) {
            $maps[] = [];
            $i++;
            while ($i < count($in) && $in[$i] !== '') {
                [$ds, $ss, $len] = Str::of($in[$i])->explode(' ')->map(fn ($s) => intval($s));
                $maps[count($maps) - 1][] = [$ds, $ss, $len];
                $i++;
            }
            $i++;
        }

        return $maps;
    }

    private static function parseMaps2(Collection $in): array
    {
        $maps = [];
        $i = 2;

        while ($i < count($in)) {
            $maps[] = [];
            $i++;
            while ($i < count($in) && $in[$i] !== '') {
                [$ds, $ss, $len] = Str::of($in[$i])->explode(' ')->map(fn ($s) => intval($s));
                $maps[count($maps) - 1][] = [$ds, $ss, $len];
                $i++;
            }
            $maps[count($maps) - 1] = collect($maps[count($maps) - 1])->sort(fn ($a, $b) => $a[1] < $b[1] ? $a : $b);
            $i++;
        }

        return $maps;
    }

    private static function process(int $seed, array &$maps): int
    {
        $c = $seed;

        foreach ($maps as $map) {
            foreach ($map as $m) {
                [$ds, $ss, $len] = $m;
                if ($ss <= $c && $c < $ss + $len) {
                    $c = $ds + $c - $ss;
                    break;
                }
            }
        }

        return $c;
    }

    private static function remap($lo, $hi, $m)
    {
        $ans = [];

        foreach ($m as $_s) {
            [$ds, $src, $r] = $_s;
            $end = $src + $r - 1;
            $r = $ds - $src;

            if (! ($end < $lo || $src > $hi)) {
                $ans[] = [max($src, $lo), min($end, $hi), $r];
            }
        }

        for ($i = 0; $i < count($ans); $i++) {
            $interval = $ans[$i];
            [$l, $r, $d] = $interval;
            yield [$l + $d, $r + $d];

            if ($i < count($ans) - 1 && $ans[$i + 1][0] > $r + 1) {
                yield [$r + 1, $ans[$i + 1][0] - 1];
            }
        }

        if (empty($ans)) {
            yield [$lo, $hi];

            return;
        }

        if ($ans[0][0] != $lo) {
            yield [$lo, $ans[0][0] - 1];
        }

        if ($ans && $ans[count($ans) - 1][1] != $hi) {
            yield [$ans[count($ans) - 1][1] + 1, $hi];
        }
    }
}

echo 'Part 1: '.Day05::part1(false).PHP_EOL;
echo 'Part 2: '.Day05::part2(false).PHP_EOL;
