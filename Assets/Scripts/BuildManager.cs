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

    public bool TryBuild(Vector2Int cell)
    {
        if (IsOccupied(cell))
        {
            return false;
        }
        occupied.Add(cell, Instantiate(minerPrefab, grid.CellToWorld(cell), Quaternion.identity));
        return true;
    }
}
