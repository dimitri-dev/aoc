use std::fs::File;
use std::io;
use std::io::BufRead;
use regex::Regex;

pub fn result(file_path: &str) -> (String, String) {
    let file = File::open(file_path);

    if file.is_err() {
        eprintln!("Error: File not found");
        return ("".to_string(), "".to_string());
    }

    let reader = io::BufReader::new(file.unwrap());
    let mul_regex = Regex::new(r"mul\((?<a>[0-9]{1,3}),(?<b>[0-9]{1,3})\)").unwrap();
    let mut sum: i32 = 0;

    for line in reader.lines() {
        sum += mul_regex.captures_iter(&line.unwrap())
            .map(|x| x.name("a").unwrap().as_str().parse::<i32>().unwrap() * x.name("b").unwrap().as_str().parse::<i32>().unwrap())
            .sum::<i32>();
    }

    (sum.to_string(), "".to_string())
}