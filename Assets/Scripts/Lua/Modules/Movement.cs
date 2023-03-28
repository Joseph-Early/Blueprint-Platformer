using UnityEngine;

namespace Lua.Modules
{
    public class Movement : MonoBehaviour
    {
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