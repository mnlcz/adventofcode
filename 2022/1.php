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
    echo "Part 1: max $max by elf $elf\n";
}

function part2($in)
{
    $arr = parse_into_string_arr($in);
    $len = count($arr);
    $map = [];
    $out = 0;
    for ($i = 0; $i < $len; $i++)
    {  
        $values = array_map('intval', explode("-", $arr[$i]));
        $calories_sum = array_sum($values);
        $map[$i] = $calories_sum; // map of INT(index) => INT(sum)
    }
    arsort($map); // sort map desc
    $it = (new ArrayObject($map))->getIterator(); // iterator is needed to iterate the map following the new placements of the keys
    $counter = 0;
    foreach ($it as $v) // iterate 3 times to get final result
    {
        if ($counter === 3)
            break;
        $out += $v;
        $counter++;
    }
    echo "Part 2: $out";
}

$in = file_get_contents("./inputs/1.txt");
part1($in);
part2($in);
