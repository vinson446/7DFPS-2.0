using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] float mouseSens = 100;
    [SerializeField] Transform playerTrans;

    float xRot = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Cursor.lockState = Cursor.lockState;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        LookWithMouse();
    }

    void LookWithMouse()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;

        xRot -= mouseY;
        xRot = Mathf.Clamp(xRot, -90, 90);

        transform.localRotation = Quaternion.Euler(xRot, 0, 0);
        playerTrans.Rotate(Vector3.up * mouseX);
    }
}
