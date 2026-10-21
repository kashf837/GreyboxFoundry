using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] int capacity = 50;
    int count = 0;

    public bool CanAccept()
    {
        return count < capacity;
    }

    public bool AddOre(Ore ore, Direction travelDirection)
    {
        if (!CanAccept()) return false;
        count++;
        Destroy(ore.gameObject);
        Debug.Log($"[{name}] {count}/{capacity}");
        return true;
    }
}
