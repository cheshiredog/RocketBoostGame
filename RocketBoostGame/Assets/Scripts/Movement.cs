using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody myRigidbody;
    Transform myTransform;
    // Rocket speed variable
    [SerializeField] float pushSpeed = 100f;
    // Rocket rotation speed variable
    [SerializeField] float rotateSpeed = 100f;

    // Start is called before the first frame update
    void Start()
    {
        // Getting access to the rigidbody
        myRigidbody = GetComponent<Rigidbody>();
        // Getting access to the transform
        myTransform= GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        // Pushing rocket
        ProcessThrust();
        // Rotating rocket
        ProcessRotation();
    }

    void ProcessThrust()
    {
        // Checking if space is pressed
        if (Input.GetKey(KeyCode.Space))
        {
            // Pushing rocket upwards by y-axis, if space is pressed
            myRigidbody.AddRelativeForce(pushSpeed * Time.deltaTime * Vector3.up);
        }
    }

    void ProcessRotation()
    {
        // Checking if A key is pressed
        if (Input.GetKey(KeyCode.A))
        {
            // Rotating rocket to the left
            ApplyRotation(rotateSpeed);
        }
        // Checking if D key is pressed
        else if (Input.GetKey(KeyCode.D))
        {
            // Rotating rocket to the right
            ApplyRotation(-rotateSpeed);
        }
    }

    void ApplyRotation(float rotation)
    {
        // Rotating rocket
        myTransform.Rotate(rotation * Time.deltaTime * Vector3.forward);
    }
}
