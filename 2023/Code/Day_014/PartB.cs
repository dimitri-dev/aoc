using System.Collections.Concurrent;

namespace Code.Day_014;

public partial class ParabolicReflectorDish
{
    public void RollWest(char[,] map, int size)
    {
        for (int r = 0; r < size; r++)
        {
            for (int c = 1; c < size; c++)
            {
                if (map[r, c] == 'O')
                {
                    int nc = c;
                    while (nc - 1 >= 0 && map[r, nc - 1] == '.') 
                    {
                        map[r, nc - 1] = 'O';
                        map[r, nc] = '.';
                        --nc;
                    }
                }
            }
        }
    }
    
    public void RollEast(char[,] map, int size)
    {
        for (int r = 0; r < size; r++)
        {
            for (int c = size - 1; c >= 0 ; c--)
            {
                if (map[r, c] == 'O')
                {
                    int nc = c;
                    while (nc + 1 < size && map[r, nc + 1] == '.') 
                    {
                        map[r, nc + 1] = 'O';
                        map[r, nc] = '.';
                        ++nc;
                    }
                }
            }
        }
    }
    
    public void RollSouth(char[,] map, int size)
    {
        for (int r = size - 1; r >= 0; r--)
        {
            for (int c = 0; c < size; c++)
            {
                if (map[r, c] == 'O')
                {
                    int nr = r;
                    while (nr + 1 < size && map[nr + 1, c] == '.') 
                    {
                        map[nr + 1, c] = 'O';
                        map[nr, c] = '.';
                        ++nr;
                    }
                }
            }
        }
    }

    public void Cycle(char[,] map, int size)
    {
        RollNorth(map, size);
        RollWest(map, size);
        RollSouth(map, size);
        RollEast(map, size);
    }
    
    public bool IsCycle(List<string> hashes, int a, int b)
    {
        int c = b - a;

        for (int j = 0; j < c; ++j) 
        {
            if (hashes[a + j] != hashes[b + j])
                return false;
        }

        return true;
    }

    public string GetHashCode(char[,] map, int size)
    {
        string s = "";
        
        for (int r = 0; r < size; r++) 
        {
            for (int c = 0; c < size; c++)
            {
                s += map[r, c];
            }
        }

        return s;
    }
    
    public long Solve_PartB(string inputPath = "Inputs/014.in")
    {
        var mapInstance = new MapInstance(File.ReadAllLines(inputPath));

        List<string> hashes = new();
        List<long> loads = new();
        for (int i = 0; i < 500; ++i)
        {
            Cycle(mapInstance.Map, mapInstance.Size);
            hashes.Add(GetHashCode(mapInstance.Map, mapInstance.Size));
            loads.Add(GetTotalLoad(mapInstance.Map, mapInstance.Size));
        }

        int a = 0, b = -1;
        for (a = 0; a < hashes.Count; ++a)
        {
            var flag = false;
            for (b = a + 1; b < hashes.Count; ++b)
            {
                if (hashes[a] == hashes[b] && IsCycle(hashes, a, b))
                {
                    flag = true;
                    break;
                }
            }

            if (flag) break;
        }
        
        int cycleLen = b - a;
        int offset = ((1_000_000_000 - b) % cycleLen) - 1;

        Console.WriteLine($"{b} {a} {offset} {cycleLen}");
        return loads[a + offset];
    }
}