using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerControll : MonoBehaviour
{
    public float sensitivity = 100f;

    private float mouseY = 0;

    // Update is called once per frame
    void Update()
    {
        mouseY += Input.GetAxis("Mouse Y") * Time.deltaTime;
        if (mouseY < -0.83f)
        {
            mouseY = -0.83f;
        }
        if (mouseY > 0.83f)
        {
            mouseY = 0.83f;
        }

        transform.localRotation = Quaternion.AngleAxis(-mouseY * sensitivity, Vector3.right);
    }
}