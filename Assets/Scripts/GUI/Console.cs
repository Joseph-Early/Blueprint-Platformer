using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using TMPro;

namespace GUI
{
    public class Console : MonoBehaviour
    {
        // Singleton
        [HideInInspector] public static Console Instance;

        // Console configuration variables
        [Header("Console")]
        [SerializeField] private TextMeshProUGUI ConsoleText; // Text reference
        [SerializeField] private int MaxLines = 10; // Max lines which can be displayed before they are called
        [SerializeField] private int MaxCharactersPerLine = 50;  // Max characters per line
        [SerializeField] private float TimeUntilCleared = 5; // Time until that message is cleared
        
        // Private variables
        private Queue<string> _queue = new Queue<string>(); // Queue of messages
        private float _clearTimer = 0; // Timer until line is cleared

        // Set the singleton instance
        void Awake()
        {
            // Check if the instance is null
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        // Sta\rt is called before the first frame update
        void Start()
        {
            // Update the console text (clears it)
            UpdateConsoleText();
        }

        // Update is called once per frame
        void Update()
        {
            // If the queue is empty, return and reset the timer
            if (_queue.Count == 0)
            {
                _clearTimer = 0;
                return;
            }

            // If timer is bigger than time until cleared, dequeue the message
            if (_clearTimer > TimeUntilCleared)
            {
                // Remove message and reset timer
                _queue.Dequeue();
                _clearTimer = 0;

                // Update the console text
                UpdateConsoleText();
            }

            // Increase timer
            _clearTimer += Time.deltaTime;

            // If the queue is bigger than is allowed, dequeue the messages until it is not
            if (_queue.Count > MaxLines)
            {
                // Continuously dequeue until the queue is within the limit
                while (_queue.Count > MaxLines)
                    _queue.Dequeue();

                // Update the console text
                UpdateConsoleText();
            }
        }

        // Update the console text
        private void UpdateConsoleText()
        {
            // Create a string builder
            StringBuilder sb = new StringBuilder();

            // Loop through the queue and add each message to the string builder on a new line
            foreach (string message in _queue)
            {
                // Check if the message is bigger than allowed
                if (message.Length > MaxCharactersPerLine)
                {
                    // Only show allowed characters
                    sb.Append($"{message.Substring(0, MaxCharactersPerLine)}...");
                }
                else
                {
                    // Add message
                    sb.Append($"{message}\n");
                }
            }

            // Update the console text
            ConsoleText.text = sb.ToString();
        }

        /// <summary>
        /// Add a message to the console
        /// </summary>
        /// <param name="message">Message to add</param>
        public void AddMessage(string message)
        {
            // Enqueue message
            _queue.Enqueue(message);

            // Update console text
            UpdateConsoleText();
        }

        /// <summary>
        /// Clear the console
        /// </summary>
        public void Clear()
        {
            // Clear the queue
            _queue.Clear();

            // Update console text
            UpdateConsoleText();
        }
    }
}