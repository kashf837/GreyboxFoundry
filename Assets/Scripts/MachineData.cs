using UnityEngine;

[CreateAssetMenu(menuName = "GreyboxFoundry/MachineData")]
public class MachineData : ScriptableObject
{
    public string displayName = "机器";
    public GameObject prefab;
    public Vector3 previewScale = Vector3.one;
    public bool showArrow = true;
    public ItemType inputType = ItemType.Ore;
    public ItemType outputType = ItemType.Ore;
    public float craftTime = 2f;
    public float speed = 1f;
    public int capacity = 50;
    public GameObject outputPrefab;
}
