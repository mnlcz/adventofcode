<?php

namespace Aoc;

use Illuminate\Support\Str;

require 'vendor/autoload.php';
require 'utils.php';

class Day07
{
    public static function part1(bool $testing): int
    {
        $in = Str::of(input($testing ? '07Sample1.txt' : '07.txt'))->trim()->explode(PHP_EOL);
        $rounds = collect($in)->map(fn ($l) => explode(' ', $l))->map(fn ($r) => [$r[0], intval($r[1])]);
        $hands = $rounds->map(fn ($r) => $r[0])->toArray();
        $sorted = collect(static::bubbleSort($hands));
        $total = 0;

        foreach ($sorted as $i => $hand) {
            $rank = $i + 1;
            $bid = $rounds->where(fn ($r) => $r[0] === $hand)->flatten()[1];
            $total += $rank * $bid;
        }

        return $total;
    }

    public static function part2(bool $testing): int
    {
        return PHP_INT_MAX;
    }

    private static function bubbleSort(array $hands): array
    {
        $n = count($hands);
        for ($i = 0; $i < $n - 1; $i++) {
            for ($j = 0; $j < $n - $i - 1; $j++) {
                if (static::winnerHand($hands[$j], $hands[$j + 1]) === $hands[$j]) {
                    $temp = $hands[$j];
                    $hands[$j] = $hands[$j + 1];
                    $hands[$j + 1] = $temp;
                }
            }
        }

        return $hands;
    }

    public static function winnerHand(string $h1, string $h2): string
    {
        [$t1, $t2] = [static::handType($h1), static::handType($h2)];

        if ($t1 != $t2) {
            return $t1->value < $t2->value ? $h1 : $h2;
        }

        for ($i = 0; $i < strlen($h1); $i++) {
            [$c1, $c2] = [$h1[$i], $h2[$i]];

            if ($c1 === $c2) {
                continue;
            }

            return static::$values[$c1] < static::$values[$c2] ? $h1 : $h2;
        }

        exit('FAIL');
    }

    public static function handType(string $hand): HandType
    {
        $counts = collect(str_split($hand))->countBy();

        return match (true) {
            $counts->count() === 1 => HandType::FiveOfKind,
            $counts->count() === 2 && $counts->contains(1) => HandType::FourOfKind,
            $counts->count() === 2 && $counts->contains(2) => HandType::FullHouse,
            $counts->count() === 3 && $counts->contains(2) => HandType::TwoPair,
            $counts->count() === 3 => HandType::ThreeOfKind,
            $counts->count() === 4 => HandType::OnePair,
            $counts->count() === 5 => HandType::HighCard
        };
    }

    private static array $values = [
        'A' => 1,
        'K' => 2,
        'Q' => 3,
        'J' => 4,
        'T' => 5,
        '9' => 6,
        '8' => 7,
        '7' => 8,
        '6' => 9,
        '5' => 10,
        '4' => 11,
        '3' => 12,
        '2' => 13,
    ];
}

enum HandType: int
{
    case FiveOfKind = 0;
    case FourOfKind = 1;
    case FullHouse = 2;
    case ThreeOfKind = 3;
    case TwoPair = 4;
    case OnePair = 5;
    case HighCard = 6;
}

// Day07::part1(true);
$start = microtime(true);
echo 'Part 1: '.Day07::part1(false).PHP_EOL;
$end = microtime(true) - $start;
echo "Time: $end seconds";
// echo 'Part 2: '.Day07::part2(false).PHP_EOL;
