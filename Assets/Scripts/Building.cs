using UnityEngine;

public abstract class Building : MonoBehaviour
{
    public MachineData data;
    public Direction direction = Direction.North;
    public BuildManager buildManager;
    
    public virtual bool IsBlocked() { return false; }
}
