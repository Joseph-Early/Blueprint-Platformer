using UnityEngine;

namespace Managers
{
    /// <summary>
    /// Manages the state of the game
    /// </summary>
    public class GameState : MonoBehaviour
    {
        /// <summary>
        /// The current state of the game
        /// </summary>
        public State CurrentState { get; set; } = State.Playing;

        // Singleton
        public static GameState Instance;

        // Set the singleton instance
        void Awake()
        {
            // Check if the instance is null
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        /// <summary>
        /// The states of the game
        /// </summary>
        public enum State
        {
            Playing,
            InDialogue,
            InEditor,
            Paused
        }
    }
}