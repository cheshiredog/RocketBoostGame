using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float pushSpeed = 500f; // Rocket speed variable
    [SerializeField] float rotateSpeed = 100f; // Rocket rotation speed variable
    [SerializeField] AudioClip rocketBoostSound; // Getting audio clip from the editor

    Rigidbody myRigidbody;
    Transform myTransform;
    AudioSource myAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>(); // Getting access to the rigidbody
        myTransform = GetComponent<Transform>(); // Getting access to the transform
        myAudioSource = GetComponent<AudioSource>(); // Getting access to the audio source
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust(); // Pushing rocket
        ProcessRotation(); // Rotating rocket
    }

    void ProcessThrust() // Pushing rocket
    {
        // Checking if space is pressed
        if (Input.GetKey(KeyCode.Space))
        {
            myRigidbody.AddRelativeForce(pushSpeed * Time.deltaTime * Vector3.up); // Pushing rocket upwards by y-axis, if space is pressed

            // Checking if nothing is playing
            if (!myAudioSource.isPlaying)
            {
                myAudioSource.PlayOneShot(rocketBoostSound); // Starting playing rocket sound
            }
        }

        else
        {
            myAudioSource.Stop(); // Stopping rocket sound
        }
    }

    void ProcessRotation() // Rotating rocket
    {
        // Checking if A key is pressed
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotateSpeed); // Rotating rocket to the left
        }
        // Checking if D key is pressed
        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotateSpeed); // Rotating rocket to the right
        }
    }

    void ApplyRotation(float rotation) // Rotating rocket in some direction
    {
        myRigidbody.freezeRotation = true; // Freezing rotation so we can manually rotate
        myTransform.Rotate(rotation * Time.deltaTime * Vector3.forward); // Rotating rocket
        myRigidbody.freezeRotation = false; // Unfreeze rotation
    }
}
