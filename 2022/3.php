<?php

require "../utils/parsers.php";

$in = parse_into_arr("3", NULL);
part1($in);

function part1(array $in)
{
    $sum = 0;
    foreach ($in as $rucksack)
    {
        [$item1, $item2] = str_split($rucksack, strlen($rucksack) / 2);
        $common_item = find_common_item($item1, $item2);
        $sum += get_value($common_item);
    }
    echo "Part 1: $sum\n";
}

function find_common_item(string $str1, string $str2): string
{
    $item = "error";
    $chars = str_split($str1);
    foreach ($chars as $char)
    {
        if (str_contains($str2, $char))
        {
            $item = $char;
            break;
        }
    }
    return $item;
}

function get_value(string $char): int
{
    $letters = ctype_upper($char) ? range("A", "Z") : range("a", "z");
    $value = find_value($char, $letters);
    return ctype_upper($char) ? $value + 27 : $value + 1;
}

function find_value(string $search, array $arr)
{
    $value = 0;
    foreach ($arr as $char)
    {
        if ($char === $search)
            break;
        $value++;
    }
    return $value;
}
