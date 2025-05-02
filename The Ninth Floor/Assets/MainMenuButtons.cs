using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    public void startButton()
    { SceneManager.LoadScene("SampleScene"); }

    public void quitButton()
    { UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
