using UnityEngine;

[CreateAssetMenu(menuName = "GreyboxFoundry/MachineData")]
public class MachineData : ScriptableObject
{
    public string displayName = "机器";
    public ItemType inputType = ItemType.Ore;
    public ItemType outputType = ItemType.Ore;
    public float craftTime = 2f;
}
