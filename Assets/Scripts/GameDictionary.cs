using System.Collections.Generic;

public static class GameDictionary
{
    public static Dictionary<ElementType, string> ElementTypeNames = new Dictionary<ElementType, string>
    {
        { ElementType.Air, "Воздух" },
        { ElementType.Earth, "Земля" },
        { ElementType.Fire, "Огонь" },
        { ElementType.Water, "Вода" }
    };

    public static Dictionary<TargetType, string> TargetTypeNames = new Dictionary<TargetType, string>
    {
        { TargetType.Self, "На себя" },
        { TargetType.Enemy, "Противник" }
    };
}
