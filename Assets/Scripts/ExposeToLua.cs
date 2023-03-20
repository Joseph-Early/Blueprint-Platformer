using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lua
{
    // This is a wrapper class around the LuaInteraction to allow easy setting of exposed properties via the Unity inspector (Does not expose any properties itself but invokes .Expose())
    [RequireComponent(typeof(LuaInteraction))]
    public class ExposeToLua : MonoBehaviour
    {
        #region Exposing
        [Header("Expose Properties to Lua")]
        [SerializeField] private bool ReadOnly = false;

        // Fields that can be exposed
        [SerializeField] private bool ExposeMovement, ExposeEnabled, ExposeColours, ExposeRotation, ExposeScale;
        #endregion

        // Set allowed flags
        private void Awake() {
            // Get reference to LuaInteraction which controls the Lua logic for each object
            var interaction = GetComponent<LuaInteraction>();

            // Check interaction is valid and expose properties on the LuaInteraction script
            if (interaction == null)
                UnityEngine.Debug.LogError($"Interaction component missing on {gameObject.name}");
            else
                interaction.Expose(ExposeMovement, ExposeEnabled, ExposeColours, ExposeRotation, ExposeScale, ReadOnly);
        }
    }
}