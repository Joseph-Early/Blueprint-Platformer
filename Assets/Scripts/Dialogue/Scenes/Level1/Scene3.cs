using Dialogue;
using UnityEngine;

namespace Dialogue.Scenes.Level1
{
    public class Scene3 : ScriptedScene
    {
        // Called when the scene begins
        public override void OnBegin()
        {
            // Set the dialogue lines
            DialogueSystem.Instance.AddDialogue("Try jumping to get across that gap by hitting the space key");
        }

        // Called when the scene continues
        public override void OnContinue()
        {
            // Check messages are complete and player moved
            if (Input.GetKeyDown(KeyCode.Space) && DialogueSystem.Instance.IsDialogueComplete())
                SetComplete();
        }

        // Called when the scene ends
        public override void OnEnd() { }
    }
}