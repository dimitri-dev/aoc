using System.Text.RegularExpressions;

namespace Code.Day_005;

public partial class IfYouGiveASeedAFertilizer
{
    private class Mapping()
    {
        public List<Range> Ranges { get; set; } = new();
        public long GetDestination(long value)
        {
            var foundRange = Ranges.FirstOrDefault(range =>
                (value >= range.sourceStartRange) && (value <= range.sourceStartRange + range.offset));

            if (foundRange != null)
            {
                return foundRange.destinationStartRange + (value - foundRange.sourceStartRange);
            }

            return value;
        }    
    }
    
    private record class Range(long sourceStartRange, long destinationStartRange, long offset);
    
    private readonly Mapping _seedToSoil = new();
    private readonly Mapping _soilToFertilizer = new();
    private readonly Mapping _fertilizerToWater = new();
    private readonly Mapping _waterToLight = new();
    private readonly Mapping _lightToTemperature = new();
    private readonly Mapping _temperatureToHumidity = new();
    private readonly Mapping _humidityToLocation = new();
    
    private Mapping GetRangeDirectory(string map) => map switch
    {
        "seed-to-soil" => _seedToSoil,
        "soil-to-fertilizer" => _soilToFertilizer,
        "fertilizer-to-water" => _fertilizerToWater,
        "water-to-light" => _waterToLight,
        "light-to-temperature" => _lightToTemperature,
        "temperature-to-humidity" => _temperatureToHumidity,
        "humidity-to-location" => _humidityToLocation,
        _ => null
    };
    
    public long Internal_PartA_GetLocation(long seedNumber)
    {
        var soil = _seedToSoil.GetDestination(seedNumber);
        var fertilizer = _soilToFertilizer.GetDestination(soil);
        var water = _fertilizerToWater.GetDestination(fertilizer);
        var light = _waterToLight.GetDestination(water);
        var temperature = _lightToTemperature.GetDestination(light);
        var humidity = _temperatureToHumidity.GetDestination(temperature);
        var location = _humidityToLocation.GetDestination(humidity);

        return location;
    }
    
    public void Internal_PartA_SetupMap(IEnumerable<string> mapping)
    {
        Mapping refMap = null;
        
        foreach (var map in mapping)
        {
            if (string.IsNullOrEmpty(map)) continue;
            
            var rangeDirectory = GetRangeDirectory(map.Split(' ').First());
            if (rangeDirectory != null)
            {
                refMap = rangeDirectory;
                continue;
            }

            var numbers = map.Split(' ').Select(x => Int64.Parse(x.Trim())).ToArray();
            
            var destinationRange = numbers[0];
            var sourceRange = numbers[1];
            var rangeLength = numbers[2];

            refMap.Ranges.Add(new(sourceRange, destinationRange, rangeLength));
        }
    }
    
    public long Solve_PartA(string inputPath = "Inputs/005.in")
    {
        var lines = File.ReadAllLines(inputPath);
        
        Internal_PartA_SetupMap(lines.Skip(1));

        var seedNumbers = lines.First()
            .Split(':')[1]
            .Split(' ')
            .Where(x => !string.IsNullOrEmpty(x));

        var minLocation = seedNumbers.Min(x => Internal_PartA_GetLocation(Int64.Parse(x.Trim())));
        
        return minLocation;
    }
}