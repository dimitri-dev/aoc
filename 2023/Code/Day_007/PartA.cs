namespace Code.Day_007;

public partial class CamelCards
{
    public int Internal_PartA_GetHandType(string hand)
    {
        var cardCount = hand.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());
        switch (cardCount.Count)
        {
            case 1:
                return 7;
            case 2 when cardCount.First().Value is 1 or 4:
                return 6;
            case 2:
                return 5;
            case 3 when cardCount.Any(x => x.Value == 3):
                return 4;
            case 3:
                return 3;
            case 4:
                return 2;
            case 5:
                return 1;
            default:
                return 0;
        }
    }

    public int Internal_PartA_GetCardValue(char card)
    {
        return card switch
        {
            '2' => 2,
            '3' => 3,
            '4' => 4,
            '5' => 5,
            '6' => 6,
            '7' => 7,
            '8' => 8,
            '9' => 9,
            'T' => 10,
            'J' => 11,
            'Q' => 12,
            'K' => 13,
            'A' => 14
        };
    }

    public long Internal_PartA_GetHandValue(string hand)
    {
        return Internal_PartA_GetCardValue(hand[0]) * 100000000 +
               Internal_PartA_GetCardValue(hand[1]) * 1000000 +
               Internal_PartA_GetCardValue(hand[2]) * 10000 +
               Internal_PartA_GetCardValue(hand[3]) * 100 +
               Internal_PartA_GetCardValue(hand[4]);
    }

    public long Solve_PartA(string inputPath = "Inputs/007.in")
    {
        var hands = File.ReadAllLines(inputPath);

        var maxRank = hands.Length;

        var types = hands
            .GroupBy(x => Internal_PartA_GetHandType(x.Split(' ').First()))
            .ToDictionary(
                x => x.Key,
                x => x.Select(x => (
                    originalValue: x,
                    handValue: Internal_PartA_GetHandValue(x),
                    bidValue: long.Parse(x.Split(' ').Last()))
                ).OrderByDescending(x => x.handValue))
            .OrderByDescending(x => x.Key);

        long result = 0;

        foreach (var handType in types)
        {
            foreach (var hand in handType.Value)
            {
                result += hand.bidValue * maxRank--;
            }
        }

        return result;
    }
}