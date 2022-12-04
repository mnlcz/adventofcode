<?php

require "../utils/parsers.php";

$in = parse_into_chunks_map("4", NULL, ",");
part1($in);
part2($in);

function part1(array $in): void
{
    $counter = count_contains($in, fn($pair) => contains_all(...$pair));
    echo "Part 1: $counter\n";
}

function part2(array $in): void
{
    $counter = count_contains($in, fn($pair) => contains_any(...$pair));
    echo "Part 2: $counter";
}

function count_contains(array $in, callable $contain_logic): int
{
    str_to_ranges($in);
    $counter = 0;
    foreach ($in as $pair)
    {
        if (call_user_func($contain_logic, $pair))
            $counter++;
    }
    return $counter;
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

function contains_any(array $arr1, array $arr2): bool
{
    foreach ($arr1 as $n)
        if (in_array($n, $arr2))
            return true;
    return false;
}

function contains_all(array $arr1, array $arr2): bool
{
    /**
     * array_diff returns an array with the elements of $arr1 that are not in $arr2
     *      if that array its empty it means that $arr2 CONTAINS all the elements of $arr1
     * I check it twice swapping the places because $arr1 and $arr2 may have different length
     *      Example:
     *          $a1 = [2, 3, 4, 5, 6, 7, 8] and $a2 = [3, 4, 5, 6, 7]
     *          empty(array_diff($a1, $a2)) ==> FALSE
     *                  because $a2 cant contain $a1 because it has a different length
     *          empty(array_diff($a2, $a1)) ==> TRUE
     *                  because $a1 is large enough to contain $a2 and also checks array_diff
     */
    return empty(array_diff($arr1, $arr2)) || empty(array_diff($arr2, $arr1));
}
