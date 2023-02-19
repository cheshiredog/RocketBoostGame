using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float delayBeforeLoading = 1.0f; // Setting up the time of the delay before loading or reloading
    [SerializeField] float soundVolume = 1f; // Setting up the the sound volume
    [SerializeField] AudioClip successSound; // Getting the success sound from the editor
    [SerializeField] AudioClip crashSound; // Getting the crash sound from the editor
    [SerializeField] ParticleSystem successParticles; // Getting the success particles from the editor
    [SerializeField] ParticleSystem crashParticles; // Getting the crash particles from the editor

    AudioSource audioSource; // Cashing audio source

    bool isTransitioning = false; // Has the process of the loading or the reloading level started?
    bool collisionDisabled = false; // Is the collision enabled or disables?

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // Getting access to the audio source
    }

    // Update is called once per frame
    void Update()
    {
        RespondToDebugKeys(); // This method is responsible for debugging
    }

    // This method is responsible for debugging
    void RespondToDebugKeys()
    {
        // Checking if L is pressed
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel(); // Loading the next level
        }
        // Checking if C is pressed
        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled; // Disabling collosions
        }
    }

    // Calls when rocket bumped into something
    void OnCollisionEnter(Collision collision) 
    {
        // Checking if the process of the loading or the reloading has already started or if collision is disabled
        if (isTransitioning || collisionDisabled) 
        { 
            return; // Stop executing OnCollisionEnter (this) method
        }

        // What is the object which the rocket bumped into?
        switch (collision.gameObject.tag)
        {
            case "Friendly": // If object is friendly
                break;

            case "Finish": // If object is finish
                StartLevelEnding(nameof(LoadNextLevel), successSound, successParticles); // Loading the next level
                break;

            default: // If object is obstacle
                StartLevelEnding(nameof(LevelReload), crashSound, crashParticles); // The player lost so we reload this level
                break;
        }
    }

    // Starting the end of the level process
    void StartLevelEnding(string loadOrReload, AudioClip crashOrSuccessSound, ParticleSystem particles) 
    {
        isTransitioning = true; // Marking that the process of the loading or the reloading has already started
        audioSource.Stop(); // Turning off the rocket boost sound
        particles.Play(); // Starting playing some particles
        audioSource.PlayOneShot(crashOrSuccessSound, soundVolume); // Playing the success or the crash sound
        gameObject.GetComponent<Movement>().enabled = false; // Turning off the rocket control
        Invoke(loadOrReload, delayBeforeLoading); // Reloading this level or loading the next level within a delay
    }

    // This method loads the next level
    void LoadNextLevel() 
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; // Getting the index of the current level
        int nextSceneIndex = currentSceneIndex + 1; // Getting the index of the next level

        // Checking if this level is the last one (checking if the index is bigger than the last level index)
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0; // Returning to the first level
        }

        SceneManager.LoadScene(nextSceneIndex); // Loading the next level
    }

    // This method reloads the current level
    void LevelReload()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; // Getting the index of the current level
        SceneManager.LoadScene(currentSceneIndex); // Loading the current level
    }
}
