using UnityEngine;

public class PopIn : MonoBehaviour
{
    [SerializeField] float duration = 0.25f;   // 弹多久

    float t = 0f;

    void Awake()
    {
        transform.localScale = Vector3.zero;   // 出场前先缩到看不见
    }

    void Update()
    {
        t += Time.deltaTime;

        float k = Mathf.Clamp01(t / duration);
        if (k >= 1f)
        {
            transform.localScale = Vector3.one;
            enabled = false;                   // 演完了，把自己关掉
            return;
        }

        float b = k - 1f;
        float s = 1f + 4f * b * b * b + 3f * b * b;

        transform.localScale = Vector3.one * s;
    }
}
