using UnityEngine;
using UnityEngine.SceneManagement;

public class CollosionHandler : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision) // Calls when rocket bumped into something
    {
        // What is the object which the rocket bumped into?
        switch (collision.gameObject.tag)
        {
            case "Friendly": // If object is friendly
                Debug.Log("Friendly");
                break;

            case "Finish": // If object is finish
                Debug.Log("Finish");
                break;

            default: // If object is obstacle
                SceneReload(); // The player lost so we reload this level
                break;
        }
    }

    void SceneReload() // This method reloads the current level
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; // Getting the index of the current scene
        SceneManager.LoadScene(currentSceneIndex); // Loading the current scene
    }
}
