using UnityEngine;

public class PopIn : MonoBehaviour
{
    [SerializeField] Transform visual;
    [SerializeField] float duration = 0.25f;

    float t = 0f;

    void Awake()
    {
        visual.localScale = Vector3.zero;
    }

    void Update()
    {
        t += Time.deltaTime;

        float k = Mathf.Clamp01(t / duration);
        if (k >= 1f)
        {
            visual.localScale = Vector3.one;
            enabled = false;
            return;
        }

        float b = k - 1f;
        float s = 1f + 4f * b * b * b + 3f * b * b;

        visual.localScale = Vector3.one * s;
    }
}
