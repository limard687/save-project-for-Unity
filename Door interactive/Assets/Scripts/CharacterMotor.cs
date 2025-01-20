using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMotor : MonoBehaviour
{
    [SerializeField] float Speed = 5;
    [SerializeField] float LookSensitivity;
    public bool FreezeLook;
    Transform cam;
    Rigidbody rb;
    float xRotation;
    float yRotation;

    private void Start()
    {
        cam = Camera.main.transform;
        rb = GetComponent<Rigidbody>();
        FreezeLook = false;
    }

    void FixedUpdate()
    {
        rb.velocity = (transform.forward * Input.GetAxisRaw("Vertical") + transform.right * Input.GetAxisRaw("Horizontal")).normalized * Speed * Time.deltaTime;

        if (!FreezeLook)
        {
            xRotation += Input.GetAxis("Mouse X") * Time.deltaTime * LookSensitivity;
            yRotation -= Input.GetAxis("Mouse Y") * Time.deltaTime * LookSensitivity;
            yRotation = Mathf.Clamp(yRotation, -80, 80);
            if (xRotation >= 360)
                xRotation -= 360;
            if (xRotation < 0)
                xRotation += 360;
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, xRotation, transform.rotation.eulerAngles.z);
            cam.rotation = Quaternion.Euler(yRotation, cam.rotation.eulerAngles.y, cam.rotation.eulerAngles.z);
        }

    }
}
