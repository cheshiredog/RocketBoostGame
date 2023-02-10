using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float delayBeforeLoading = 1.0f; // Setting up the time of the delay before loading or reloading

    private void OnCollisionEnter(Collision collision) // Calls when rocket bumped into something
    {
        // What is the object which the rocket bumped into?
        switch (collision.gameObject.tag)
        {
            case "Friendly": // If object is friendly
                Debug.Log("Friendly");
                break;

            case "Finish": // If object is finish
                StartLevelEnding(nameof(LoadNextLevel)); // Loading the next level
                break;

            default: // If object is obstacle
                StartLevelEnding(nameof(LevelReload)); // The player lost so we reload this level
                break;
        }
    }

    void StartLevelEnding(string loadOrReload) // Starting the end of the level process
    {
        gameObject.GetComponent<AudioSource>().Stop(); // Turning off the rocket boost sound
        gameObject.GetComponent<Movement>().enabled = false; // Turning off the rocket control
        Invoke(loadOrReload, delayBeforeLoading); // Reloading this level or loading the next level within a delay
    }

    void LoadNextLevel() // This method loads the next level
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

    void LevelReload() // This method reloads the current level
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; // Getting the index of the current level
        SceneManager.LoadScene(currentSceneIndex); // Loading the current level
    }
}
