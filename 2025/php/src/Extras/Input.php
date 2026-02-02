<?php

namespace AdventOfCode\Year2025\Extras;

class Input
{
    private function __construct() {}
    public static function get(string $day): string
    {
        $url = "https://adventofcode.com/2025/day/$day/input";
        $session = file_get_contents(dirname(__FILE__) . "/session.txt");
        $ch = curl_init($url);
        curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
        curl_setopt($ch, CURLOPT_COOKIE, "session=$session");
        $response = curl_exec($ch);
        curl_close($ch);

        return $response;
    }
}
