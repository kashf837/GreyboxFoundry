using UnityEngine;

public class Chest : Building, IReceiver
{
    int count = 0;

    public bool CanAccept(Direction travelDirection)
    {
        return count < data.capacity;
    }

    public bool AddOre(Ore ore, Direction travelDirection)
    {
        if (!CanAccept(travelDirection)) return false;
        count++;
        Destroy(ore.gameObject);
        Debug.Log($"[{name}] {count}/{data.capacity}");
        return true;
    }
}
