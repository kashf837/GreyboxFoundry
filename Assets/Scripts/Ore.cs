using UnityEngine;

public enum ItemType { Ore, Gear }

public class Ore : MonoBehaviour
{
    public ItemType itemType = ItemType.Ore;
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
