using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

namespace GUI
{
    public class PauseMenu : MonoBehaviour
    {
        // Reference to the pause menu canvas to enable/disable
        [SerializeField] private GameObject _pauseMenuCanvas;

        // Track if the game is paused
        private bool _isPaused = false;

        // Method to resume the game
        public void ResumeGame() => TogglePauseMenu();

        // Method to load the level select
        public void LevelSelect() => SceneManager.LoadScene("MainMenu");

        // Method to restart the current level
        public void RestartLevel() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        // Update is called once per frame
        private void Update()
        {
            // If escape is pressed, toggle the pause menu
            if (Input.GetKeyDown(KeyCode.Escape))
                TogglePauseMenu();
        }
        

        // Toggle the pause menu and time scale
        private void TogglePauseMenu()
        {
            // Toggle the pause menu
            _pauseMenuCanvas.SetActive(!_pauseMenuCanvas.activeSelf);

            // Toggle the time scale
            if (_isPaused)
                Time.timeScale = 1;
            else
                Time.timeScale = 0;

            // Toggle the paused state
            _isPaused = !_isPaused;
        }
    }
}