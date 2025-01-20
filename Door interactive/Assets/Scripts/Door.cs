using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] float OpenAngle;
    [SerializeField] float CloseAngle;
    [SerializeField] float BackOpenAngle;
    [SerializeField] float Speed;

    float targetAngle;
    [SerializeField] bool open;

    private void Start()
    {
        open = false;
    }


    void Update()
    {
        if (transform.rotation.eulerAngles.z != targetAngle)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(-90, 0, targetAngle), Time.deltaTime * Speed);
        }
    }

    public void SetTarget(Vector3 position)
    {


        if (open)
        {
            targetAngle = CloseAngle;
        }
        else
        {
            if (position.x < transform.position.x)
                targetAngle = OpenAngle;
            else
                targetAngle = BackOpenAngle;
        }
        open = !open;
        Debug.Log(open);
    }
}
