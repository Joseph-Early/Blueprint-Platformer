using UnityEngine;

namespace Managers
{
    public class TutorialManager : MonoBehaviour
    {
        private TutorialPoint tutorial = TutorialPoint.LevelStart;
        private bool InitialisationOfTutorialStateHasRun = false;

        private void Start()
        {
            // 
        }

        private void Update()
        {
            // If entering a new state
            if (!InitialisationOfTutorialStateHasRun)
            {
                switch (tutorial)
                {
                    case TutorialPoint.LevelStart:
                        // After dialogue system is hidden, advance to ready to jump
                        if (Globals.Instance.Dialogue)
                            tutorial = TutorialPoint.ReadyToJump;
                            InitialisationOfTutorialStateHasRun = false;
                        break;

                    case TutorialPoint.ReadyToJump:
                        // Wait until player tries to jump with space and then advance state
                        print("Here");
                        if (Input.GetKeyDown(KeyCode.Space))
                        {
                            tutorial = TutorialPoint.PlayerTriedToJump;
                            InitialisationOfTutorialStateHasRun = false;
                }
                    break;
                    case TutorialPoint.PlayerTriedToJump:
                        Globals.Instance.Dialogue.AddDialogue("Odd seems like jump hasn't been implemented yet...");
                        Globals.Instance.Dialogue.AddDialogue("We can wait around for the developer to implement the ability to jump but that could take a while or we could extend the game ourselves");
                        Globals.Instance.Dialogue.SetDialogueActive(true);
                    break;
                    case TutorialPoint.PlayerCannotReachTheOtherSide: break;
                    case TutorialPoint.InTheCodeEditor: break;
                    case TutorialPoint.PlayerReachesOtherSide: break;
                }
            }
        }
    }

    enum TutorialPoint
    {
        LevelStart,  // Initial dialogue
        ReadyToJump, // When waiting for the player to jump
        PlayerTriedToJump, // Player jump not implemented
        PlayerCannotReachTheOtherSide, // Tell the player to open the code editor
        InTheCodeEditor, // Tell the player what to do and how.
        PlayerReachesOtherSide, // Player has crossed the platform
    }
}