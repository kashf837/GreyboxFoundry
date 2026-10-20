using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    [SerializeField] GameObject minerPrefab;
    Dictionary<Vector2Int, GameObject> occupied = new Dictionary<Vector2Int, GameObject>();
    [SerializeField] GridManager grid;

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
        if (IsOccupied(cell))
        {
            return false;
        }
        GameObject building = Instantiate(minerPrefab, grid.CellToWorld(cell), Quaternion.identity);
        building.name = $"Miner ({cell.x},{cell.y})";
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
}
