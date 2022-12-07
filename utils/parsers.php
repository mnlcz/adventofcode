<?php

/**
 * Example:
 *      IN:
 *          TEXT:
 *              100
 *              200
 *              300
 *          CALLABLE: 'intval'
 *
 *      OUT: 
 *          INT ARRAY => [100, 200, 300]
 */
function parse_into_arr(string $input_name, ?callable $converter, bool $filter_empties = TRUE, bool $fix_carriage_return = FALSE): array
{
    $year = 2022;
    $in = file_get_contents("../$year/inputs/$input_name.txt");
    $arr = explode("\n", $in);
    if ($filter_empties)
        $arr = array_filter($arr);
    if ($fix_carriage_return)
    {
        $arr = array_map('rtrim', $arr);
    }
    return is_null($converter) ? $arr : array_map($converter, $arr);
}


/**
 *  Example:
 *      IN:
 *          TEXT:
 *              1000
 *              5000
 *
 *              2000
 *              6000
 *          CALLABLE: 'intval'
 *
 *      OUT: 
 *          MAP of type [INT, INT_ARRAY]
 *              [0] => [1000, 5000]
 *              [1] => [2000, 6000]
 */
function parse_into_chunks_map(string $input_name, ?callable $converter, string $separator = ""): array
{
    return $separator === "" ? blank_separator_logic($input_name, $converter) : default_logic($input_name, $converter, $separator);
}


/**
 *  Example:
 *      IN:
 *          TEXT:
 *              1000
 *              5000
 *
 *              2000
 *              6000
 *          CALLABLE: 'intval'
 *
 *      OUT: 
 *          MAP of type [INT, INT]
 *              [0] => [6000]
 *              [1] => [8000]
 */
function parse_into_sum_map(string $input_name, callable $converter, string $separator = ""): array
{
    $map = parse_into_chunks_map($input_name, $converter, $separator);
    $sum_map = [];
    foreach ($map as $k => $v)
        $sum_map[$k] = array_sum($v);
    return $sum_map;
}


/**
 * Example:
 *      IN:
 *          STRING: "1-2-3-4"
 *          SEPARATOR = "-"
 *
 *      OUT:
 *          INT ARRAY => [1, 2, 3, 4]
 */
function str_get_ints(string $in, string $separator): array
{
    return array_map('intval', explode($separator, $in));
}


/**
 * Example:
 *      IN:
 *          STRING: "1-3"
 *          SEPARATOR: "-"
 *
 *      OUT:
 *          INT ARRAY => [1, 2, 3]
 */
function str_get_range(string $in, string $separator): array
{
    [$l, $r] = str_get_ints($in, $separator);
    return range($l, $r);
}


// ---------------------------- IGNORE BELOW ----------------------------
function blank_separator_logic(string $input_name, ?callable $converter): array
{
    $arr = parse_into_arr($input_name, NULL, FALSE);
    $separator = "";
    $i = 0;
    $contents = [];
    $map = [];
    foreach ($arr as $value)
    {
        if ($value === $separator)
        {
            $map[$i] = $contents;
            $i++;
            $contents = [];
        }
        else
            $contents[] = is_null($converter) ? $value : call_user_func($converter, $value);
    }
    $map[] = $contents;
    return $map;
}

function default_logic(string $input_name, ?callable $converter, string $separator): array
{
    $arr = parse_into_arr($input_name, $converter);
    $map = [];
    $i = 0;
    foreach ($arr as $value)
    {
        $map[$i] = explode($separator, $value);
        $i++;
    }
    return $map;
}
