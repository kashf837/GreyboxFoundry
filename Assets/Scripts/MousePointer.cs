using UnityEngine;

public class MousePointer : MonoBehaviour
{
    [SerializeField] GridManager grid;
    [SerializeField] Transform preview;

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
        {
            preview.gameObject.SetActive(true);
            Vector3 worldPos = grid.CellToWorld(grid.WorldToCell(hit.point));
            preview.position = new Vector3(worldPos.x, 0.5f, worldPos.z);
        }
        else
        {
            preview.gameObject.SetActive(false);
        }
    }
}
