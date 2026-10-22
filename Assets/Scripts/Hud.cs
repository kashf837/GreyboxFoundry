using TMPro;
using UnityEngine;

public class Hud : MonoBehaviour
{
    [SerializeField] TMP_Text oreLabel;
    [SerializeField] TMP_Text gearLabel;

    void OnEnable()
    {
        ItemStats.Changed += Refresh;
        Refresh();
    }

    void OnDisable()
    {
        ItemStats.Changed -= Refresh;
    }

    void Refresh()
    {
        oreLabel.text = $"Ore  {ItemStats.OreCount}";
        gearLabel.text = $"Gear {ItemStats.GearCount}";
    }
}
