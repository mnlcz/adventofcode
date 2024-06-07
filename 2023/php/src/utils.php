<?php

use Illuminate\Filesystem\Filesystem;

function input(string $file): string
{
    return (new Filesystem)->get(__DIR__."/../../inputs/$file");
}
