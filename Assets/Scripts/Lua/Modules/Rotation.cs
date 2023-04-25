using UnityEngine;

namespace Lua.Modules
{
    public class Rotation : MonoBehaviour
    {
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