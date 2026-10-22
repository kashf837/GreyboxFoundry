using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildMenu : MonoBehaviour
{
    [SerializeField] BuildManager buildManager;
    [SerializeField] List<BuildButton> buttons;
    [SerializeField] Color normalColor = new Color(0.25f, 0.25f, 0.28f, 1f);
    [SerializeField] Color selectedColor = new Color(1f, 0.6f, 0.15f, 1f);

    int lastType = -1;

    void Update()
    {
        int current = (int)buildManager.CurrentType();
        if (current == lastType) return;
        lastType = current;
        Refresh();
    }

    void Refresh()
    {
        foreach (BuildButton b in buttons)
        {
            Image img = b.GetComponent<Image>();
            img.color = (b.type == buildManager.CurrentType()) ? selectedColor : normalColor;
        }
    }
}
