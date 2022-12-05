<?php

require "../utils/parsers.php";

[$crates, $moves] = parse_into_chunks_map("5", NULL);
$qs = get_queues($crates);
$mvs = get_moves($moves);
part1($qs, $mvs);

function part1(array $queues, array $moves): void
{
    foreach ($moves as $move)
        execute_move($queues, $move);
    $msg = get_message($queues);
    echo "Part 1: $msg\n";
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

function execute_move(array &$queues, array $move): void
{
    [$amount, $from, $to] = [$move[0], $move[1] - 1, $move[2] - 1];
    while ($amount != 0)
    {
        $element = array_shift($queues[$from]);
        $queues[$to] = array_merge(array(0 => $element), $queues[$to]);
        $amount--;
    }
}

function get_message(array $queues): string
{
    $msg = "";
    ksort($queues);
    foreach ($queues as $q)
        $msg .= $q[0][1];
    return $msg;
}