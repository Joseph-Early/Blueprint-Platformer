using Dialogue;

public class DSceneTest2 : ScriptedScene
{
    // Called when the scene begins
    public override void OnBegin() {
        // Set the dialogue lines
        DialogueSystem.Instance.AddDialogue("You did it!");
        UnityEngine.Debug.Log("DSceneTest2: OnBegin()");
    }

    // Called when the scene continues
    public override void OnContinue() {}

    // Called when the scene ends
    public override void OnEnd() {}
}