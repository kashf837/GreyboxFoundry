using UnityEngine;

public class GridManager : MonoBehaviour
{
    float gridSize = 1.0f;
    int width = 40;
    int height = 40;
    Vector3 origin = new(-20, 0, -20);
    void Start()
    {

    }

    Vector2Int WorldToCell(Vector3 worldPos)
    {
        Vector3 localPos = worldPos - origin;
        int x = Mathf.FloorToInt(localPos.x / gridSize);
        int y = Mathf.FloorToInt(localPos.z / gridSize);
        return new Vector2Int(x, y);
    }

    Vector3 CellToWorld(Vector2Int cell)
    {
        float x = cell.x * gridSize + gridSize / 2.0f;
        float z = cell.y * gridSize + gridSize / 2.0f;
        return new Vector3(x, 0, z) + origin;
    }
}
