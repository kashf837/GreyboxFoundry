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
            Belt belt = buildManager.DownstreamBelt(transform.position, direction);
            if (belt == null || !belt.CanAccept(direction)) return;

            GameObject oreObj = Instantiate(orePrefab, Vector3.zero, Quaternion.identity);
            belt.AddOre(oreObj.GetComponent<Ore>(), direction);
        }
    }
    
}
