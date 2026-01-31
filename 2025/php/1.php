<?php

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
$input = file_get_contents('../inputs/1.in');

$dial = 50;
$count = 0;
$rotations = array_filter(explode(PHP_EOL, $input));

foreach ($rotations as $rotation) {
    $direction = substr($rotation, 0, 1);
    $n = substr($rotation, 1);

    if ($direction == 'R') {
        $result = $dial + $n;
        $dial = wrap($result, 0, 99);
    } elseif ($direction == 'L') {
        $result = $dial - $n;
        $dial = wrap($result, 0, 99);
    }

    if ($dial == 0) {
        $count++;
    }
}
echo "Part 1: $count\n";

function wrap(int $num, int $left, int $right): int
{
    $size = $right - $left + 1;
    return (($num - $left) % $size) + $left;
}
