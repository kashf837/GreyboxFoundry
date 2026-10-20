using UnityEngine;

public class Miner : MonoBehaviour
{
    public Direction direction = Direction.North;
    public BuildManager buildManager;

    float timer = 0.0f;
    [SerializeField] GameObject orePrefab;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 2.0f)
        {
            timer = 0.0f;
            Belt belt = DownstreamBelt();
            if (belt == null || !belt.CanAccept()) return;

            GameObject oreObj = Instantiate(orePrefab, Vector3.zero, Quaternion.identity);
            belt.AddOre(oreObj.GetComponent<Ore>());
        }
    }
    
    Belt DownstreamBelt()
    {
        GameObject front = buildManager.BuildingInFront(transform.position, direction);
        if (front == null) return null;
        Belt belt = front.GetComponent<Belt>();
        if (belt == null) return null;
        if (belt.direction != direction) return null;
        return belt;
    }
}
