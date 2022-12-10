<?php

require "../../utils/php/parsers.php";

$in = parse_into_arr("8", "\n", NULL);
const HORIZONTAL = 1;
const VERTICAL = 0;
$height = count($in);
$length = strlen($in[0]);
part1($in);
part2($in);

function part1(array $in): void
{
    global $length, $height;
    $count = 0;
    for ($i = 0; $i < $height; $i++)
        for ($j = 0; $j < $length; $j++)
            if (is_visible($i, $j, $in))
                $count++;
    echo "Part 1: $count\n";
}

function part2(array $in): void
{
    global $length, $height;
    $max = -1;
    for ($i = 0; $i < $height; $i++)
        for ($j = 0; $j < $length; $j++)
        {
            $score = get_scenic_score($i, $j, $in);
            if ($score > $max)
                $max = $score;
        }
    echo "Part 2: $max";
}

function is_visible(int $line_i, int $tree_i, array $map): bool
{
    return is_edge($line_i, $tree_i) ? TRUE : check(VERTICAL, $line_i, $tree_i, $map) || check(HORIZONTAL, $line_i, $tree_i, $map);
}

function check(int $type, int $line_i, int $tree_i, array $map): bool
{
    global $height, $length;
    $tree = intval($map[$line_i][$tree_i]);
    $iterator = $type === 1 ? 'iterate_hor' : 'iterate_ver';
    $limit = $type === 1 ? $length : $height;
    [$fixed_i, $variable_i] = $type === 1 ? [$line_i, $tree_i] : [$tree_i, $line_i];
    if (call_user_func($iterator, 0, $variable_i, $fixed_i, $tree, $map))
        return TRUE;
    return call_user_func($iterator, $variable_i + 1, $limit, $fixed_i, $tree, $map);
}

function iterate_hor(int $start, int $end, int $line_i, int $tree, array $map): bool
{
    for ($i = $start; $i < $end; $i++) 
        if (intval($map[$line_i][$i]) >= $tree)
            return FALSE;
    return TRUE;
}

function iterate_ver(int $start, int $end, int $tree_i, int $tree, array $map): bool
{
    for ($i = $start; $i < $end; $i++) 
        if (intval($map[$i][$tree_i]) >= $tree)
            return FALSE;
    return TRUE;
}

function is_edge(int $line_i, int $tree_i): bool
{
    global $height, $length;
    return $line_i === 0 || $line_i === $height - 1 || $tree_i === 0 || $tree_i === $length - 1; 
}

function get_scenic_score(int $line_i, int $tree_i, array $map): int
{
    $params = [$line_i, $tree_i, $map];
    return north_view(...$params) * south_view(...$params) * west_view(...$params) * east_view(...$params);
}

function north_view(int $line_i, int $tree_i, array $map): int
{
    if ($line_i === 0)
        return 0;
    $trees = 1;
    for ($i = $line_i - 1; $i > 0; $i--)
    {
        if (intval($map[$i][$tree_i]) >= intval($map[$line_i][$tree_i]))
            break;
        $trees++;
    }
    return $trees;
}

function south_view(int $line_i, int $tree_i, array $map): int
{
    global $height;
    if ($line_i === $height - 1)
        return 0;
    $trees = 1;
    for ($i = $line_i + 1; $i < $height - 1; $i++)
    {
        if (intval($map[$i][$tree_i]) >= intval($map[$line_i][$tree_i]))
            break;
        $trees++;
    }
    return $trees;
}

function west_view(int $line_i, int $tree_i, array $map): int
{
    if ($tree_i === 0)
        return 0;
    $trees = 1;
    for ($i = $tree_i - 1; $i > 0; $i--)
    {
        if (intval($map[$line_i][$i]) >= intval($map[$line_i][$tree_i]))
            break;
        $trees++;
    }
    return $trees;
}

function east_view(int $line_i, int $tree_i, array $map): int
{
    global $length;
    if ($tree_i === $length - 1)
        return 0;
    $trees = 1;
    for ($i = $tree_i + 1; $i < $length - 1; $i++)
    {
        if (intval($map[$line_i][$i]) >= intval($map[$line_i][$tree_i]))
            break;
        $trees++;
    }
    return $trees; 
}
