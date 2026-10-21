using UnityEngine;

public class Miner : Building
{
    float timer = 0.0f;
    [SerializeField] GameObject orePrefab;
    bool wasBlocked = false;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer <= 2.0f) return;
        timer = 0.0f;

        Belt belt = buildManager.DownstreamBelt(transform.position, direction);
        bool blocked = (belt == null || !belt.CanAccept(direction));

        if (blocked != wasBlocked)
        {
            Debug.Log(blocked ? $"[{name}] 下游堵塞，停产" : $"[{name}] 下游恢复");
            wasBlocked = blocked;
        }

        if (blocked) return;

        GameObject oreObj = Instantiate(orePrefab, Vector3.zero, Quaternion.identity);
        belt.AddOre(oreObj.GetComponent<Ore>(), direction);
    }
    
}
