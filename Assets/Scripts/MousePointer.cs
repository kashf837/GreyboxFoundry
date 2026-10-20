using UnityEngine;

public class MousePointer : MonoBehaviour
{
    [SerializeField] GridManager grid;
    [SerializeField] Transform preview;
    [SerializeField] BuildManager buildManager;
    [SerializeField] Renderer previewRenderer;
    [SerializeField] Material freeMat;
    [SerializeField] Material blockedMat;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) buildManager.SetType(BuildingType.Miner);
        if (Input.GetKeyDown(KeyCode.Alpha2)) buildManager.SetType(BuildingType.Belt);
        if (Input.GetKeyDown(KeyCode.R))
        {
            buildManager.RotateDirection();
            Debug.Log($"方向 → {buildManager.CurrentDirection()}");
        }
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
        {
            preview.gameObject.SetActive(true);
            Vector2Int cell = grid.WorldToCell(hit.point);
            Vector3 worldPos = grid.CellToWorld(cell);
            Vector3 scale = buildManager.PreviewScale();
            preview.localScale = scale;
            preview.position = new Vector3(worldPos.x, scale.y / 2f, worldPos.z);
            preview.rotation = Quaternion.Euler(0, (int)buildManager.CurrentDirection() * 90f, 0);
            previewRenderer.sharedMaterial = buildManager.CanBuild(cell) ? freeMat : blockedMat;
            if (Input.GetMouseButtonDown(0))
            {
                buildManager.TryBuild(cell);
            }
            if (Input.GetMouseButtonDown(1))
            {
                buildManager.TryDemolish(cell);
            }
        }
        else
        {
            preview.gameObject.SetActive(false);
        }
    }
}
