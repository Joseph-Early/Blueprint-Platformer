using System.Globalization;
using System;
using System.Collections.Generic;
using UnityEngine;
using MoonSharp.Interpreter;
using Lua.Modules;

namespace Lua
{
    public class LuaManager : MonoBehaviour
    {
        public Stack<string> LuaScripts;
        public static string[] FunctionGlobals;

        private void Awake() => LuaScripts = new Stack<string>();

        public static LuaControllable[] Controllers { get; private set; } = null;

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
            Controllers = controllersList.ToArray();

            
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

                // Translation
                #region Expose
                script.Globals["Move"] = (Func<string, float, float, int>)Movement.SetPositionRelative;
                script.Globals["MoveAbs"] = (Func<string, float, float, int>)Movement.SetPositionAbsolute;

                // Rotation
                script.Globals["Rotate"] = (Func<string, float, int>)Rotation.SetRotationRelative;
                script.Globals["RotateAbs"] = (Func<string, float, int>)Rotation.SetRotationAbsolute;

                // Scale
                script.Globals["Scale"] = (Func<string, float, float, int>)Scale.SetScaleRelative;
                script.Globals["ScaleAbs"] = (Func<string, float, float, int>)Scale.SetScaleAbsolute;

                // Miscellaneous
                script.Globals["Print"] = (Action<string>)Miscellaneous.Print;
                #endregion

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

        // Add code
        public void AddScript()
        {
            var text = GameObject.Find("MainText").GetComponent<TMPro.TextMeshProUGUI>().text;

            // Print to console
            UnityEngine.Debug.Log(text);

            // Delete the zero width space
            // Removing this code will break everything as TMPro adds this character into the text field
            text = text.Replace("\u200B", "");

            LuaScripts.Push(text);
        }
    }
}