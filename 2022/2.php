<?php

require "../utils/parsers.php";

$in = parse_into_chunks_map("2", NULL, " ");
part1($in);
part2($in);

function part1(array $in)
{
    $total = 0;
    foreach ($in as $round)
        $total += calculate_score($round);
    echo "Part 1: $total\n";
}

function part2(array $in)
{
    $total = 0;
    foreach ($in as $round)
    {
        [$opponent, $code] = $round;
        $player = Relationship::getOption($opponent, get_outcome($code));
        $total += calculate_score([$opponent, $player]);
    }
    echo "Part 2: $total";
}

class Relationship
{
    private static array $win = [
        "X" => "C",
        "Y" => "A",
        "Z" => "B"
    ];

    private static array $draw = [
        "X" => "A",
        "Y" => "B",
        "Z" => "C"
    ];

    private static array $lose = [
        "X" => "B",
        "Y" => "C",
        "Z" => "A"
    ];

    public static function checkWin(string $opponent, string $player): bool
    {
        return self::$win[$player] === $opponent;
    }

    public static function checkDraw(string $opponent, string $player): bool
    {
        return self::$draw[$player] === $opponent;
    }

    public static function getOption(string $opponent, string $option): string
    {
        $out = "error";
        $map = [];
        if ($option === "w")
            $map = self::$win;
        elseif ($option === "d")
            $map = self::$draw;
        elseif ($option === "l")
            $map = self::$lose;

        foreach ($map as $k => $v)
        {
            if ($v === $opponent)
            {
                $out = $k;
                break;
            }
        }
        return $out;
    }
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
    if (Relationship::checkWin($opponent, $mine))
        return 6;
    elseif (Relationship::checkDraw($opponent, $mine))
        return 3;
    else
        return 0;
}

function get_outcome(string $letter): string
{
    $out = "";
    switch($letter)
    {
        case "X": $out = "l"; break;
        case "Y": $out = "d"; break;
        default: $out = "w"; break;
    }
    return $out;
}

