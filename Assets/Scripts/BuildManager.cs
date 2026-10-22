using System.Collections.Generic;
using UnityEngine;

public enum BuildingType { Miner, Belt, Chest, Furnace, Turret }

public class BuildManager : MonoBehaviour
{
    [SerializeField] List<MachineData> machines;
    Dictionary<Vector2Int, GameObject> occupied = new Dictionary<Vector2Int, GameObject>();
    [SerializeField] GridManager grid;
    BuildingType currentType = BuildingType.Miner;
    Direction currentDirection = Direction.North;

    public bool IsOccupied(Vector2Int cell)
    {
        return occupied.ContainsKey(cell);
    }

    public bool CanBuild(Vector2Int cell)
    {
        return !IsOccupied(cell);
    }

    public bool TryBuild(Vector2Int cell)
    {
        if (IsOccupied(cell)) return false;

        MachineData data = machines[(int)currentType];
        GameObject building = Instantiate(data.prefab, grid.CellToWorld(cell), Quaternion.identity);

        building.name = $"{currentType} ({cell.x},{cell.y})";

        Building b = building.GetComponent<Building>();
        b.data = data;
        b.direction = currentDirection;
        b.buildManager = this;

        building.transform.rotation = Quaternion.Euler(0, (int)currentDirection * 90f, 0);

        occupied.Add(cell, building);
        return true;
    }

    public bool TryDemolish(Vector2Int cell)
    {
        if (!IsOccupied(cell))
        {
            return false;
        }
        Destroy(occupied[cell]);
        occupied.Remove(cell);
        return true;
    }

    public void SetType(BuildingType type) { currentType = type; }

    public Vector3 PreviewScale()      // 当前类型的预览尺寸
    {
        return machines[(int)currentType].previewScale;
    }

    public bool ShowArrow()      // 当前类型的预览要不要显示方向箭头
    {
        return machines[(int)currentType].showArrow;
    }

    public Direction CurrentDirection() { return currentDirection; }

    public void RotateDirection()
    {
        currentDirection = (Direction)(((int)currentDirection + 1) % 4);
    }

    public BuildingType CurrentType() { return currentType; }

    public GameObject GetBuilding(Vector2Int cell)
    {
        occupied.TryGetValue(cell, out GameObject building);
        return building;
    }
    public GameObject BuildingInFront(Vector3 from, Direction dir)
    {
        Vector3 ahead = from + Quaternion.Euler(0, (int)dir * 90f, 0) * Vector3.forward * grid.CellSize();
        return GetBuilding(grid.WorldToCell(ahead));
    }

    public Belt DownstreamBelt(Vector3 from, Direction dir)
    {
        GameObject front = BuildingInFront(from, dir);
        if (front == null) return null;
        return front.GetComponent<Belt>();
    }

}
