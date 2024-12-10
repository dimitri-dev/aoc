use std::collections::{HashMap, HashSet};
use std::fs::File;
use std::io;
use std::io::BufRead;

pub fn result(file_path: &str) -> (String, String) {
    let file = File::open(file_path);

    if file.is_err() {
        eprintln!("Error: File not found");
        return ("".to_string(), "".to_string());
    }

    let mut flag = false;
    let mut page_ordering_rules: HashMap<i32, HashSet<i32>> = HashMap::new();
    let mut reversed_page_ordering_rules: HashMap<i32, HashSet<i32>> = HashMap::new();
    let mut updates: Vec<Vec<i32>> = Vec::new();

    let reader = io::BufReader::new(file.unwrap());
    for line in reader.lines() {
        let line = line.unwrap();

        if line.is_empty()
        {
            flag = true;
            continue;
        }

        if flag {
            updates.push(line.split(',').filter_map(|x| x.parse::<i32>().ok()).collect());
            continue;
        }

        let nums: Vec<i32> = line.split('|').filter_map(|x| x.parse::<i32>().ok()).collect();
        page_ordering_rules.entry(nums[0]).or_insert(HashSet::new()).insert(nums[1]);
        reversed_page_ordering_rules.entry(nums[1]).or_insert(HashSet::new()).insert(nums[0]);
    }

    let sum = updates.iter()
        .filter(|x| {
            let mut block: HashSet<i32> = HashSet::new();

            for num in *x {
                if block.contains(&num) {
                    return false
                }

                let future =reversed_page_ordering_rules.entry(*num).or_default();
                if !future.is_empty() {
                    block = block.union(&future).cloned().collect();
                }
            }

            true
        })
        .map(|x| x.get((x.iter().count() - 1) / 2).unwrap())
        .sum::<i32>();

    (sum.to_string(), "".to_string())
}
