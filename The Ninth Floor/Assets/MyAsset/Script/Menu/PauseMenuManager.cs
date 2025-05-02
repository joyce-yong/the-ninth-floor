using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem; // Required for PlayerInput

namespace StarterAssets
{
    public class PauseMenuManager : MonoBehaviour
    {
        public static bool GameIsPaused = false;
        public GameObject PauseMenuUI;

        private InputManager inputManager;

        private void Awake()
        {
            // Find and reference the InputManager
            inputManager = FindFirstObjectByType<InputManager>();

            if (inputManager == null)
            {
                Debug.LogError("InputManager not found in the scene!");
            }
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (GameIsPaused == true)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
        }

        public void Resume()
        {
            PauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            AudioListener.pause = false;
            
            inputManager.ToggleActionMap(inputManager._input.Player);
            
            GameIsPaused = false;
        }

        void Pause()
        {

            PauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            AudioListener.pause = true;
            
            inputManager.ToggleActionMap(inputManager._input.UI);

            GameIsPaused = true;
        }

        public void LoadMenu()
        {
            Debug.Log("Load game");
        }

        public void QuitGame()
        {
            Debug.Log("Quit game");
        }
    }
}
