using System.Collections.Generic;
using UnityEngine;

public enum Direction { North, East, South, West }

public class Belt : MonoBehaviour
{
    public Direction direction = Direction.North;
    [SerializeField] float speed = 1f; 
    List<Ore> ores = new List<Ore>();

    public void AddOre(Ore ore)
    {
        ore.progress = 0f;
        ore.transform.position = PointAt(0f);
        ores.Add(ore);
    }

    public Vector3 PointAt(float t)
    {
        Vector3 lift = Vector3.up * 0.25f;      // 带面 0.1 + 矿石半高 0.15
        Vector3 start = transform.position - transform.forward * 0.5f + lift;
        Vector3 end   = transform.position + transform.forward * 0.5f + lift;
        return Vector3.Lerp(start, end, t);
    }

    void Update()
    {
        for (int i = 0; i < ores.Count; i++)
        {
            Ore ore = ores[i];
            float limit = (i == 0) ? 1f : ores[i - 1].progress - 0.5f;
            ore.progress = Mathf.Min(ore.progress + speed * Time.deltaTime, limit);
            ore.transform.position = PointAt(ore.progress);
        }
    }
}
