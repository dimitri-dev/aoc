use std::cmp::Ordering;
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
        .filter(|x| is_ordered(x, &reversed_page_ordering_rules))
        .map(|x| x.get((x.iter().count() - 1) / 2).unwrap())
        .sum::<i32>();

    let sum2 = updates.iter_mut()
        .filter(|x| !is_ordered(&&**x, &reversed_page_ordering_rules))
        .map(|mut x| {
            x.sort_by(|a, b| {
                if let Some(ruleA) = page_ordering_rules.get(a) {
                    if ruleA.contains(b) {
                        return Ordering::Less;
                    }
                }

                if let Some(ruleB) = page_ordering_rules.get(b) {
                    if ruleB.contains(a) {
                        return Ordering::Greater;
                    }
                }

                Ordering::Equal
            });

            x
        })
        .map(|x| x.get((x.iter().count() - 1) / 2).unwrap())
        .sum::<i32>();

    (sum.to_string(), sum2.to_string())
}

pub fn is_ordered(x: &&Vec<i32>, reversed_page_ordering_rules: &HashMap<i32, HashSet<i32>>) -> bool {
    let mut block: HashSet<i32> = HashSet::new();

    for num in *x {
        if block.contains(&num) {
            return false
        }

        if reversed_page_ordering_rules.contains_key(num) {
            let future = reversed_page_ordering_rules.get(num).unwrap();
            block = block.union(future).cloned().collect();
        }
    }

    true
}