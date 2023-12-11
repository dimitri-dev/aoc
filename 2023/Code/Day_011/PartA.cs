using System.Collections;

namespace Code.Day_011;

public partial class CosmicExpansion
{
    private List<string> _map;
    
    private long GetOldGalaxies(int expansionRate)
    {
        var emptyRows = _map.Select((row, idx) => (row, idx))
            .Where(enumerated => enumerated.row.All(ch => ch == '.'))
            .Select(x => x.idx)
            .ToList();
        
        var emptyCols = _map.Select((row, idx) => (row, idx))
            .Where(enumerated => _map.All(row => row[enumerated.idx] == '.'))
            .Select(x => x.idx)
            .ToList();

        var galaxies = _map.SelectMany((row, x) =>
        {
            return row.Select((ch, y) => (ch, y))
                .Where(enumeratedRow => enumeratedRow.ch == '#')
                .Select(enumeratedRow => new Point(x, enumeratedRow.y));
        }).ToList();

        long sum = galaxies.SelectMany((left, idx) =>
        {
            return galaxies.Skip(idx + 1).Select(right =>
            {
                long distanceSum = Math.Abs(left.x - right.x) + Math.Abs(left.y - right.y);

                long extraRows = (expansionRate - 1) *
                                 emptyRows.Count(rowIdx =>
                                     (left.x < rowIdx && right.x > rowIdx) || (left.x > rowIdx && right.x < rowIdx));

                long extraColumns = (expansionRate - 1) *
                                    emptyCols.Count(colIdx =>
                                        (left.y < colIdx && right.y > colIdx) || (left.y > colIdx && right.y < colIdx));

                return distanceSum + extraRows + extraColumns;
            });
        }).Sum();

        return sum;
    }
    
    public long Solve_PartA(string inputPath = "Inputs/011.in")
    {
        _map = File.ReadAllLines(inputPath).ToList();
        
        return GetOldGalaxies(2);
    }

    private record Point(int x, int y);
}