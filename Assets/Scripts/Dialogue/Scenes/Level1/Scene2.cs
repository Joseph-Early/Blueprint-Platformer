using Dialogue;
using UnityEngine;

namespace Dialogue.Scenes.Level1
{
    public class Scene2 : ScriptedScene
    {
        // Called when the scene begins
        public override void OnBegin()
        {
            DialogueSystem.Instance.AddDialogue("Now explore the level to find the goal.");
        }

        // Called when the scene continues
        public override void OnContinue()
        {
            // If triggered, advance
            if (DialogueSystem.Instance.Triggered)
                SetComplete();
        }

        // Called when the scene ends
        public override void OnEnd()
        {

        }
    }
}