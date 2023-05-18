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
            DialogueSystem.Instance.AddDialogue("We could wait around for it be implemented or open the game's code and scale up the platform so we can walk across it.");
            DialogueSystem.Instance.AddDialogue("Hover over green platform and look at the popup on left. The name written inside quotes is the name of the object. We will need this so remember it.");
            DialogueSystem.Instance.AddDialogue("The information in the popup below are functions we can use for that object. If you look at the function 'ScaleAbs', you can see it is enabled. We can use this to scale the platform.");
            DialogueSystem.Instance.AddDialogue("A size of 12 on the x axis should be enough. ScaleAbs takes a few parameters, the first is the object name, the second is the x scale, the third is the y scale.");
            DialogueSystem.Instance.AddDialogue("All code you write will be written in Lua, an industry standard scripting language. But it's simple to learn!");
            DialogueSystem.Instance.AddDialogue("To write code, press alt to open the text editor. Type in the following code:");
            DialogueSystem.Instance.AddDialogue("ScaleAbs(\"OBJECT_NAME_HERE\", SIZE_X_HERE, SIZE_Y_HERE)");
            DialogueSystem.Instance.AddDialogue("Replace OBJECT_NAME_HERE with the name of the platform and SIZE_X_HERE and SIZE_Y_HERE with the size you want the platform to be.");
            DialogueSystem.Instance.AddDialogue("Once you have written the code, press alt again to close the text editor and the code will automatically run.");
            DialogueSystem.Instance.AddDialogue("If you make an error, the game will say you have an error but you can press alt again and find the error in the text editor and fix it.");
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