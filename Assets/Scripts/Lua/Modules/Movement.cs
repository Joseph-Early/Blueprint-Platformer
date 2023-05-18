using UnityEngine;

namespace Lua.Modules
{
    public class Movement : MonoBehaviour
    {
        /// <summary>
        /// Moves the controller relative to its current position
        /// </summary>
        /// <param name="controllerName">The name of the controller</param>
        /// <param name="x">The amount to move the controller on the x axis</param>
        /// <param name="y">The amount to move the controller on the y axis</param>
        public static int SetPositionRelative(string controllerName, float x, float y)
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

        /// <summary>
        /// Moves the controller to an absolute position
        /// </summary>
        /// <param name="controllerName">The name of the controller</param>
        /// <param name="x">The x position to move the controller to</param>
        /// <param name="y">The y position to move the controller to</param>
        public static int SetPositionAbsolute(string controllerName, float x, float y)
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

                // Calculate the new position
                var pos = new UnityEngine.Vector2(x, y);

                // Set the transform
                trans.position = pos;

                // Return 0 - success
                return 0;
            }

            // Return 1 - error
            return 1;
        }
    }
}