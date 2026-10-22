using UnityEngine;

public class Miner : Building
{
    float timer = 0.0f;
    bool blocked = false;

    public override bool IsBlocked() { return blocked; }

    bool CanDeliver()
    {
        Belt belt = buildManager.DownstreamBelt(transform.position, direction);
        return belt != null && belt.CanAccept(direction);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer <= data.craftTime) return;
        timer = 0.0f;

        bool nowBlocked = !CanDeliver();
        if (nowBlocked != blocked)
        {
            blocked = nowBlocked;
            Debug.Log(blocked ? $"[{name}] 下游堵塞，停产" : $"[{name}] 下游恢复");
        }

        if (blocked) return;

        Belt belt = buildManager.DownstreamBelt(transform.position, direction);
        GameObject oreObj = Instantiate(data.outputPrefab, Vector3.zero, Quaternion.identity);
        belt.AddOre(oreObj.GetComponent<Ore>(), direction);
    }
}
