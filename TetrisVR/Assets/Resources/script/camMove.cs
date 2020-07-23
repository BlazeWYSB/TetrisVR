using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camMove : MonoBehaviour
{
    public float moveSpeed = 2.0f;
    public float rotationSpeed = 100.0f;
    public GameObject camVR;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            moveSpeed = 8.0f;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            moveSpeed = 2.0f;
        }
        camVR.transform.Translate(Vector3.forward * v * moveSpeed * Time.deltaTime);
        camVR.transform.Translate(Vector3.right * h * moveSpeed * Time.deltaTime);
        if (Input.GetKey(KeyCode.R))
        {
            camVR.transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.F))
        {
            camVR.transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.T))
        {
            camVR.transform.Rotate(Vector3.right * Time.deltaTime * -rotationSpeed);
        }
        if (Input.GetKey(KeyCode.G))
        {
            camVR.transform.Rotate(Vector3.right * Time.deltaTime * rotationSpeed);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            camVR.transform.RotateAround(camVR.transform.position, Vector3.up, Time.deltaTime * -rotationSpeed);
        }
        if (Input.GetKey(KeyCode.E))
        {
            camVR.transform.RotateAround(camVR.transform.position, Vector3.up, Time.deltaTime * rotationSpeed);
        }
    }
}

