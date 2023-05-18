using UnityEngine;

namespace Lua.Modules
{
    public class EditorPowers : MonoBehaviour
    {
        /// <summary>
        /// Destroy an object
        /// </summary>
        /// <param name="controllerName">The name of the controller</param>
        public static int DestroyObject(string controllerName)
        {
            var controller = Utils.ReturnLuaObject(controllerName);

            if (controller.controller != null)
            {
                // Illegal operation check
                switch (controller.controller.CheckOperationLegality(System.Reflection.MethodBase.GetCurrentMethod().Name))
                {
                    case false:
                        return 2; // Illegal operation
                    case null:
                        return 3; // Unknown operation
                }

                // Destroy the object
                Destroy(controller.obj);

                // Return 0 - success
                return 0;
            }

            // Return 1 - error
            return 1;
        }
    }
}