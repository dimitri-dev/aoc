using System.Collections.Concurrent;

namespace Code.Day_020;

public partial class PulsePropagation
{
    public List<Pulse> ProcessInput(List<string> input)
    {
        List<Pulse> ret = new();
        
        foreach (var line in input)
        {
            var ls = line.Split(' ');
            string name = ls[0];
            ModuleType type = ModuleType.Broadcaster;
            
            if (name.Contains("%"))
            {
                name = name.Replace("%", "");
                type = ModuleType.FlipFlop;
            }
            
            if (name.Contains("&"))
            {
                name = name.Replace("&", "");
                type = ModuleType.Conjuction;
            }

            List<string> exits = new();
            for (int i = 2; i < ls.Length; ++i)
            {
                exits.Add(ls[i].Replace(",", ""));
            }
            
            ret.Add(new (name, new(), exits, type));
        }

        foreach (var pulse in ret)
        {
            pulse.Inputs.AddRange(ret.Where(x => x.Exits.Contains(pulse.Name)).Select(x => x.Name));
            pulse.InitMemory();
        }

        return ret;
    }
    
    public long Solve_PartA(string inputPath = "Inputs/020.in")
    {
        var lines = File.ReadAllLines(inputPath).ToList();

        var pulses = ProcessInput(lines);

        var result = MultiplyHighAndLowInputs(pulses, 1000);
        
        return result;
    }

    public long MultiplyHighAndLowInputs(List<Pulse> pulses, int repetitionCount)
    {
        long on = 0, off = 0;
        
        for (int i = 0; i < repetitionCount; ++i)
        {
            var queue = new Queue<Message>();
            queue.Enqueue(new Message("btn", "broadcaster", false));

            while (queue.TryDequeue(out var msg))
            {
                if (msg.On) ++on;
                else ++off;
                
                var pulse = pulses.First(x => x.Name == msg.Receiver);
                foreach (var newMsg in pulse.ComposeMessages(msg.Sender, msg.On))
                {
                    queue.Enqueue(newMsg);
                }
            }
        }

        return on * off;
    }

    public record Pulse(string Name, List<string> Inputs, List<string> Exits, ModuleType type)
    {
        private bool _pulseState = false;
        private Dictionary<string, bool> _memory = new();

        public void InitMemory()
        {
            _memory = Inputs.ToDictionary(x => x, _ => false);
        }
        
        public List<Message> ComposeMessages(string sender, bool state)
        {
            switch (type)
            {
                case ModuleType.FlipFlop:
                    if (state)
                    {
                        return new();
                    }
                    _pulseState = !_pulseState;
                    break;
                case ModuleType.Conjuction:
                    _memory[sender] = state;
                    _pulseState = !(_memory.Values.All(x => x));
                    break;
                case ModuleType.Broadcaster:
                    _pulseState = state;
                    break;
            }
            
            return Exits.Select(exit => new Message(Name, exit, _pulseState)).ToList();
        }
    }

    public enum ModuleType
    {  
        FlipFlop,
        Conjuction,
        Broadcaster
    }

    public record Message(string Sender, string Receiver, bool On);
}