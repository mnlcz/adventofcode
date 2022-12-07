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
function parse_into_arr(string $input_name, string $separator, ?callable $converter): array
{
    $year = 2022;
    $in = file_get_contents("../$year/inputs/$input_name.txt");
    fix_win_carriage_return($separator, $in);
    $in = rtrim($in);
    $arr = explode($separator, $in);
    return is_null($converter) ? $arr : array_map($converter, $arr);
}


/**
 * Example:
 *      IN:
 *          TEXT:
 *              100
 *              200
 *
 *              300
 *              400
 *          SEPARATOR1: \n\n
 *          SEPARATOR2: \n
 *          CONVERTER: 'intval'
 *
 *      OUT:
 *          INT ARRAY => [[100, 200], [300, 400]]
 */
function parse_into_arr_multi_separator(string $input_name, string $separator1, string $separator2, ?callable $converter): array
{
    $in = parse_into_arr($input_name, $separator1, NULL);
    $out = [];
    foreach ($in as $elements)
    {
        if (fix_win_carriage_return($separator2, $elements))
            break;
    }
    foreach ($in as $elements)
        $out[] = is_null($converter) ? explode($separator2, $elements) : array_map($converter, explode($separator2, $elements));
    return $out;
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
 *          INT ARRAY => [6000, 8000]
 */
function parse_into_sum(string $input_name, string $separator1, string $separator2, callable $converter): array
{
    $map = parse_into_arr_multi_separator($input_name, $separator1, $separator2, $converter);
    $sum_map = [];
    foreach ($map as $k => $v)
        $sum_map[$k] = array_sum($v);
    return $sum_map;
}


/**
 * Example:
 *      IN:
 *          STRING: "1-2-3-4"
 *          SEPARATOR: "-"
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


/**
 * Example:
 *      IN:
 *          SEPARATOR: "\n"
 *          STRING: "Hello\r\n"
 *      OUT:
 *          SEPARATOR: "\r\n"
 */
function fix_win_carriage_return(string &$separator, string $in): bool
{
    if (str_contains($separator, "\n") && str_contains($in, "\r"))
    {
        $separator = str_replace("\n", "\r\n", $separator);
        return true;
    }
    return false;
}
