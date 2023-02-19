using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPosition;
    float movementFactor;
    [SerializeField] Vector3 movementVector; // The destination of the object
    [SerializeField] float period = 2f; // The speed of a movement

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position; // Getting the starting position of this object
    }

    // Update is called once per frame
    void Update()
    {
        // Checking if the period is not equal to 0
        if (period <= Mathf.Epsilon) { return; }

        float cycles = Time.time / period; // Calculating the speed of a movement
        const float tau = Mathf.PI * 2; // Setting up the tau value (around 6.28)
        float rawSinWave = Mathf.Sin(cycles * tau); // Getting the number between -1 and 1
        movementFactor = (rawSinWave + 1f) / 2; // Recalculating -1 to 1 into 0 to 1 so it's cleaner
        Vector3 offset = movementVector * movementFactor; // Calculating the step to next position
        transform.position = startingPosition + offset; // Moving the obstacle to the new position
    }
}
