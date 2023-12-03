using Code.Day_001;

namespace Tests;

public class Day_001
{
    private Trebuchet instance = new();
    
    [Fact]
    public void PartA_Examples()
    {
        Assert.Equal(12, instance.Internal_PartA_GetCalibrationValue("1abc2"));
        Assert.Equal(38, instance.Internal_PartA_GetCalibrationValue("pqr3stu8vwx"));
        Assert.Equal(15, instance.Internal_PartA_GetCalibrationValue("a1b2c3d4e5f"));
        Assert.Equal(77, instance.Internal_PartA_GetCalibrationValue("treb7uchet"));
    }

    [Fact]
    public void PartB_Examples()
    {
        Assert.Equal(29, instance.Internal_PartB_GetCalibrationValue("two1nine"));
        Assert.Equal(83, instance.Internal_PartB_GetCalibrationValue("eightwothree"));
        Assert.Equal(13, instance.Internal_PartB_GetCalibrationValue("abcone2threexyz"));
        Assert.Equal(24, instance.Internal_PartB_GetCalibrationValue("xtwone3four"));
        Assert.Equal(42, instance.Internal_PartB_GetCalibrationValue("4nineeightseven2"));
        Assert.Equal(14, instance.Internal_PartB_GetCalibrationValue("zoneight234"));
        Assert.Equal(76, instance.Internal_PartB_GetCalibrationValue("7pqrstsixteen"));
    }
}