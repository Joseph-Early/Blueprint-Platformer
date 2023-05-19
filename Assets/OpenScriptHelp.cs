using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GUI
{
    public class OpenScriptHelp : MonoBehaviour
    {
        // Update is called once per frame
        void Update()
        {
            // Open Lua guide in browser
            if (Input.GetKeyDown(KeyCode.F2))
            {
                // Open link in browser
                Application.OpenURL("https://raw.githubusercontent.com/Joseph-Early/Blueprint-Platformer/main/Scripting%20Guide.pdf");
            }
        }
    }

}