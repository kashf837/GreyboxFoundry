using UnityEngine;

public class MousePointer : MonoBehaviour
{
    [SerializeField] GridManager grid;
    [SerializeField] Transform preview;
    [SerializeField] BuildManager buildManager;

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
        {
            preview.gameObject.SetActive(true);
            Vector2Int cell = grid.WorldToCell(hit.point);
            Vector3 worldPos = grid.CellToWorld(cell);
            preview.position = new Vector3(worldPos.x, 0.5f, worldPos.z);
            if (Input.GetMouseButtonDown(0))
            {
                buildManager.TryBuild(cell);
            }
        }
        else
        {
            preview.gameObject.SetActive(false);
        }
    }
}
