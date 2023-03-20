using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoonSharp.Interpreter;
using System;

namespace Lua
{
    public class LuaInteraction : MonoBehaviour
    {
        State state = State.ExecutionNone;
        Transform trans;
        Script script = new Script();

        string TemplateScript = @"
        function Tick()
            // Any code here is called every frame of the game while the script is executing
        end

        function ExecutionStart()
            // Any code here is called only when the script is first executed
        end

        function Reset()
            // Called when Terminate(true) is executed to reset all properties
        end
        ";

        // Reference to transform
        private void Awake() => trans = GetComponent<Transform>();

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (state == State.ExecutionNone) return;

            // Do script
            script.DoString(TemplateScript);

            if (state == State.ExecutionStart)
                script.Call(script.Globals["ExecutionStart"]);
            else
                script.Call(script.Globals["Tick"]);
        }

        internal void Expose(bool ExposeMovement, bool ExposeEnabled, bool ExposeColours, bool ExposeRotation, bool ExposeScale, bool ReadOnly)
        {
            script.Globals["Terminate"] = (Func<bool, bool>)Terminate; // Terminate script

            // Expose movement functionality
            if (ExposeMovement)
            {
                script.Globals["Move"] = (Func<float, float, int>)Move; // Basic movement method
            }
        }

        #region Exposed functions
        private static int Move(float x, float y)
        {
            var trans = GameObject.Find("Platform").GetComponent<Transform>();
            var pos = new UnityEngine.Vector2(trans.position.x + x, trans.position.y + y);

            trans.position = pos;

            return 0;
        }

        private static bool Terminate(bool reset)
        {
            // // Reset everything here
            // if (reset)
            //     script.Call(script.Globals["Reset"]);

            // // Terminate script
            // state = State.ExecutionNone;

            return reset;
        }
        #endregion
    }

    // State
    enum State
    {
        ExecutionStart,
        ExecutionTick,
        ExecutionNone
    }

}