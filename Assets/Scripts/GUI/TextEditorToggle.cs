using UnityEngine;
using TMPro;
using Managers;

namespace GUI
{
    public class TextEditorToggle : MonoBehaviour
    {
        [SerializeField] private GameObject textEditor, objectInformation;

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.LeftAlt))
            {
                // If dialogue is active or the game is paused, don't allow the player to toggle the text editor
                if (GameState.Instance.CurrentState == GameState.State.InDialogue || GameState.Instance.CurrentState == GameState.State.Paused)
                    return;

                var code = "";
                // If the text editor is enabled, set the code to that provided by the player
                if (textEditor.activeSelf)
                    code = GameObject.Find("MainText").GetComponent<TextMeshProUGUI>().text;

                // Toggle activeness
                textEditor.SetActive(!textEditor.activeSelf);

                // Hide the object information if the text editor is active (so the player can see the text editor)
                objectInformation.SetActive(!textEditor.activeSelf);

                // Change the state of the game
                Managers.GameState.Instance.CurrentState = textEditor.activeSelf ? Managers.GameState.State.InEditor : Managers.GameState.State.Playing;

                // Clear the console
                Console.Instance.Clear();

                // Set the lua script
                Managers.Globals.Instance.GetComponent<Lua.LuaManager>().SetScript(code);
            }
        }
    }
}