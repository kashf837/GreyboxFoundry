using UnityEngine;

public class CameraController : MonoBehaviour
{
    float speed = 10.0f;
    
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 dir = new(h, 0, v);
        dir.Normalize();

        transform.position += speed * Time.deltaTime * dir;
    }
}
