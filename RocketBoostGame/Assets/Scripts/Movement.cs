using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float pushSpeed = 500f; // Rocket speed variable
    [SerializeField] float rotateSpeed = 100f; // Rocket rotation speed variable
    [SerializeField] AudioClip rocketBoostSound; // Getting audio clip from the editor
    [SerializeField] ParticleSystem mainBoostParticles; // Getting the main boost particles from the editor
    [SerializeField] ParticleSystem rightBoostParticles; // Getting the right boost particles from the editor
    [SerializeField] ParticleSystem leftBoostParticles; // Getting the left boost particles from the editor

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
            // Checking if the main boost particles is not playing
            if (!mainBoostParticles.isPlaying)
            {
                mainBoostParticles.Play(); // Starting playing the maing boost particles, if space is pressed
            }
            // Checking if no sound is playing
            if (!myAudioSource.isPlaying)
            {
                myAudioSource.PlayOneShot(rocketBoostSound); // Starting playing rocket sound
            }
        }
        // If space is not pressed
        else
        {
            myAudioSource.Stop(); // Stopping the rocket sound, if space is not pressed
            mainBoostParticles.Stop(); // Stopping playing the main boost particles, if space is not pressed
        }
    }

    void ProcessRotation() // Rotating rocket
    {
        // Checking if A key is pressed
        if (Input.GetKey(KeyCode.A))
        {
            // Checking if the left boost particles is not playing
            if (!leftBoostParticles.isPlaying)
            {
                leftBoostParticles.Play(); // Starting playing the left boost particles
            }

            ApplyRotation(rotateSpeed); // Rotating rocket to the left
        }
        // Checking if D key is pressed
        else if (Input.GetKey(KeyCode.D))
        {
            // Checking if the right boost particles is not playing
            if (!rightBoostParticles.isPlaying)
            {
                rightBoostParticles.Play(); // Starting playing the right boost particles
            }

            ApplyRotation(-rotateSpeed); // Rotating rocket to the right
        }
        // If neither A nor D pressed
        else
        {
            leftBoostParticles.Stop(); // Stopping playing the left boost particles
            rightBoostParticles.Stop(); // Stopping playing the right boost particles
        }
    }

    void ApplyRotation(float rotation) // Rotating rocket in some direction
    {
        myRigidbody.freezeRotation = true; // Freezing rotation so we can manually rotate
        myTransform.Rotate(rotation * Time.deltaTime * Vector3.forward); // Rotating rocket
        myRigidbody.freezeRotation = false; // Unfreeze rotation
    }
}
