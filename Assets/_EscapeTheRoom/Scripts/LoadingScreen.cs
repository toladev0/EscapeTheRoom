using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

// This script shows a loading screen before switching to a new scene
public class LoadingScreen : MonoBehaviour
{
    public GameObject loadingPanel;  // The UI panel that shows the loading screen

    // Call this method to load a scene by its build index (sceneId)
    public void LoadScene(int sceneId)
    {
        StartCoroutine(LoadSceneAsync(sceneId));  // Start loading the scene 
    }

    // Coroutine that loads the scene with a short delay
    IEnumerator LoadSceneAsync(int sceneId)
    {
        // Begin loading the scene in the background
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);
        operation.allowSceneActivation = false;   // Wait before switching to the scene

        loadingPanel.SetActive(true);             // Show the loading panel

        yield return new WaitForSeconds(1f);      // Wait for 1 second (e.g., to display animation)

        operation.allowSceneActivation = true;    // Allow the scene to finish loading and activate
    }
}
