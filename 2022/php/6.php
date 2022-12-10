<?php

$in = file_get_contents("../inputs/6.txt");
part1($in);
part2($in);

function part1(string $chars): void
{
    $value = get_marker($chars, 4);
    echo "Part 1: $value\n";
}

function part2(string $chars): void
{
    $value = get_marker($chars, 14);
    echo "Part 2: $value";

}

function get_marker(string $chars, int $marker_len): int
{
    $value = -1;
    $len = strlen($chars);
    for ($i = 0; $i < $len - 3; $i++)
    {
        $str = substr($chars, $i, $marker_len);
        if (has_unique_chars($str))
        {
            $value = $i + $marker_len;
            break;
        }
    }
    return $value;
}

function has_unique_chars(string $str): bool
{
    $chars = str_split($str);
    $chars_before = [];
    foreach ($chars as $char)
    {
        if (in_array($char, $chars_before))
            return false;
        $chars_before[] = $char;
    }
    return true;
}
