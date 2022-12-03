<?php

/**
 * IF $converter is NULL:
 *      OUT: STRING_ARRAY
 * ELSE:
 *      OUT: ARRAY of type $converter result
 *
 * Example:
 *      IN: $converter = 'intval' 
 *      OUT: INT ARRAY
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
 * IF $converter is NULL:
 *      OUT: MAP where:
 *          INT KEY = sample chunk index
 *          STRING_ARRAY VALUE = input values until separator
 * ELSE:
 *      OUT: MAP where:
 *          INT KEY = sample chunk index
 *          $converter result ARRAY VALUE = input value until separator
 *
 *  Example:
 *      SAMPLE TEXT:
 *          1000
 *          5000
 *
 *          2000
 *          6000
 *      IN: $converter = 'intval'
 *      OUT: MAP of type [INT, INT_ARRAY]
 *          [0] => [1000, 5000]
 *          [1] => [2000, 6000]
 *          
 */
function parse_into_chunks_map(string $input_name, ?callable $converter, string $separator = ""): array
{
    return $separator === "" ? blank_separator_logic($input_name, $converter) : default_logic($input_name, $converter, $separator);
}

/**
 *  Example:
 *      SAMPLE TEXT:
 *          1000
 *          5000
 *
 *          2000
 *          6000
 *      IN: $converter = 'intval'
 *      OUT: MAP of type [INT, INT]
 *          [0] => [6000]
 *          [1] => [8000]
 */
function parse_into_sum_map(string $input_name, callable $converter, string $separator = ""): array
{
    $map = parse_into_chunks_map($input_name, $converter, $separator);
    $sum_map = [];
    foreach ($map as $k => $v)
        $sum_map[$k] = array_sum($v);
    return $sum_map;
}

// HELPER FUNCTIONS
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
    return $map;
}

function default_logic(string $input_name, ?callable $converter, string $separator): array
{
    $arr = parse_into_arr($input_name, NULL, TRUE);
    $map = [];
    $i = 0;
    foreach ($arr as $value)
    {
        $map[$i] = explode($separator, $value);
        $i++;
    }
    return $map;
}
