using System.Numerics;
using System;
using System.Collections.Generic;
using UnityEngine;
using MoonSharp.Interpreter;
using UnityEngine.UI;

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

            script.DoString(scriptCode);
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
    #endregion

    // Add code
    public void AddScript()
    {
        var text = GameObject.Find("TextScript").GetComponent<Text>().text;
        LuaScripts.Push(text);
    }
}
