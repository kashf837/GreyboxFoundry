using UnityEngine;

public class Furnace : Building, IReceiver
{
    bool busy = false;
    float timer = 0f;
    bool blocked = false;          // 上一次"想交货"时的结论，存下来给灯看

    public bool CanAccept(Direction travelDirection) { return !busy; }

    public bool AddOre(Ore ore, Direction travelDirection)
    {
        if (CanAccept(travelDirection) == false) return false;
        Destroy(ore.gameObject);
        busy = true;
        timer = 0f;
        blocked = false;           // 新一轮加工开始，先当我没堵
        return true;
    }

    public override bool IsBlocked() { return blocked; }

    bool CanDeliver()
    {
        Belt next = buildManager.DownstreamBelt(transform.position, direction);
        return next != null && next.CanAccept(direction);
    }

    void Update()
    {
        if (busy == false) return;

        timer += Time.deltaTime;
        if (timer <= data.craftTime) return;      // ← 还在加工，谈不上堵

        bool nowBlocked = !CanDeliver();
        if (nowBlocked != blocked)
        {
            blocked = nowBlocked;
            Debug.Log(blocked ? $"[{data.displayName}] 出口堵塞" : $"[{data.displayName}] 出口恢复");
        }

        if (blocked) return;

        Belt next = buildManager.DownstreamBelt(transform.position, direction);
        GameObject product = Instantiate(data.outputPrefab, Vector3.zero, Quaternion.identity);
        next.AddOre(product.GetComponent<Ore>(), direction);

        busy = false;
        timer = 0f;
    }
}
