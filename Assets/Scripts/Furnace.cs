using UnityEngine;

public class Furnace : Building, IReceiver
{
    bool busy = false;
    float timer = 0f;
    bool wasBlocked = false;

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
        wasBlocked = false;
        return true;
    }

    public override bool IsBlocked()
    {
        if (busy == false) return false;
        Belt next = buildManager.DownstreamBelt(transform.position, direction);
        return next == null || next.CanAccept(direction) == false;
    }

    void Update()
    {
        if (busy == false) return;

        timer += Time.deltaTime;

        Belt next = buildManager.DownstreamBelt(transform.position, direction);
        bool blocked = IsBlocked();

        if (blocked != wasBlocked)
        {
            Debug.Log(blocked ? $"[{data.displayName}] 出口堵塞" : $"[{data.displayName}] 出口恢复");
            wasBlocked = blocked;
        }

        if (timer <= data.craftTime) return;
        if (blocked) return;

        GameObject product = Instantiate(data.outputPrefab, Vector3.zero, Quaternion.identity);
        next.AddOre(product.GetComponent<Ore>(), direction);

        busy = false;
        timer = 0f;
    }
}
