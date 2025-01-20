using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactive : MonoBehaviour
{
    [SerializeField] float distance;
    [SerializeField] float OpenOffset;
    [SerializeField] float GrabSpeed;
    [SerializeField] CharacterMotor character;

    RaycastHit hit;
    Rigidbody grabedDoor = null;

    float grabOffset;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        Debug.DrawLine(transform.position, transform.position + transform.forward * 10);
        if (Input.GetButtonDown("Fire1"))
        {
            if (Physics.Raycast(transform.position, transform.forward, out hit, distance, LayerMask.GetMask("Default")))
            {
                Debug.Log(hit.transform.name);
                if (hit.transform.tag == "AnimatedDoor")
                {
                    Animator anim = hit.transform.GetComponent<Animator>();
                    anim.SetBool("Open", !anim.GetBool("Open"));
                }

                if (hit.transform.tag == "ScriptedDoor")
                {
                    hit.transform.GetComponent<Door>().SetTarget(transform.position);
                }

                if (hit.transform.tag == "PhysicalDoor")
                {
                    grabedDoor = hit.transform.GetComponent<Rigidbody>();
                    grabOffset = 0;
                }
            }
        }

        if (Input.GetButton("Fire1") && grabedDoor != null)
        {
            character.FreezeLook = true;
            grabOffset += Input.GetAxis("Mouse Y");
            if (Mathf.Abs(grabOffset) > OpenOffset)
            {
                grabedDoor.isKinematic = false;
                if (transform.position.x < grabedDoor.transform.position.x)
                {
                    grabedDoor.AddTorque(Vector3.up * Input.GetAxis("Mouse Y") * GrabSpeed, ForceMode.VelocityChange);
                }
                else
                {
                    grabedDoor.AddTorque(-Vector3.up * Input.GetAxis("Mouse Y") * GrabSpeed, ForceMode.VelocityChange);
                }
            }
            Debug.Log(grabOffset);
        }

        if (Input.GetButtonUp("Fire1"))
        {
            grabedDoor = null;
            character.FreezeLook = false;
            Debug.Log("Drop");
        }
    }

    private void OnGUI()
    {
        GUI.Button(new Rect(Screen.width / 2, Screen.height / 2, 3, 3), "");
    }
}
