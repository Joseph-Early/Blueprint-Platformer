using Dialogue;
using UnityEngine;

namespace Dialogue.Scenes.Level2
{
    public class Scene1 : ScriptedScene
    {
        // Called when the scene begins
        public override void OnBegin()
        {
            // Set the dialogue lines
            DialogueSystem.Instance.AddDialogue("Now I think you're ready to solve a level on your own.");
            DialogueSystem.Instance.AddDialogue("One final thing which may help in this level, press F1 to show the mouse position. This is useful if you need to position an object to a certain point.");
            DialogueSystem.Instance.AddDialogue("A quick hint, you can use the Tick function to run code once every 60th of second. This might help if you want to move a platform a little bit each frame.");
            DialogueSystem.Instance.AddDialogue("Not sure how to do that? Press F2 to open the scripting guide for the game.");
        }

        // Called when the scene continues
        public override void OnContinue()
        {
            // Auto-finish the dialogue
            if (DialogueSystem.Instance.IsDialogueComplete())
                SetComplete();
        }

        // Called when the scene ends
        public override void OnEnd() { }
    }
}