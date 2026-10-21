using UnityEngine;

public class Chest : MonoBehaviour, IReceiver
{
    [SerializeField] int capacity = 50;
    int count = 0;

    public bool CanAccept(Direction travelDirection)
    {
        return count < capacity;
    }

    public bool AddOre(Ore ore, Direction travelDirection)
    {
        if (!CanAccept(travelDirection)) return false;
        count++;
        Destroy(ore.gameObject);
        Debug.Log($"[{name}] {count}/{capacity}");
        return true;
    }
}
