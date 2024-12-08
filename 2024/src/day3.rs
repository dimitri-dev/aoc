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
    let conditional_mul_regex = Regex::new(r"mul\((?<a>[0-9]{1,3}),(?<b>[0-9]{1,3})\)|do\(\)|don't\(\)").unwrap();
    let mut sum: i32 = 0;

    let mut conditional_sum: i32 = 0;
    let mut condition = true;

    for line in reader.lines() {
        let line = line.unwrap();
        sum += mul_regex.captures(&line).iter()
            .map(|x| x.name("a").unwrap().as_str().parse::<i32>().unwrap() * x.name("b").unwrap().as_str().parse::<i32>().unwrap())
            .sum::<i32>();

        conditional_mul_regex.captures_iter(&line).for_each(|capture| {
            if capture.get(0).unwrap().as_str() == "do()" {
                condition = true;
                return;
            }

            if capture.get(0).unwrap().as_str() == "don't()" {
                condition = false;
                return;
            }

            if condition {
                if let (Some(a), Some(b)) = (capture.name("a"), capture.name("b")) {
                    conditional_sum += a.as_str().parse::<i32>().unwrap() * b.as_str().parse::<i32>().unwrap();
                }
            }
        });
    }

    (sum.to_string(), conditional_sum.to_string())
}