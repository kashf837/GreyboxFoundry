using System;

public static class ItemStats
{
    public static event Action Changed;
    public static int OreCount = 0;
    public static int GearCount = 0;

    public static void AddOre(int delta)
    {
        OreCount += delta;
        if (Changed != null) Changed();
    }

    public static void AddGear(int delta)
    {
        GearCount += delta;
        if (Changed != null) Changed();
    }
}
