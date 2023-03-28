using UnityEngine;

namespace Lua.Modules
{
    public class Scale : MonoBehaviour
    {
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