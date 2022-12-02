<?php

function parse_into_sorted_sums($in)
{
    $in = explode("\n", $in);
    $map = [];
    $i = 0;
    $sum = 0;
    foreach ($in as $v)
    {
        if (empty($v))
        {
            $map[$i] = $sum;
            $sum = 0;
        }
        else
            $sum += intval($v);
        $i++;
    }
    arsort($map);
    return array_values($map);
}

function part1($sums) 
{ 
    echo "Part 1: $sums[0]\n"; 
}

function part2($sums)
{
    $sum = $sums[0] + $sums[1] + $sums[2];
    echo "Part 2: $sum";
}

$sums = parse_into_sorted_sums(file_get_contents("./inputs/1.txt"));
part1($sums);
part2($sums);
