namespace Code.Day_014;

public partial class ParabolicReflectorDish
{
    public long GetTotalLoad(char[,] map, int size)
    {
        long load = 0;
        for (int r = 0; r < size; r++) 
        {
            for (int c = 0; c < size; c++) 
            {
                if (map[r, c] == 'O')
                    load += size - r;
            }
        }

        return load;
    }

    public void RollNorth(char[,] map, int size)
    {
        for (int r = 1; r < size; r++)
        {
            for (int c = 0; c < size; c++)
            {
                if (map[r, c] == 'O')
                {
                    int nr = r;
                    while (nr - 1 >= 0 && map[nr - 1, c] == '.') 
                    {
                        map[nr - 1, c] = 'O';
                        map[nr, c] = '.';
                        --nr;
                    }
                }
            }
        }
    }
    
    public long Solve_PartA(string inputPath = "Inputs/014.in")
    {
        var lines = File.ReadAllLines(inputPath);
        int size = lines.Length;
        
        char[,] map = new char[lines.Length, lines.Length];
        for (int r = 0; r < lines.Length; r++) 
        {
            for (int c = 0; c < lines.Length; c++) 
            {
                map[r, c] = lines[r][c];
            }
        }
        
        RollNorth(map, lines.Length);
        var result = GetTotalLoad(map, size);
        
        return result;
    }
}