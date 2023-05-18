using Dialogue;
using UnityEngine;

namespace Dialogue.Scenes.Level1
{
    public class Scene1 : ScriptedScene
    {
        // Called when the scene begins
        public override void OnBegin()
        {
            // Set the dialogue lines
            DialogueSystem.Instance.AddDialogue("Tutorial time!");
            DialogueSystem.Instance.AddDialogue("Press A or D to move left or right.");
        }

        // Called when the scene continues
        public override void OnContinue()
        {
            // Check messages are complete and player moved
            if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D)) && DialogueSystem.Instance.IsDialogueComplete())
                Invoke("NextScene", 0.5f); // Wait 0.5 seconds before moving to the next scene
        }

        private void NextScene() => SetComplete();

        // Called when the scene ends
        public override void OnEnd() { }
    }
}