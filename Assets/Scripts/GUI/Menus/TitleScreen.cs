using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

namespace GUI.Menus
{
    public class TitleScreen : MonoBehaviour
    {
        // Update the version text with the current build version
        [SerializeField] private TextMeshProUGUI _versionText;

        private void Start() => _versionText.text = $"Version: {Application.version}";

        // Load the main menu if screen clicked or any key pressed
        private void Update()
        {
            if (Input.anyKeyDown || Input.GetMouseButtonDown(0))
                SceneManager.LoadScene("MainMenu");
        }
    }
}