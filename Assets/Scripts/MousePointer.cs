using UnityEngine;
using UnityEngine.EventSystems;

public class MousePointer : MonoBehaviour
{
    [SerializeField] GridManager grid;
    [SerializeField] Transform preview;
    [SerializeField] Transform previewArrow;
    [SerializeField] BuildManager buildManager;
    [SerializeField] Renderer previewRenderer;
    [SerializeField] Material freeMat;
    [SerializeField] Material blockedMat;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) buildManager.SetType(BuildingType.Miner);
        if (Input.GetKeyDown(KeyCode.Alpha2)) buildManager.SetType(BuildingType.Belt);
        if (Input.GetKeyDown(KeyCode.Alpha3)) buildManager.SetType(BuildingType.Chest);
        if (Input.GetKeyDown(KeyCode.Alpha4)) buildManager.SetType(BuildingType.Furnace);
        if (Input.GetKeyDown(KeyCode.Alpha5)) buildManager.SetType(BuildingType.Turret);
        if (Input.GetKeyDown(KeyCode.R)) buildManager.RotateDirection();

        if (EventSystem.current.IsPointerOverGameObject())
        {
            preview.gameObject.SetActive(false);
            previewArrow.gameObject.SetActive(false);
            return;
        }
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
        {
            Vector2Int cell = grid.WorldToCell(hit.point);
            Vector3 worldPos = grid.CellToWorld(cell);
            Vector3 scale = buildManager.PreviewScale();

            preview.gameObject.SetActive(true);
            preview.localScale = scale;
            preview.position = new Vector3(worldPos.x, scale.y / 2f, worldPos.z);
            preview.rotation = Quaternion.Euler(0, (int)buildManager.CurrentDirection() * 90f, 0);
            previewRenderer.sharedMaterial = buildManager.CanBuild(cell) ? freeMat : blockedMat;

            previewArrow.gameObject.SetActive(buildManager.CurrentType() != BuildingType.Chest);
            previewArrow.position = new Vector3(worldPos.x, scale.y, worldPos.z);
            previewArrow.rotation = preview.rotation;

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
            previewArrow.gameObject.SetActive(false);
        }
    }
}
