using UnityEngine;

namespace Dialogue.Scenes {
    public class Scene : DialogueScene
    {
        public override void OnEnd()
        {
            DialogueManager.SetDialogueActive(false);
        }

        public override void OnBegin()
        {
            DialogueManager.AddDialogue("Test");
            DialogueManager.AddDialogue("This is using the new scripted scenes");
        }

        public override void OnContinue()
        {
            
        }
    }
}