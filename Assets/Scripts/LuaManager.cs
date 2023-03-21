using System.Numerics;
using System;
using System.Collections.Generic;
using UnityEngine;
using MoonSharp.Interpreter;

public class LuaManager : MonoBehaviour
{
    public Stack<string> LuaScripts;

    private void Awake() => LuaScripts = new Stack<string>();


    // Execute code from Stack
    void Update()
    {
        if (LuaScripts.Count != 0)
        {
            // Read the top value from the stack
            var scriptCode = LuaScripts.Pop();

            // Add globals (Exposing properties and methods)
            Script script = new Script();
            script.Globals["Move"] = (Func<float, float, int>)Move;
            script.Globals["Print"] = (Action<string>)Print;

            // Execute the code and check for exceptions
            try
            {
                script.DoString(scriptCode);
            }
            catch (ScriptRuntimeException e)
            {
                UnityEngine.Debug.LogWarning($"User script execution exception: {e.DecoratedMessage}");
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogError($"Error caught {e}");
            }
        }
    }


    #region Exposed functions
    private static int Move(float x, float y)
    {
        var obj = "Platform";
        var trans = GameObject.Find(obj).GetComponent<Transform>();

        var pos = new UnityEngine.Vector2(trans.position.x + x, trans.position.y + y);

        trans.position = pos;

        return 0;
    }
    private static void Print(string message)
    {
        UnityEngine.Debug.Log(message);
    }
    #endregion

    // Add code
    public void AddScript()
    {
        var text = GameObject.Find("TextScript").GetComponent<TMPro.TextMeshProUGUI>().text;

        #region Debug
        List<char> charList = new List<char>(text);
        for (int i = 0; i < charList.Count; i++)
        {
            print(charList[i]);
        }

        print(charList.Count);
        print(text.Length);
        #endregion

        // Delete the zero width space
        // Removing this code will break everything as TMPro adds this character into the text field
        text = text.Replace("\u200B", "");

        LuaScripts.Push(text);
    }
}
