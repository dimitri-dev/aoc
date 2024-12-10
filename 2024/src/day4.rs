use std::fs::File;
use std::io;
use std::io::BufRead;

pub fn result(file_path: &str) -> (String, String) {
    let file = File::open(file_path);

    if file.is_err() {
        eprintln!("Error: File not found");
        return ("".to_string(), "".to_string());
    }

    let mut board: Vec<Vec<char>> = Vec::new();

    let reader = io::BufReader::new(file.unwrap());
    for line in reader.lines() {
        let line = line.unwrap();

        board.push(line.chars().collect());
    }

    let mut xmas_count = 0;
    let mut mas_as_x_count = 0;

    for i in 0..board.len() {
        for j in 0..board[i].len() {
            if board[i][j] == 'X' {
                xmas_count += find_xmas(&board, (i, j));
                continue
            }

            if board[i][j] != 'A' || !(i > 0 && i + 1 < board.len() && j > 0 && j + 1 < board[i].len()) {
                continue
            }

            let diag1 = board[i - 1][j - 1] == 'M' && board[i + 1][j + 1] == 'S';
            let diag1_reverse = board[i - 1][j - 1] == 'S' && board[i + 1][j + 1] == 'M';
            let diag2 = board[i - 1][j + 1] == 'M' && board[i + 1][j - 1] == 'S';
            let diag2_reverse = board[i - 1][j + 1] == 'S' && board[i + 1][j - 1] == 'M';

            mas_as_x_count += if (diag1 || diag1_reverse) && (diag2 || diag2_reverse) {1} else {0};
        }
    }

    (xmas_count.to_string(), mas_as_x_count.to_string())
}

pub fn find_xmas(board: &Vec<Vec<char>>, (i, j): (usize, usize)) -> i32 {
    let mut count = 0;
    let rows = board.len();
    let cols = board[0].len();

    // up
    if i as i32 - 3 >= 0 && board[i - 1][j] == 'M' && board[i - 2][j] == 'A' && board[i - 3][j] == 'S' {
        count += 1;
    }

    // down
    if i + 3 < rows && board[i + 1][j] == 'M' && board[i + 2][j] == 'A' && board[i + 3][j] == 'S' {
        count += 1;
    }

    // left
    if j as i32 - 3 >= 0 && board[i][j - 1] == 'M' && board[i][j - 2] == 'A' && board[i][j - 3] == 'S' {
        count += 1;
    }

    // right
    if j + 3 < cols && board[i][j + 1] == 'M' && board[i][j + 2] == 'A' && board[i][j + 3] == 'S' {
        count += 1;
    }

    // up-left
    if i as i32 - 3 >= 0 && j as i32 - 3 >= 0 && board[i - 1][j - 1] == 'M' && board[i - 2][j - 2] == 'A' && board[i - 3][j - 3] == 'S' {
        count += 1;
    }

    // up-right
    if i as i32 - 3 >= 0 && j + 3 < cols && board[i - 1][j + 1] == 'M' && board[i - 2][j + 2] == 'A' && board[i - 3][j + 3] == 'S' {
        count += 1;
    }

    // down-left
    if i + 3 < rows && j as i32 - 3 >= 0 && board[i + 1][j - 1] == 'M' && board[i + 2][j - 2] == 'A' && board[i + 3][j - 3] == 'S' {
        count += 1;
    }

    // down-right
    if i + 3 < rows && j + 3 < cols && board[i + 1][j + 1] == 'M' && board[i + 2][j + 2] == 'A' && board[i + 3][j + 3] == 'S' {
        count += 1;
    }

    count
}