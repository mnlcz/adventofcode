<?php

require "../utils/parsers.php";

[$crates, $moves] = parse_into_arr_multi_separator("5", "\n\n", "\n", NULL);
$qs = get_queues($crates);
$mvs = get_moves($moves);
part1($qs, $mvs);
part2($qs, $mvs);

function part1(array $queues, array $moves): void
{
    foreach ($moves as $move)
        move_one_by_one($queues, $move);
    $msg = get_message($queues);
    echo "Part 1: $msg\n";
}

function part2(array $queues, array $moves): void
{
    foreach ($moves as $move)
        move_multiple($queues, $move);
    $msg = get_message($queues);
    echo "Part 2: $msg";
}

function get_queues(array $crates): array
{
    $queues = [];
    foreach ($crates as $line)
    {
        if (str_contains($line, "1"))
            break;
        $values = str_split($line, 4);
        foreach ($values as $k => $v)
        {
            if ($v[0] === " ")
                continue;
            $queues[$k][] = $v;
        }
    }
    return $queues;
}

function get_moves(array $moves): array
{
    $mvs = [];
    foreach ($moves as $move)
    {
        $strings = explode(" ", $move);
        $nums = [];
        foreach ($strings as $str)
            if (is_numeric($str))
                $nums[] = intval($str);
        $mvs[] = $nums;
    }
    return $mvs;
}

function move_one_by_one(array &$queues, array $move): void
{
    [$amount, $from, $to] = [$move[0], $move[1] - 1, $move[2] - 1];
    while ($amount != 0)
    {
        move_one($queues, $from, $to);
        $amount--;
    }
}

function move_multiple(array &$queues, array $move): void
{
    [$amount, $from, $to] = [$move[0], $move[1] - 1, $move[2] - 1];
    if ($amount === 1)
        move_one($queues, $from, $to);
    else
    {
        $elements = [];
        while ($amount != 0)
        {
           $element = array_shift($queues[$from]);
           $elements = array_merge(array(0 => $element), $elements);
           $amount--;
        }
        foreach ($elements as $element)
           $queues[$to]  = array_merge(array(0 => $element), $queues[$to]);
    }
}

function move_one(array &$queues, int $from, int $to): void
{
    $element = array_shift($queues[$from]);
    $queues[$to] = array_merge(array(0 => $element), $queues[$to]);
}

function get_message(array $queues): string
{
    $msg = "";
    ksort($queues);
    foreach ($queues as $q)
        $msg .= $q[0][1];
    return $msg;
}
