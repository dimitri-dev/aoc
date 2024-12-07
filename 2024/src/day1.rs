use std::collections::HashMap;
use std::fs::File;
use std::io;
use std::io::BufRead;
use std::iter::zip;

pub fn result(file_path: &str) {
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
    let mut similarity_map: HashMap<i64, i64> = HashMap::new();

    zip(vec1.iter(), vec2.iter())
        .for_each(|(a, b)| {
            p1 += (a - b).abs();
            similarity_map.entry(*a).or_insert(a * vec1.iter().filter(|&x| *x == *a).count() as i64 * vec2.iter().filter(|&x| *x == *a).count() as i64);
        });

    println!("Day 1 Part 1: {}", p1);
    println!("Day 1 Part 2: {}", similarity_map.values().sum::<i64>());
}