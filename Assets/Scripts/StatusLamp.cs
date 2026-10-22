using UnityEngine;

public class StatusLamp : MonoBehaviour
{
    Renderer rend;
    Building building;
    bool wasBlocked = false;

    void Awake()
    {
        rend = GetComponent<Renderer>();
        building = GetComponentInParent<Building>();
        rend.enabled = false;          // 平时不显示
    }

    void Update()
    {
        bool blocked = building.IsBlocked();
        if (blocked == wasBlocked) return;

        wasBlocked = blocked;
        rend.enabled = blocked;
    }
}
