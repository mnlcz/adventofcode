<?php

require "../utils/parsers.php";

$in = parse_into_chunks_map("4", NULL, ",");
part1($in);

function part1(array $in): void
{
    str_to_ranges($in);
    $counter = 0;
    foreach ($in as $pair)
    {
        [$elf1, $elf2] = $pair;
        // array_diff returns an array with the elements that are not in common
        $contains = empty(array_diff($elf1, $elf2)) || empty(array_diff($elf2, $elf1));
        if ($contains)
            $counter++;
    }
    echo "Part 1: $counter\n";
}

function str_to_ranges(array &$in): void
{
    foreach ($in as $k => $pair)
    {
        [$elf1, $elf2] = $pair;
        $rng1 = str_get_range($elf1, "-");
        $rng2 = str_get_range($elf2, "-");
        $in[$k] = [$rng1, $rng2];
    }
}

