<?php

require "../utils/parsers.php";

function part1(array $in)
{
    $total = 0;
    foreach ($in as $round)
        $total += calculate_score($round);
    echo "Part 1: $total\n";
}

function calculate_score(array $round): int
{
    [$opponent, $mine] = $round;
    $score = 0;
    switch ($mine)
    {
        case "X":
            $score = 1;
            break;
        case "Y":
            $score = 2;
            break;
        default:
            $score = 3;
            break;
    }
    $score += round_result($opponent, $mine);
    return $score;
}

function round_result(string $opponent, string $mine): int
{
    $win = [
        "X" => "C",
        "Y" => "A",
        "Z" => "B"
    ];
    $draw = [
        "X" => "A",
        "Y" => "B",
        "Z" => "C"
    ];
    if ($win[$mine] === $opponent)
        return 6;
    elseif ($draw[$mine] === $opponent)
        return 3;
    else
        return 0;
}

$in = parse_into_chunks_map("2", NULL, " ");
part1($in);
