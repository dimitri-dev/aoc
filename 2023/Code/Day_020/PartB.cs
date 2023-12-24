namespace Code.Day_020;

public partial class PulsePropagation
{
    public long Solve_PartB(string inputPath = "Inputs/020.in")
    {
        var lines = File.ReadAllLines(inputPath).ToList();

        var pulses = ProcessInput(lines);

        var result = GetLowestRXPulseCount(pulses, () => ProcessInput(lines));
        
        return result;
    }
    
    public long GetLowestRXPulseCount(List<Pulse> p, Func<List<Pulse>> getNewPulsesInstance)
    {
        // rx has one input and its mg, which is a conjuction pulse
        // when all inputs to the pulse are true, it will send a false signal to rx
        // so we will just multiply the results of obtaining a true signal on every input to the mg pulse which sends it to rx
        var inputs = p.First(a => a.Name == p.First(x => x.Name == "rx").Inputs.First()).Inputs;
        long lcm = 1, prevVal = 1;
        foreach (var importantPulse in inputs)
        {
            var freshPulses = getNewPulsesInstance();

            for (long iter = 1;; ++iter)
            {
                var queue = new Queue<Message>();
                queue.Enqueue(new Message("btn", "broadcaster", false));
            
                while (queue.TryDequeue(out var msg))
                {
                    if (msg.On && msg.Sender == importantPulse)
                    {
                        lcm *= iter;
                        break;
                    }
                
                    var pulse = freshPulses.First(x => x.Name == msg.Receiver);
                    foreach (var newMsg in pulse.ComposeMessages(msg.Sender, msg.On))
                    {
                        queue.Enqueue(newMsg);
                    }
                }

                if (lcm != prevVal)
                {
                    break;
                }
            }

            prevVal = lcm;
        }

        return lcm;
    }
}