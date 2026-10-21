using UnityEngine;

public class BuildButton : MonoBehaviour
{
    public BuildingType type;
    public BuildManager buildManager;

    public void Select()
    {
        buildManager.SetType(type);
    }
}
