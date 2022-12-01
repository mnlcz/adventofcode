<?php
// Elves separated by: ","
// Values separated by: "-"
function parse_into_string_arr($in)
{
    $in = str_replace("\n", "-", $in); // separate all values
    $in = str_replace("--", ",", $in); // separate elves
    $in = rtrim($in, "-");
    return explode(",", $in);
}

function part1($in)
{
    $arr = parse_into_string_arr($in);
    $max = -1;
    $elf = -1;
    $len = count($arr);
    for ($i = 0; $i < $len; $i++)
    {
        $values = array_map('intval', explode("-", $arr[$i]));
        $calories_sum = array_sum($values);
        if ($calories_sum > $max)
        {
            $elf = $i + 1;
            $max = $calories_sum;
        }
    }
    echo "Max: $max by elf $elf";
}

$in = file_get_contents("./inputs/1.txt");
part1($in);
