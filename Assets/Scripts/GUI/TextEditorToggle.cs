using UnityEngine;
using TMPro;

namespace GUI
{
    public class TextEditorToggle : MonoBehaviour
    {
        [SerializeField] GameObject textEditor;

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.LeftAlt))
            {
                var code = "";
                // If the text editor is enabled, set the code to that provided by the player
                if (textEditor.activeSelf)
                    code = GameObject.Find("MainText").GetComponent<TextMeshProUGUI>().text;

                // Toggle activeness
                textEditor.SetActive(!textEditor.activeSelf);

                // Set the lua script
                Managers.Globals.Instance.GetComponent<Lua.LuaManager>().SetScript(code);
            }
        }
    }
}