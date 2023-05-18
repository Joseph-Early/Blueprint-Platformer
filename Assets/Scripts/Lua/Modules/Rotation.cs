using UnityEngine;

namespace Lua.Modules
{
    public class Rotation : MonoBehaviour
    {
        /// <summary>
        /// Rotates the controller relative to its current rotation
        /// </summary>
        /// <param name="controllerName">The name of the controller</param>
        /// <param name="angle">The amount to rotate the controller by</param>
        public static int SetRotationRelative(string controllerName, float angle)
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

                // Calculate the new rotation
                var rot = new UnityEngine.Vector3(trans.rotation.x, trans.rotation.y, angle);

                // Set the rotation
                trans.Rotate(rot);

                // Return 0 - success
                return 0;
            }

            // Return 1 - error
            return 1;
        }

        /// <summary>
        /// Rotates the controller to an absolute rotation
        /// </summary>
        /// <param name="controllerName">The name of the controller</param>
        /// <param name="angle">The angle to rotate the controller to</param>
        public static int SetRotationAbsolute(string controllerName, float angle)
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

                // Calculate the new rotation
                var rot = new UnityEngine.Vector3(trans.rotation.x, trans.rotation.y, angle);

                // Set the rotation
                trans.rotation = UnityEngine.Quaternion.Euler(rot);

                // Return 0 - success
                return 0;
            }

            // Return 1 - error
            return 1;
        }
    }
}