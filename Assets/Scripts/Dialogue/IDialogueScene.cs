using UnityEngine;

namespace Dialogue {
    /// <summary>
    /// IDialogueScene defines an interface for each event required for a scripted scene in the dialogue system.
    /// </summary>
    interface IDialogueScene
    {
        /// <summary>
        /// Invoked once the scene begins
        /// </summary>
        void OnBegin();
        /// <summary>
        /// Invoked continually until the scene ends
        /// </summary>
        void OnContinue();
        /// <summary>
        /// Invoked as the scene ends
        /// </summary>
        void OnEnd();
    }
}