using UnityEngine;

namespace Managers
{
    /// <summary>
    /// Handles saving and loading of the current level
    /// </summary>
    public class SaveSystem : MonoBehaviour
    {
        /// <summary>
        /// The current level the player is on
        /// First two scenes are the title and menu so zero should denote tutorial 1
        /// </summary>
        public static int currentLevel {get; private set; } = 0;

        /// <summary>
        /// Save the current level to the player prefs
        /// </summary>
        public static void SaveLevel() => PlayerPrefs.SetInt("CurrentLevel", currentLevel);

        /// <summary>
        /// Load the current level from the player prefs (if it exists)
        /// </summary>
        public static void LoadLevel() => currentLevel = PlayerPrefs.GetInt("CurrentLevel", 0);

        /// <summary>
        /// Advance the current level
        /// </summary>
        public static void AdvanceLevel() => currentLevel++;
    }
}