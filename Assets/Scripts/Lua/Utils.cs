using UnityEngine;

namespace Lua
{
    public class Utils : MonoBehaviour
    {
        /// <summary>
        /// Returns the LuaControllable and GameObject
        /// </summary>
        /// <param name="obj">Gameobject</param>
        /// <param name="controllerName">String name for the gameobject to find (must have a LuaControllable to work)</param>
        /// <returns>Tuple for GameObject .obj and LuaControllable .controller.</returns>
        public static (GameObject obj, LuaControllable controller) ReturnLuaObject(string controllerName)
        {
            // Iterate through all LuaControllers
            foreach (LuaControllable controller in LuaManager.Controllers)
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
    }
}