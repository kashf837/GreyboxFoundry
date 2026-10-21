using System.Collections.Generic;
using UnityEngine;

public enum BuildingType { Miner, Belt, Chest, Furnace }

public class BuildManager : MonoBehaviour
{
    [SerializeField] GameObject minerPrefab;
    [SerializeField] GameObject beltPrefab;
    [SerializeField] GameObject chestPrefab;
    [SerializeField] GameObject furnacePrefab;
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

        GameObject prefab = null;
        switch (currentType)
        {
            case BuildingType.Miner: prefab = minerPrefab; break;
            case BuildingType.Belt:  prefab = beltPrefab;  break;
            case BuildingType.Chest: prefab = chestPrefab; break;
            case BuildingType.Furnace: prefab = furnacePrefab; break;
        }
        GameObject building = Instantiate(prefab, grid.CellToWorld(cell), Quaternion.identity);

        building.name = $"{currentType} ({cell.x},{cell.y})";

        Building b = building.GetComponent<Building>();
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
        switch (currentType)
        {
            case BuildingType.Miner: return Vector3.one;
            case BuildingType.Belt:  return new Vector3(1f, 0.1f, 1f);
            case BuildingType.Furnace: return new Vector3(0.9f, 0.7f, 0.9f);
            default:                 return new Vector3(0.9f, 0.6f, 0.9f);
        }
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
