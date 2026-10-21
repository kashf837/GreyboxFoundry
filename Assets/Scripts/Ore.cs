using UnityEngine;

public class Ore : MonoBehaviour
{
    public float progress = 0f;
    public Direction entrySide = Direction.North;
    public bool blocked = false;

    [SerializeField] Renderer rend;
    [SerializeField] Material movingMat;
    [SerializeField] Material blockedMat;

    public void SetBlocked(bool value)
    {
        if (blocked == value) return;
        blocked = value;
        rend.sharedMaterial = value ? blockedMat : movingMat;
    }
}
