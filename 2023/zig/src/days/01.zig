const std = @import("std");
const ascii = std.ascii;

const sample =
    \\1abc2
    \\pqr3stu8vwx
    \\a1b2c3d4e5f
    \\treb7uchet
;

pub fn part1(input: []const u8) !i32 {
    var arena = std.heap.ArenaAllocator.init(std.heap.page_allocator);
    defer arena.deinit();
    const allocator = arena.allocator();

    var lines = std.mem.split(u8, input, "\n");
    var total: i32 = 0;

    while (lines.next()) |l| {
        const val = try calibrationValue(allocator, l);
        total += try std.fmt.parseInt(i32, val, 10);
    }

    return total;
}

fn calibrationValue(allocator: std.mem.Allocator, str: []const u8) ![]const u8 {
    var digits = try allocator.alloc(u8, 2);
    var first: bool = false;
    var sec: bool = false;

    for (str) |ch| {
        if (ascii.isDigit(ch)) {
            if (!first) {
                first = true;
                digits[0] = ch;
            } else {
                sec = true;
                digits[1] = ch;
            }
        }
    }

    if (!sec) {
        digits[1] = digits[0];
    }

    return digits[0..];
}

test "part 1 calibration value with samples" {
    const ally = std.testing.allocator;
    const exp = [_][]const u8{ "12", "38", "15", "77" };
    var act: [4][]const u8 = std.mem.zeroes([4][]const u8);
    var lines = std.mem.splitSequence(u8, sample, "\n");

    var i: usize = 0;
    while (lines.next()) |line| : (i += 1) {
        act[i] = try calibrationValue(ally, line);
    }

    for (act, 0..) |str, index| {
        try std.testing.expectEqualStrings(exp[index], str);
        ally.free(str);
    }
}

test "part 1 with sample" {
    try std.testing.expectEqual(142, part1(sample));
}
