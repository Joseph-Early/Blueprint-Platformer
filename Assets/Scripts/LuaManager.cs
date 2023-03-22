using System.Numerics;
using System;
using System.Collections.Generic;
using UnityEngine;
using MoonSharp.Interpreter;

public class LuaManager : MonoBehaviour
{
    public Stack<string> LuaScripts;

    private void Awake() => LuaScripts = new Stack<string>();

    private static LuaControllable[] controllers;

    // Get a reference to all the LuaControllable's
    private void Start()
    {
        // Get a reference to all the gameobjects with LuaControllable tag
        GameObject[] objs = GameObject.FindGameObjectsWithTag("LuaControllable");

        // Temporary list to controllers so can be converted to a more performant array
        var controllersList = new List<LuaControllable>();

        // Find the LuaControllable for each controller object
        foreach (var controller in objs) controllersList.Add(controller.GetComponent<LuaControllable>());

        // Set the controllers to controllersList
        controllers = controllersList.ToArray();
    }


    // Execute code from Stack
    void Update()
    {
        if (LuaScripts.Count != 0)
        {
            // Read the top value from the stack
            var scriptCode = LuaScripts.Pop();

            // Add globals (Exposing properties and methods)
            Script script = new Script();
            script.Globals["Move"] = (Func<string, float, float, int>)Move;
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
    private static int Move(string controllerName, float x, float y)
    {
        var controller = ReturnLuaObject(controllerName);

        if (controller.controller != null)
        {
            // Get the transform
            var trans = controller.obj.GetComponent<Transform>();

            // Calculate the new position
            var pos = new UnityEngine.Vector2(trans.position.x + x, trans.position.y + y);

            // Set the transform
            trans.position = pos;

            // Return 0 - success
            return 0;
        }

        // Return 1 - error
        return 1;
    }
    private static void Print(string message)
    {
        UnityEngine.Debug.Log(message);
    }
    #endregion

    // Find LuaController by name
    private static (GameObject obj, LuaControllable controller) ReturnLuaObject(string controllerName)
    {
        // Iterate through all LuaControllers
        foreach (LuaControllable controller in controllers)
        {
            // Check if name matches identifier name
            if (controller.IdentifierInLevel == controllerName)
            {
                // Return the gameobject and controller
                return (controller.gameObject, controller);
            }
        }

        // Return null - failed to get controller and gameobject of matching name
        return (null, null);
    }

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
