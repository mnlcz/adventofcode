<?php

require "../utils/php/parsers.php";

function part1(array $sorted_sums): void
{
    echo "Part 1: $sorted_sums[0]\n";
}

function part2(array $sorted_sums): void
{
    $top3 = $sorted_sums[0] + $sorted_sums[1] + $sorted_sums[2];
    echo "Part 2: $top3";
}

$in = parse_into_sum("1", "\n\n", "\n", 'intval');
arsort($in);
$in = array_values($in);
part1($in);
part2($in);
