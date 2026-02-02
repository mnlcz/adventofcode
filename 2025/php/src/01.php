<?php

require __DIR__ . "/../vendor/autoload.php";

use AdventOfCode\Year2025\Extras\Input;

$sample = <<<END
L68
L30
R48
L5
R60
L55
L1
L99
R14
L82
END;
$input = Input::get(1);
$rotations = array_filter(explode(PHP_EOL, $input));
const MIN = 0;
const SIZE = 100;

part1($rotations);
part2($rotations);

function part1(array &$input): void
{
    $dial = 50;
    $count = 0;
    foreach ($input as $rotation) {
        $direction = substr($rotation, 0, 1);
        $n = substr($rotation, 1);

        if ($direction == "L") {
            $n = -$n;
        }

        $result = $dial + $n;
        $dial = wrap($result);

        if ($dial == 0) {
            $count++;
        }
    }
    echo "Part 1: $count\n";
}

function part2(array &$input): void
{
    $dial = 50;
    $count = 0;
    foreach ($input as $rotation) {
        $direction = substr($rotation, 0, 1);
        $n = substr($rotation, 1);

        if ($direction == "L") {
            $n = -$n;
        }

        $result = $dial + $n;

        if ($result == 0) {
            $count++;
        } elseif ($result > 0) {
            $count += intdiv($result, SIZE);
        } elseif ($result < 0) {
            $count += intdiv(abs($result) + SIZE, SIZE);
            if ($dial == 0) {
                $count--;
            }
        }

        $dial = wrap($result);
    }
    echo "Part 2: $count\n";
}

function wrap(int $num): int
{
    return (((($num - MIN) % SIZE) + SIZE) % SIZE) + MIN;
}
