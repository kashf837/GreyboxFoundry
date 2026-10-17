using UnityEngine;

public class CameraController : MonoBehaviour
{
    float speed = 15.0f;
    float zoomSpeed = 10.0f;
    Vector3 pivot = Vector3.zero;

    void Update()
    {

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 fwd = transform.forward;
        fwd.y = 0;
        fwd.Normalize();
        Vector3 dir = h * transform.right + v * fwd;
        dir.Normalize();

        transform.position += speed * Time.deltaTime * dir;
        pivot += speed * Time.deltaTime * dir;

        transform.LookAt(pivot);

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (transform.position.y + scroll * zoomSpeed * transform.forward.y > 5.0f)
        {
            // 滚轮已经是每帧的量了，不乘 deltaTime
            transform.position += scroll * zoomSpeed * transform.forward;
        }

    }
}
