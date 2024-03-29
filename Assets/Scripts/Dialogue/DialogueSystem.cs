using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Dialogue
{
    /// <summary>
    /// C# controlled dialogue system for Unity.
    /// </summary>
    public class DialogueSystem : MonoBehaviour
    {
        // Component references
        [Header("Component references")]
        [SerializeField] private GameObject _dialogueBox;
        [SerializeField] private TextMeshProUGUI _dialogueText;

        // Configuration variables
        [Header("Configuration variables")]
        [SerializeField] private float _dialogueSpeed = 0.05f;
        [SerializeField] private ScriptedScene[] _scenes;

        // Public properties
        public bool IsDialogueActive { get => _isDialogueActive; }
        public static DialogueSystem Instance { get; private set; } = null;
        [SerializeField] public bool ActiveOnStart = false;

        [HideInInspector] public bool Triggered = false;

        // Private variables
        private Queue<string> _dialogueLines = new Queue<string>();
        private int _currentDialogueLine;
        private int _currentPlaceInDialogueLine;
        private bool _isDialogueActive;
        private int _currentScene;
        private SceneStates _sceneState;
        private float _dialogueTimer;

        // Set the instance to this object
        private void Awake()
        {
            if (Instance != null)
                Destroy(this);

            Instance = this;
        }

        // If no dialogue scenes are provided, throw an error
        private void Start()
        {
            if (_scenes.Length == 0)
                UnityEngine.Debug.LogError("No dialogue scenes provided!");

            // Set the current scene to the first scene
            _currentScene = 0;

            // If the dialogue is active on start, set the dialogue to active
            if (ActiveOnStart)
                _isDialogueActive = true;

            // Set the text to blank to avoid prevent text equality errors when determining if complete
            _dialogueText.text = "";
        }

        // Update the dialogue system
        private void Update()
        {
            // Game state update
            if (Managers.GameState.Instance.CurrentState == Managers.GameState.State.InDialogue || Managers.GameState.Instance.CurrentState == Managers.GameState.State.Playing)
            {
                Managers.GameState.Instance.CurrentState = _dialogueBox.activeSelf ? Managers.GameState.State.InDialogue : Managers.GameState.State.Playing;
            }

            // Run methods for the scene state
            if (_scenes.Length > 0 && _scenes.Length > _currentScene)
                switch (_sceneState)
                {
                    case SceneStates.Begin:
                        BeginScene();

                        break;
                    case SceneStates.Continue:
                        ContinueScene();
                        break;
                    case SceneStates.End:
                        EndScene();
                        break;
                }

            // If the dialogue is active, update the dialogue
            if (_isDialogueActive)
                UpdateDialogue();
        }

        // Update the dialogue
        private void UpdateDialogue()
        {
            // If the dialogue is complete, return
            _dialogueBox.SetActive(!IsDialogueComplete()); // Workaround for dialogue box not hiding when dialogue is complete
            if (IsDialogueComplete())
                return;

            // Update the dialogue text
            UpdateDialogueText();
        }

        #region Scene event methods
        // Begin the scene
        private void BeginScene()
        {
            // Invoke the OnBegin() method of the current scene
            _scenes[_currentScene].OnBegin();

            // Advance the scene state
            _sceneState = SceneStates.Continue;
        }

        // Continue the scene
        private void ContinueScene()
        {
            // Invoke the OnContinue() method of the current scene
            _scenes[_currentScene].OnContinue();

            // If the current scene is complete, advance the scene state
            if (_scenes[_currentScene].IsComplete())
                _sceneState = SceneStates.End;
        }

        // End the scene
        private void EndScene()
        {
            // Get the name of the current scene
            UnityEngine.Debug.Log($"Scene: {_scenes[_currentScene].GetType().Name}");

            // Invoke the OnEnd() method of the current scene
            _scenes[_currentScene].OnEnd();

            // Advance the current scene
            _currentScene++;

            // If the current scene is the last scene, set the dialogue to inactive
            if (_currentScene == _scenes.Length)
                _isDialogueActive = false;

            // Advance the scene state
            _sceneState = SceneStates.Begin;

            // Reset the dialogue text
            _dialogueText.text = "";

            // Reset the dialogue lines
            _dialogueLines.Clear();

            // Reset the current dialogue line
            _currentDialogueLine = 0;

            // Reset the current place in the dialogue line
            _currentPlaceInDialogueLine = 0;

            // Reset the dialogue timer
            _dialogueTimer = 0;
        }
        #endregion

        // Update the dialogue text
        private void UpdateDialogueText()
        {
            // Check if the dialogue timer is greater than the dialogue speed
            if (_dialogueTimer > _dialogueSpeed)
            {                
                // Reset the dialogue timer
                _dialogueTimer = 0;

                // Check if the message is complete
                if (_dialogueText.text != _dialogueLines.Peek())
                {
                    // Add the next character
                    _dialogueText.text += _dialogueLines.Peek()[_currentPlaceInDialogueLine];

                    // Increase the current place in the dialogue line
                    _currentPlaceInDialogueLine++;
                }
            }
            else
            {
                // Increment the dialogue timer
                _dialogueTimer += Time.deltaTime;
            }

            // Check if the dialogue is complete
            if (_dialogueText.text == _dialogueLines.Peek())
            {
                // Check if the player has pressed the spacebar, return, mouse button, or gamepad button
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0) || Input.GetButtonDown("Submit"))
                {
                    // Advance the dialogue line
                    AdvanceDialogueLine();
                }
            }
        }

        // Advance the dialogue line
        private void AdvanceDialogueLine()
        {
            // Remove the current dialogue line from the queue
            _dialogueLines.Dequeue();

            // Reset the current place in the dialogue line
            _currentPlaceInDialogueLine = 0;

            // Check if the dialogue queue is empty
            if (_dialogueLines.Count == 0)
            {
                // Reset the dialogue text
                _dialogueText.text = "";
            }
            else
            {
                // Increment the current dialogue line
                _currentDialogueLine++;

                // Reset the dialogue text
                _dialogueText.text = "";

                // Set the current place in the dialogue line to 0
                _currentPlaceInDialogueLine = 0;
            }
        }

        #region Public methods
        /// <summary>
        /// Add a line of dialogue to the queue.
        /// </summary>
        /// <param name="dialogue">The dialogue to add to the queue.</param>
        public void AddDialogue(string dialogue) => _dialogueLines.Enqueue(dialogue);

        /// <summary>
        /// Check if dialogue is complete.
        /// </summary>
        /// <returns>True if the dialogue is complete, false if not.</returns>
        public bool IsDialogueComplete() => _dialogueLines.Count == 0;
        #endregion
    }
}