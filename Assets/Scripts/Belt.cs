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
    const float Lift = 0.25f; 

    public static Direction Opposite(Direction d)
    {
        return (Direction)(((int)d + 2) % 4);
    }

    public Vector3 EdgePoint(Direction side)
    {
        Vector3 dir = Quaternion.Euler(0, (int)side * 90f, 0) * Vector3.forward;
        return transform.position + dir * 0.5f + Vector3.up * Lift;
    }

    public Vector3 PointAt(float t, Direction entrySide)
    {
        Vector3 center = transform.position + Vector3.up * Lift;
        Vector3 entry = EdgePoint(entrySide);
        Vector3 exit  = EdgePoint(direction);

        float u = 1f - t;
        return u * u * entry + 2f * u * t * center + t * t * exit;
    }

    public bool CanAccept(Direction travelDirection)
    {
        if (Opposite(travelDirection) == direction) return false;
        if (ores.Count == 0) return true;
        return ores[ores.Count - 1].progress >= Spacing;
    }


    public float TailProgress()
    {
        if (ores.Count == 0) return 1f;      // 空带子：假装队尾在一格开外
        return ores[ores.Count - 1].progress;
    }

    public bool AddOre(Ore ore, Direction travelDirection)
    {
        if (!CanAccept(travelDirection)) return false;
        ore.transform.SetParent(transform);
        ore.entrySide = Opposite(travelDirection);          // 方向 → 边，只转这一次
        ore.progress = 0f;
        ore.transform.position = EdgePoint(ore.entrySide);
        ores.Add(ore);
        return true;
    }

    void Update()
    {
        Belt next = buildManager.DownstreamBelt(transform.position, direction);
        float headLimit = (next == null) ? 1f : Mathf.Min(1f, Spacing + next.TailProgress());

        for (int i = 0; i < ores.Count; i++)
        {
            Ore ore = ores[i];
            float limit = (i == 0) ? headLimit : ores[i - 1].progress - Spacing;
            float before = ore.progress;

            ore.progress = Mathf.Min(ore.progress + speed * Time.deltaTime, limit);
            ore.SetBlocked(ore.progress <= before);
            ore.transform.position = PointAt(ore.progress, ore.entrySide);
        }

        Handoff(next);
    }

    void Handoff(Belt next)
    {
        if (ores.Count == 0) return;
        if (ores[0].progress < 1f) return;
        if (next == null) return;
        if (!next.AddOre(ores[0], direction)) return;      // 递方向，不递坐标
        ores.RemoveAt(0);
    }
}
