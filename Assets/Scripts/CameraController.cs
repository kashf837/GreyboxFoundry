using UnityEngine;

public class CameraController : MonoBehaviour
{
    float speed = 20.0f;
    float zoomSpeed = 20.0f;
    float turnSpeed = 60.0f;
    Vector3 pivot = Vector3.zero;

    void Update()
    {
        // 获取水平和垂直输入
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // 计算移动方向
        Vector3 fwd = transform.forward;
        fwd.y = 0;
        fwd.Normalize();
        Vector3 dir = h * transform.right + v * fwd;
        dir.Normalize();

        // 移动摄像机和枢轴点
        transform.position += speed * Time.deltaTime * dir;
        pivot += speed * Time.deltaTime * dir;

        // 获取鼠标滚轮输入
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        // 缩放摄像机，限制最小高度为5.0f
        if (transform.position.y + scroll * zoomSpeed * transform.forward.y > 5.0f)
        {
            // 滚轮已经是每帧的量了，不乘 deltaTime
            transform.position += scroll * zoomSpeed * transform.forward;
        }

        // Q/E 旋转摄像机，Q键顺时针，E键逆时针
        float rotateDir = 0.0f;
        if (Input.GetKey(KeyCode.Q))
        {
            rotateDir += 1.0f;
        }
        if (Input.GetKey(KeyCode.E))
        {
            rotateDir -= 1.0f;
        }
        transform.RotateAround(pivot, Vector3.up, rotateDir * turnSpeed * Time.deltaTime);

        transform.LookAt(pivot);

    }
}
