using UnityEngine;

namespace Lua.Modules
{
    public class Scale : MonoBehaviour
    {
        /// <summary>
        /// Moves the controller relative to its current position
        /// </summary>
        /// <param name="controllerName">The name of the controller</param>
        /// <param name="x">The amount to move the controller on the x axis</param>
        /// <param name="y">The amount to move the controller on the y axis</param>
        public static int SetScaleRelative(string controllerName, float x, float y)
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

                // Get the transform
                var trans = controller.obj.GetComponent<Transform>();

                // Calculate the new scale
                var scale = new UnityEngine.Vector3(trans.localScale.x + x, trans.localScale.y + y, trans.localScale.z);

                // Set the scale
                trans.localScale = scale;

                // Return 0 - success
                return 0;
            }

            // Return 1 - error
            return 1;
        }
        /// <summary>
        /// Moves the controller to an absolute position
        /// </summary>
        /// <param name="controllerName">The name of the controller</param>
        /// <param name="x">The x position to move the controller to</param>
        /// <param name="y">The y position to move the controller to</param>
        public static int SetScaleAbsolute(string controllerName, float x, float y)
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

                // Get the transform
                var trans = controller.obj.GetComponent<Transform>();

                // Calculate the new scale
                var scale = new UnityEngine.Vector3(x, y, trans.localScale.z);

                // Set the scale
                trans.localScale = scale;

                // Return 0 - success
                return 0;
            }

            // Return 1 - error
            return 1;
        }
    }
}