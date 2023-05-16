using UnityEngine;

namespace Dialogue
{
    /// <summary>
    /// This is the base class for a scripted scene which is fed to the dialogue system.
    /// This class must be inherited from to be used.
    /// API:
    ///     - OnBegin(): Called when the scene begins.
    ///     - OnContinue(): Called when the scene continues.
    ///     - OnEnd(): Called when the scene ends.
    ///     - SetComplete(): Sets the scene to complete (not the dialogue lines).
    ///     - IsComplete(): Returns whether the scene is complete.
    /// </summary>
    public abstract class ScriptedScene : MonoBehaviour
    {
        // Private variables
        private bool _isComplete;

        /// <summary>
        /// Called when the scene begins.
        /// </summary>
        public abstract void OnBegin();

        /// <summary>
        /// Called when the scene continues.
        /// </summary>
        public abstract void OnContinue();

        /// <summary>
        /// Called when the scene ends.
        /// </summary>
        public abstract void OnEnd();

        /// <summary>
        /// Sets the scene to complete (not the dialogue lines, use SetCompleteAll()).
        /// </summary>
        public void SetComplete() => _isComplete = true;

        /// <summary>
        /// Returns whether the scene is complete.
        /// </summary>
        public bool IsComplete() => _isComplete;
    }
}