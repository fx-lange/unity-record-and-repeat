using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : MonoBehaviour
{
    public float moveSpeed = 0.1f;
    public float rotationSpeed = 5f;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey("w"))
        {
            transform.position += transform.forward * moveSpeed;
        }
        else if (Input.GetKey("s"))
        {
            transform.position -= transform.forward * moveSpeed;
        }

        if (Input.GetKey("a"))
        {
            transform.Rotate(new Vector3(0, -rotationSpeed, 0));
        }
        else if (Input.GetKey("d"))
        {
            transform.Rotate(new Vector3(0, +rotationSpeed, 0));
        }
    }
}
