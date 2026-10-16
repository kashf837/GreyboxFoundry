using UnityEngine;

public class CameraController : MonoBehaviour
{
    float speed = 10.0f;
    float zoomSpeed = 10.0f;

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 dir = new(h, 0, v);
        dir.Normalize();

        transform.position += speed * Time.deltaTime * dir;

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        transform.LookAt(Vector3.zero);

        if (transform.position.y + scroll * zoomSpeed * transform.forward.y > 5.0f)
        {
            // 滚轮已经是每帧的量了，不乘 deltaTime
            transform.position += scroll * zoomSpeed * transform.forward;
        }

    }
}
