using UnityEngine;

namespace Dialogue.Scenes {
    public class Scene : DialogueScene
    {
        public override void OnEnd()
        {
            
        }

        public override void OnBegin()
        {
            DialogueManager.AddDialogue("Test");
        }

        public override void OnContinue()
        {
            
        }
    }
}