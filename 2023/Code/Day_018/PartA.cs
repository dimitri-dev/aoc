namespace Code.Day_018;

public partial class LavaductLagoon
{
    public long Calculate_PartA(List<DigInstruction> instructions)
    {
        double answer = 1;
        long horizontal = 0;
        
        foreach (var ins in instructions)
        {
            horizontal += ins.MoveCount * ins.Direction.y;
            var vertical = ins.MoveCount * ins.Direction.x;
            answer += vertical * horizontal + (ins.MoveCount / 2.0);
        }

        return (long)answer;
    }
    
    public long Solve_PartA(string inputPath = "Inputs/018.in")
    {
        var lines = File.ReadAllLines(inputPath);

        var instructions = lines.Select(x => new DigInstruction(x)).ToList();

        var result = Calculate_PartA(instructions);
        
        return result;
    }

    public record Direction(int x, int y)
    {
        public static Direction FromChar(char ch) => ch switch
        {
            'U' => North,
            'L' => West,
            'D' => South,
            'R' => East,
            _ => null
        } ?? throw new InvalidOperationException();
        
        public static Direction North = new(-1, 0);
        public static Direction South = new(1, 0);
        public static Direction East = new(0, 1);
        public static Direction West = new(0, -1);
    }

    public record DigInstruction(Direction Direction, int MoveCount, string HexColor)
    {
        public DigInstruction(string input) : this(null, 0, null)
        {
            var inputs = input.Split(' ');
            this.Direction = Direction.FromChar(inputs[0][0]);
            this.MoveCount = Int32.Parse(inputs[1]);
            this.HexColor = inputs[2].Substring(1, inputs[2].Length - 2);
        }
    }
}