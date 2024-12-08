use std::fs::File;
use std::io;
use std::io::BufRead;

pub fn result(file_path: &str) -> (String, String) {
    let file = File::open(file_path);

    if file.is_err() {
        eprintln!("Error: File not found");
        return ("".to_string(), "".to_string());
    }

    let reader = io::BufReader::new(file.unwrap());

    let mut safe_reports = 0;

    for line in reader.lines() {
        let line = line.unwrap();
        let nums: Vec<i64> = line.split_whitespace()
            .filter_map(|x| x.parse::<i64>().ok())
            .collect();

        safe_reports += if is_safe(&nums) {1} else {0};
    }

    (safe_reports.to_string(), "".to_string())
}

pub fn is_safe(nums: &Vec<i64>) -> bool {
    let mut increase: Option<bool> = None;

    for i in 0..nums.len() - 1 {
        let diff = nums[i + 1] - nums[i];
        let diff_abs = diff.abs();

        if diff_abs > 3 || diff_abs == 0 {
            return false;
        }

        if diff > 0 {
            increase.get_or_insert(true);

            if !increase.unwrap() {
                return false;
            }
        }

        if diff < 0 {
            increase.get_or_insert(false);

            if increase.unwrap() {
                return false;
            }
        }
    }

    true
}