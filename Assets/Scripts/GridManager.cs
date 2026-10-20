using UnityEngine;

public class GridManager : MonoBehaviour
{
    float gridSize = 1.0f;
    float plotSize = 40.0f;
    Vector3 origin = new(-20, 0, -20);

    void OnDrawGizmos()
    {
        int cellCount = Mathf.RoundToInt(plotSize / gridSize);
        float y0 = 0.01f; // 将网格线稍微抬高一点，避免与地面重叠
        Gizmos.color = Color.white;
        for (int x = 0; x <= cellCount; x++)
        {
            Vector3 start = origin + new Vector3(x * gridSize, y0, 0);
            Vector3 end = origin + new Vector3(x * gridSize, y0, plotSize);
            Gizmos.DrawLine(start, end);
        }
        for (int y = 0; y <= cellCount; y++)
        {
            Vector3 start = origin + new Vector3(0, y0, y * gridSize);
            Vector3 end = origin + new Vector3(plotSize, y0, y * gridSize);
            Gizmos.DrawLine(start, end);
        }
    }

    public Vector2Int WorldToCell(Vector3 worldPos)
    {
        Vector3 localPos = worldPos - origin;
        int x = Mathf.FloorToInt(localPos.x / gridSize);
        int y = Mathf.FloorToInt(localPos.z / gridSize);
        return new Vector2Int(x, y);
    }

    public Vector3 CellToWorld(Vector2Int cell)
    {
        float x = cell.x * gridSize + gridSize / 2.0f;
        float z = cell.y * gridSize + gridSize / 2.0f;
        return new Vector3(x, 0, z) + origin;
    }

    public float CellSize() { return gridSize; }
}
