<?php

require "../utils/parsers.php";

$in = parse_into_arr("3", NULL);
part1($in);
part2($in);

function part1(array $in): void
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

function part2(array $in): void
{
    $groups = separate_into_groups($in);
    $sum = 0;
    foreach ($groups as $group_items)
    {
        $common_item = find_common_item(...$group_items);
        $sum += get_value($common_item);
    }
    echo "Part 2: $sum";
}

function find_common_item(string $str1, string $str2, string $str3 = NULL): string
{
    $item = "error";
    $chars = str_split($str1);
    foreach ($chars as $char)
    {
        if (str_contains($str2, $char))
        {
            if (is_null($str3))
            {
                $item = $char;
                break;
            }
            else
            {
                if (str_contains($str3, $char))
                {
                    $item = $char;
                    break;
                }
            }
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

function find_value(string $search, array $arr): int
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

function separate_into_groups(array $in): array
{
    $map = [];
    $items = [];
    $key = 0;
    foreach ($in as $str)
    {
        if (count($items) === 3)
        {
            $map[$key] = $items;
            $items = [];
            $key++;
        }
        $items[] = $str;
    }
    $map[] = $items;
    return $map;
}
