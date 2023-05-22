using UnityEngine;

public class CylinderRotator : MonoBehaviour
{
    public float rotationSpeedPC = 100;
    public float rotationSpeedPhone = 50;

    void Update()
    {
        // якщо на телефоні
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                float touchX = Input.GetTouch(0).deltaPosition.x;
                transform.Rotate(transform.rotation.x, -touchX * rotationSpeedPhone * Time.deltaTime, transform.rotation.z);
            }
        }

        // якщо на комп'ютері
        if (Input.GetMouseButton(0))
        {
            float mouseX = Input.GetAxisRaw("Mouse X");
            transform.Rotate(transform.rotation.x, -mouseX * rotationSpeedPC * Time.deltaTime, transform.rotation.z);
        }
    }
}
