using Code.Day_009;

namespace Tests;

public class Day_009
{
    private MirageMaintenance instance = new();
    
    [Fact]
    public void PartA_Examples()
    {
        Assert.Equal(68, instance.Internal_PartA_GetNextValue("10  13  16  21  30  45"));
        Assert.Equal(28, instance.Internal_PartA_GetNextValue("1   3   6  10  15  21"));
        Assert.Equal(18, instance.Internal_PartA_GetNextValue("0   3   6   9  12  15"));
    }
}