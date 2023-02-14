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

    // Pushing rocket
    void ProcessThrust() 
    {
        // Checking if space is pressed
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting(); // Applying thrust
        }
        // If space is not pressed
        else
        {
            StopThrustingAndPlayingAudio(); // Stopping thrusting and playing boost audio
        }
    }

    // Applying thrust
    void StartThrusting() 
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

    // Stopping thrusting and playing boost audio
    void StopThrustingAndPlayingAudio() 
    {
        myAudioSource.Stop(); // Stopping the rocket sound, if space is not pressed
        mainBoostParticles.Stop(); // Stopping playing the main boost particles, if space is not pressed
    }

    // Rotating rocket
    void ProcessRotation() 
    {
        // Checking if A key is pressed
        if (Input.GetKey(KeyCode.A))
        {
            ApplyingSideBoostParticles(leftBoostParticles); // Starting playing left boost particles
            ApplyRotation(rotateSpeed); // Rotating rocket to the left
        }
        // Checking if D key is pressed
        else if (Input.GetKey(KeyCode.D)) 
        {
            ApplyingSideBoostParticles(rightBoostParticles); // Starting playing right boost particles
            ApplyRotation(-rotateSpeed); // Rotating rocket to the right
        }
        // If neither A nor D pressed
        else
        {
            StopAnySideBoostParticles(); // Stopping the right and the left boost particles effects
        }
    }

    // Starting playing side boost particles
    void ApplyingSideBoostParticles(ParticleSystem sideBoostParticles) 
    {
        // Checking if the side boost particles is not playing
        if (!sideBoostParticles.isPlaying)
        {
            sideBoostParticles.Play(); // Starting playing the side boost particles
        }
    }

    // Rotating rocket in some direction
    void ApplyRotation(float rotation) 
    {
        myRigidbody.freezeRotation = true; // Freezing rotation so we can manually rotate
        myTransform.Rotate(rotation * Time.deltaTime * Vector3.forward); // Rotating rocket
        myRigidbody.freezeRotation = false; // Unfreeze rotation
    }

    // Stopping the right and the left boost particles effects
    void StopAnySideBoostParticles() 
    {
        leftBoostParticles.Stop(); // Stopping playing the left boost particles
        rightBoostParticles.Stop(); // Stopping playing the right boost particles
    }
}
