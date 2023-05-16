using Dialogue;
using UnityEngine;

public class DSceneTest1 : ScriptedScene
{
    // Called when the scene begins
    public override void OnBegin() {
        // Set the dialogue lines
        DialogueSystem.Instance.AddDialogue("Hello, world!");
        DialogueSystem.Instance.AddDialogue("This is a test scene.");
        DialogueSystem.Instance.AddDialogue("This is the first scene.");
        DialogueSystem.Instance.AddDialogue("You need to press the spacebar to continue.");
        DialogueSystem.Instance.AddDialogue("Press the spacebar to continue.");

        UnityEngine.Debug.Log("DSceneTest1: OnBegin()");
    }

    // Called when the scene continues
    public override void OnContinue() {
        if (Input.GetKeyDown(KeyCode.Space) && DialogueSystem.Instance.IsDialogueComplete())
            SetComplete();

        UnityEngine.Debug.Log("DSceneTest1: OnContinue()");
    }

    // Called when the scene ends
    public override void OnEnd() {

        UnityEngine.Debug.Log("DSceneTest1: OnEnd()");
    }
}