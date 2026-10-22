using UnityEngine;

public class Chest : Building, IReceiver
{
    int count = 0;

    public bool CanAccept(Direction travelDirection)
    {
        return count < data.capacity;
    }

    public override bool IsBlocked()
    {
        return count >= data.capacity;
    }

    public bool AddOre(Ore ore, Direction travelDirection)
    {
        if (CanAccept(travelDirection) == false) return false;
        count++;
        Destroy(ore.gameObject);

        if (count == data.capacity)
            Debug.Log($"[{name}] 满了（{count}/{data.capacity}）");

        return true;
    }
}
