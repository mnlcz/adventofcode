<?php

require "../utils/parsers.php";

$in = parse_into_arr("7", "\n", NULL);
part1($in);
part2($in);

function part1(array $in): void
{
    $sum = get_sum(build_map($in));
    echo "Part 1: $sum\n";
}

function part2(array $in): void
{
    $map = build_map($in);
    $used = $map[""];
    $out = min(array_filter(array_values($map), fn($size) => 70_000_000 - ($used - $size) >= 30_000_000));
    echo "Part 2: $out";
}

function build_map(array $in): array
{
    $map = ["" => 0];
    $matches = [];
    $cwd = "";
    foreach ($in as $line)
        if (preg_match('/[$] cd (.*)|(\d+)/', $line, $matches))
            is_numeric($matches[0]) ? handle_file($map, intval($matches[0]), $cwd) : handle_cd($cwd, $matches[1]);
    return $map;
}

function handle_file(array &$map, int $size, string $cwd): void
{
    while (TRUE)
    {
        $map[$cwd] = array_key_exists($cwd, $map) ? $map[$cwd] + $size : $size;
        if (empty($cwd)) break;
        $cwd = substr($cwd, 0, strrpos($cwd, "/"));
    }
}

function handle_cd(string &$current, string $destination): void
{
    if ($destination === "/")
        $current = "";
    elseif ($destination === "..")
        $current = substr($current, 0, strrpos($current, "/"));
    else
        $current = empty($current) ? $destination : "$current/$destination";
}

function get_sum(array $map): int
{
    $sum = 0;
    foreach (array_values($map) as $size)
        if ($size <= 100000)
            $sum += $size;
    return $sum;
}
