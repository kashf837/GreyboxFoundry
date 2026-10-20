using UnityEngine;

public class Miner : MonoBehaviour
{
    float timer = 0.0f;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 2.0f)
        {
            timer = 0.0f;
            Debug.Log($"[{name}] 出矿 t={Time.time:F2}");
        }
    }
}
