namespace Code.Day_019;

public partial class Aplenty
{
    private readonly Dictionary<string, Conditions> _map = new();

    public void ProcessMap(string mapping)
    {
        var maps = mapping.Split('\n');
        foreach (var map in maps)
        {
            var key = map.Split('{')[0];
            var conditions = map.Split('{')[1].Replace("}", "");

            _map.Add(key, new Conditions(conditions));
        }
    }

    public long SumAccepted(string ins)
    {
        long answer = 0;
        var xmasIns = ins.Split("\n");

        foreach (var xmas in xmasIns)
        {
            var values = xmas
                .Substring(1, xmas.Length - 2)
                .Split(',')
                .Select(x => long.Parse(x.Split('=').Last())).ToList();

            var operation = _map["in"];
            while (true)
            {
                var val = operation.EvaluateXmas(values[0], values[1], values[2], values[3]);

                if (val == "A")
                {
                    answer += values.Sum();
                    break;
                }

                if (val == "R")
                {
                    break;
                }

                operation = _map[val];
            }
        }

        return answer;
    }
    
    public long Solve_PartA(string inputPath = "Inputs/019.in")
    {
        var lines = string.Join("\n", File.ReadAllLines(inputPath)).Split("\n\n");

        ProcessMap(lines.First());

        var result = SumAccepted(lines.Last());

        return result;
    }
    
    public record Conditions
    {
        private readonly List<Condition> _conditions = new();

        public string EvaluateXmas(long x, long m, long a, long s)
        {
            var defaultKey = _conditions.First(x => x.CmpKey == string.Empty).ReturnKey;

            foreach (var condition in _conditions.Where(x => x.CmpKey != string.Empty))
            {
                string key = "";
                switch (condition.CmpKey)
                {
                    case "x":
                        key = condition.Evaluate(x);
                        if (key != string.Empty) return key;
                        break;
                    case "m":
                        key = condition.Evaluate(m);
                        if (key != string.Empty) return key;
                        break;
                    case "a":
                        key = condition.Evaluate(a);
                        if (key != string.Empty) return key;
                        break;
                    case "s":
                        key = condition.Evaluate(s);
                        if (key != string.Empty) return key;
                        break;
                }
            }

            return defaultKey;
        }

        public Conditions(string input)
        {
            // a<2006:qkq,m>2090:A,rfg
            var conditions = input.Split(',');
            foreach (var condition in conditions)
            {
                var chSplit = condition.Contains('<') ? '<' : condition.Contains('>') ? '>' : ' ';

                if (chSplit == ' ')
                {
                    _conditions.Add(new Condition(string.Empty, null, null, condition));
                    return;
                }

                var x = condition.Split(chSplit);
                _conditions.Add(new Condition(x[0], condition.Contains('<'), long.Parse(x[1].Split(':').First()), x[1].Split(':').Last()));
            }
        }
    }

    public record Condition(string CmpKey, bool? LessThan, long? Value, string ReturnKey)
    {
        public string Evaluate(long cmpValue)
        {
            if (LessThan.Value)
            {
                if (cmpValue < Value.Value)
                {
                    return ReturnKey;
                }

                return string.Empty;
            }

            if (cmpValue > Value.Value)
            {
                return ReturnKey;
            }

            return string.Empty;
        }
    }
}