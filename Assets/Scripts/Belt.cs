using System.Collections.Generic;
using UnityEngine;

public enum Direction { North, East, South, West }

public class Belt : MonoBehaviour
{
    public Direction direction = Direction.North;
    [SerializeField] float speed = 1f; 
    List<Ore> ores = new List<Ore>();
    public BuildManager buildManager;
    const float Spacing = 0.5f;

    public bool CanAccept()
    {
        if (ores.Count == 0) return true;
        return ores[ores.Count - 1].progress >= Spacing;
    }

    public float TailProgress()
    {
        if (ores.Count == 0) return 1f;      // 空带子：假装队尾在一格开外
        return ores[ores.Count - 1].progress;
    }

    public bool AddOre(Ore ore)
    {
        if (!CanAccept()) return false;
        ore.transform.SetParent(transform);
        ore.progress = 0f;
        ore.transform.position = PointAt(0f);
        ores.Add(ore);
        return true;
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
        Belt next = buildManager.DownstreamBelt(transform.position, direction);
        float headLimit = (next == null) ? 1f : Mathf.Min(1f, Spacing + next.TailProgress());

        for (int i = 0; i < ores.Count; i++)
        {
            Ore ore = ores[i];
            float limit = (i == 0) ? headLimit : ores[i - 1].progress - Spacing;
            ore.progress = Mathf.Min(ore.progress + speed * Time.deltaTime, limit);
            ore.transform.position = PointAt(ore.progress);
        }

        Handoff(next);
    }

    void Handoff(Belt next)
    {
        if (ores.Count == 0) return;
        if (ores[0].progress < 1f) return;
        if (next == null) return;
        if (!next.AddOre(ores[0])) return;
        ores.RemoveAt(0);
    }
}
