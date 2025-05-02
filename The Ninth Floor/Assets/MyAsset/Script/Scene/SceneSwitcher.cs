using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneSwitcher : MonoBehaviour
{
    [SerializeField] private string loadScene;
    [SerializeField] private GameObject whiteScreen;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(LoadSceneWithWhiteScreen());
        }
    }

    private IEnumerator LoadSceneWithWhiteScreen()
    {
        // Show the white screen instantly
        whiteScreen.SetActive(true);

        // Start loading the scene asynchronously
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(loadScene);
        asyncLoad.allowSceneActivation = false; // Prevent the scene from activating immediately

        // Wait for 1 second (optional for any effect or transition)
        yield return new WaitForSeconds(2f);

        // Activate the scene after the wait
        asyncLoad.allowSceneActivation = true;
    }
}
