use std::fs::File;
use std::io;
use std::io::BufRead;

pub(crate) fn result(file_path: &str) {
    let mut vec1: Vec<i64> = Vec::new();
    let mut vec2: Vec<i64> = Vec::new();

    let file = File::open(file_path);

    if file.is_err() {
        eprintln!("Error: File not found");
        return;
    }

    let reader = io::BufReader::new(file.unwrap());

    for line in reader.lines() {
        let line = line.unwrap();
        let nums: Vec<i64> = line.split_whitespace()
            .filter_map(|x| x.parse::<i64>().ok())
            .collect();

        if nums.len() != 2 {
            eprintln!("Error: Line wasn't parsed properly. Value: {}", line);
            continue;
        }

        vec1.push(nums[0]);
        vec2.push(nums[1]);
    }

    vec1.sort();
    vec2.sort();

    let mut p1: i64 = 0;

    for i in 0..vec1.len() {
        p1 += (vec1[i] - vec2[i]).abs();
    }

    println!("Day 1 Part 1: {}", p1);
}