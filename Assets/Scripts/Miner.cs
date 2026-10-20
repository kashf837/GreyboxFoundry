using UnityEngine;

public class Miner : MonoBehaviour
{
    float timer = 0.0f;
    [SerializeField] GameObject orePrefab;
    [SerializeField] Transform outputPoint;
    int oreCount = 0;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 2.0f)
        {
            timer = 0.0f;
            Vector3 pos = outputPoint.position + Vector3.up * 0.35f * oreCount;
            GameObject ore = Instantiate(orePrefab, pos, Quaternion.identity, transform);
            oreCount++;
            Debug.Log($"[{name}] 出矿 t={Time.time:F2} 位置={ore.transform.position}");
        }
    }
}
