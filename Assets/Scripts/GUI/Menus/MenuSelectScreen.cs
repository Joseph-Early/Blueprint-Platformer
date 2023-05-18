using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

namespace GUI.Menus
{
    public class MenuSelectScreen : MonoBehaviour
    {
        // Load scene methods used by buttons
        public void LoadTutorial1() => SceneManager.LoadScene("Tutorial1");
    }
}