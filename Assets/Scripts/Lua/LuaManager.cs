using System;
using System.Collections.Generic;
using UnityEngine;
using MoonSharp.Interpreter;
using Lua.Modules;
using System.Collections;

namespace Lua
{
    public class LuaManager : MonoBehaviour
    {
        bool executedOnce = false;
        public string LuaText = "";
        public static string[] FunctionGlobals;

        private static int luaCallsASecond = 30;

        public static LuaControllable[] Controllers { get; private set; } = null;

        string tickFunction = "Tick";

        Script LuaScript;

        string LuaScriptPlayerCode = ""; // Store what the player wrote to execute the tick method provided, if any

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

            // Create the Lua script which is executing the code the player enters
            NewLuaScript();

            // Invoke Lua tick coroutine (yield waits everytime for 1 / luaCallsASecond (60fps))
            StartCoroutine(LuaTick());
        }

        private void NewLuaScript()
        {
            // Create a new script
            LuaScript = new Script();

            // Expose functions to Lua
            #region Expose
            LuaScript.Globals["Move"] = (Func<string, float, float, int>)Movement.SetPositionRelative;
            LuaScript.Globals["MoveAbs"] = (Func<string, float, float, int>)Movement.SetPositionAbsolute;

            // Rotation
            LuaScript.Globals["Rotate"] = (Func<string, float, int>)Rotation.SetRotationRelative;
            LuaScript.Globals["RotateAbs"] = (Func<string, float, int>)Rotation.SetRotationAbsolute;

            // Scale
            LuaScript.Globals["Scale"] = (Func<string, float, float, int>)Scale.SetScaleRelative;
            LuaScript.Globals["ScaleAbs"] = (Func<string, float, float, int>)Scale.SetScaleAbsolute;

            // Sprite module
            LuaScript.Globals["SetColour"] = (Func<string, float, float, float, float, int>)SpriteModule.SetSpriteColour;

            // Miscellaneous
            LuaScript.Globals["Print"] = (Action<string>)Miscellaneous.Print;
            #endregion
        }

        // Execute code from Stack
        void Update()
        {
            if (!executedOnce)
            {
                // Set executed once to true
                executedOnce = true;

                // Create new script
                NewLuaScript();

                // Create a copy of the code provided with a goto end which calls tick and no other code
                LuaScriptPlayerCode = @"
                -- Go to the end of the script
                goto __reserved_end__

                -- Player code
                __reserved_code__
                
                -- Call the tick function
                ::__reserved_end__::
                __reserved_tick__()

                ".Replace("__reserved_code__", LuaText).Replace("__reserved_tick__", tickFunction);

                // Execute the code and check for exceptions
                try
                {
                    LuaScript.DoString(LuaText);
                }
                catch (System.Exception)
                {
                    UnityEngine.Debug.Log("Invalid script provided");
                }
            }
        }

        // Add code
        public void SetScript(string code)
        {
            // Reset executed flag
            executedOnce = false;
            
            // Print to console
            UnityEngine.Debug.Log(code);

            // Delete the zero width space
            // Removing this code will break everything as TMPro adds this character into the text field
            code = code.Replace("\u200B", "");

            LuaText = code;
        }

        // Update loop
        private IEnumerator LuaTick()
        {
            while (true)
            {

                yield return new WaitForSeconds(1f / luaCallsASecond);

                // Try to execute script, will fail if missing tick function
                try
                {
                    LuaScript.DoString(LuaScriptPlayerCode);
                }
                catch (System.Exception)
                {
                    // UnityEngine.Debug.Log("Invalid tick code");
                }
            }
        }
    }
}