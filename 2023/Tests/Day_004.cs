using Code.Day_004;

namespace Tests;

public class Day_004
{
    private Scratchcards instance = new();
    
    [Fact]
    public void PartA_Examples()
    {
        Assert.Equal(8, instance.Internal_PartA_GetValue("Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53"));
        Assert.Equal(2, instance.Internal_PartA_GetValue("Card 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19"));
        Assert.Equal(2, instance.Internal_PartA_GetValue("Card 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1"));
        Assert.Equal(1, instance.Internal_PartA_GetValue("Card 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83"));
        Assert.Equal(0, instance.Internal_PartA_GetValue("Card 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36"));
        Assert.Equal(0, instance.Internal_PartA_GetValue("Card 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11"));
    }
}