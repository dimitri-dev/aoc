namespace Code.Day_007;

public partial class CamelCards
{
    public int Internal_PartB_GetHandType(string hand)
    {
        var cardCount = hand.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());
        
        cardCount.TryGetValue('J', out var jackCount);
        
        switch (cardCount.Count)
        {
            case 1:
                return 7; // no special cases
            case 2 when cardCount.First().Value is 1 or 4:
                return jackCount switch
                {
                    1 or 4 => 7, // 4 + J / 4J + 1
                    _ => 6 // regular 4 + 1
                };
            case 2:
                return jackCount switch
                {
                    2 or 3 => 7, // 3J + 2 or 3 + 2J
                    _ => 5 // regular 3 + 2
                };
            case 3 when cardCount.Any(x => x.Value == 3): // 3 + 1 + 1
                return jackCount switch
                {
                    1 or 3 => 6, // 3J + 1 + 1 or 3 + J + 1
                    _ => 4 // regular
                };
            case 3:
                return jackCount switch
                {
                    2 => 6, // 2J + 2 + 1
                    1 => 4, // J + 2 + 2
                    _ => 3 // regular
                };
            case 4: // 2 + 1 + 1 + 1
                return jackCount switch
                {
                    2 => 4, // 2J + 1 + 1 + 1 or 2 + J + 1 + 1
                    1 => 3,
                    _ => 2 // regular
                };
            case 5: // 1 + 1 + 1 + 1 + 1
                return jackCount switch
                {
                    1 => 2, // J + 1 + 1 + 1 + 1
                    _ => 1 // regular
                };
            default:
                return 0;
        }
    }

    public int Internal_PartB_GetCardValue(char card)
    {
        return card switch
        {
            'J' => 1,
            '2' => 2,
            '3' => 3,
            '4' => 4,
            '5' => 5,
            '6' => 6,
            '7' => 7,
            '8' => 8,
            '9' => 9,
            'T' => 10,
            'Q' => 12,
            'K' => 13,
            'A' => 14
        };
    }

    public long Internal_PartB_GetHandValue(string hand)
    {
        return Internal_PartB_GetCardValue(hand[0]) * 100000000 +
               Internal_PartB_GetCardValue(hand[1]) * 1000000 +
               Internal_PartB_GetCardValue(hand[2]) * 10000 +
               Internal_PartB_GetCardValue(hand[3]) * 100 +
               Internal_PartB_GetCardValue(hand[4]);
    }

    public long Solve_PartB(string inputPath = "Inputs/007.in")
    {
        var hands = File.ReadAllLines(inputPath);

        var maxRank = hands.Length;

        var types = hands
            .GroupBy(x => Internal_PartB_GetHandType(x.Split(' ').First()))
            .ToDictionary(
                x => x.Key,
                x => x.Select(x => (
                    handValue: Internal_PartB_GetHandValue(x),
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