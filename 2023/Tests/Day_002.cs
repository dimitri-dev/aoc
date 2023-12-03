using Code.Day_002;

namespace Tests;

public class Day_002
{
    private CubeConundrum instance = new();
    
    [Fact]
    public void PartA_Examples()
    {
        Assert.Equal(1, instance.Internal_PartA_GetValue("Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green"));
        Assert.Equal(2, instance.Internal_PartA_GetValue("Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue"));
        Assert.Equal(0, instance.Internal_PartA_GetValue("Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red"));
        Assert.Equal(0, instance.Internal_PartA_GetValue("Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red"));
        Assert.Equal(5, instance.Internal_PartA_GetValue("Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green"));
    }

    [Fact]
    public void PartB_Examples()
    {
        Assert.Equal(48, instance.Internal_PartB_GetValue("Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green"));
        Assert.Equal(12, instance.Internal_PartB_GetValue("Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue"));
        Assert.Equal(1560, instance.Internal_PartB_GetValue("Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red"));
        Assert.Equal(630, instance.Internal_PartB_GetValue("Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red"));
        Assert.Equal(36, instance.Internal_PartB_GetValue("Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green"));
    }
}