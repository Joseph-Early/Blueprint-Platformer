using UnityEngine;
using Managers;

namespace Dialogue {
    /// <summary>
    /// Provides the base class for a scene for the dialogue manager
    /// It provides a number of methods for events which are invoked and overridable
    /// </summary>
    public abstract class DialogueScene : MonoBehaviour
    {
        // Store a reference to the dialogue system
        public static DialogueSystem DialogueManager {get; private set;} = null;
        
        private void Start() => DialogueManager = Globals.Instance.Dialogue;

        // Overridable event methods
        public abstract void OnBegin();
        public abstract void OnContinue();
        public abstract void OnEnd();
    }
}