using Dialogue;
using UnityEngine;

namespace Dialogue.Scenes.Level1
{
    public class Scene4 : ScriptedScene
    {
        // Called when the scene begins
        public override void OnBegin()
        {
            // Set the dialogue lines
            DialogueSystem.Instance.AddDialogue("Odd seems the jump feature isn't implemented yet...");
            DialogueSystem.Instance.AddDialogue("We could wait around for it be implemented or open the game's code and get one of the platforms to move to us.");
            DialogueSystem.Instance.AddDialogue("Hover over green platform and remember its name.");
            DialogueSystem.Instance.AddDialogue("Now hit alt to open the text editor.");
        }

        // Called when the scene continues
        public override void OnContinue()
        {
            // Check messages are complete and player moved
            if (Input.GetKeyDown(KeyCode.LeftAlt) && DialogueSystem.Instance.IsDialogueComplete())
                SetComplete();
        }

        // Called when the scene ends
        public override void OnEnd() { }
    }
}