using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotation : MonoBehaviour
{
    [SerializeField] float xSensitivity;
    [SerializeField] float ySensitivity;

    [SerializeField] Transform direction;

    float xMouse;
    float yMouse;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        float xInput = Input.GetAxisRaw("Mouse X") * xSensitivity * Time.deltaTime;
        float yInput = Input.GetAxisRaw("Mouse Y") * ySensitivity * Time.deltaTime;

        yMouse += xInput;
        xMouse -= yInput;

        xMouse = Mathf.Clamp(xMouse, -90f, 90f);

        transform.rotation = Quaternion.Euler(xMouse, yMouse, 0);
        direction.rotation = Quaternion.Euler(0, yMouse, 0);
    }

}
