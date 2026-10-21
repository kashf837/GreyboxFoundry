using UnityEngine;

public class Furnace : MonoBehaviour
{
    public MachineData data;
    public Direction direction = Direction.North;
    public BuildManager buildManager;

    [SerializeField] GameObject productPrefab;

    bool busy = false;
    float timer = 0f;

    public bool CanAccept(Direction travelDirection)
    {
        return !busy;
    }

    public bool AddOre(Ore ore, Direction travelDirection)
    {
        if (CanAccept(travelDirection) == false) return false;
        Destroy(ore.gameObject);
        busy = true;
        timer = 0f;
        return true;
    }

    void Update()
    {
        if (busy == false) return;

        timer += Time.deltaTime;
        if (timer <= data.craftTime) return;

        Belt next = buildManager.DownstreamBelt(transform.position, direction);
        if (next == null || next.CanAccept(direction) == false) return;

        GameObject product = Instantiate(productPrefab, Vector3.zero, Quaternion.identity);
        next.AddOre(product.GetComponent<Ore>(), direction);

        busy = false;
        timer = 0f;
    }
}
