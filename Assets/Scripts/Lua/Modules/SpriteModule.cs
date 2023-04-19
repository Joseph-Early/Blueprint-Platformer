using UnityEngine;

namespace Lua.Modules
{
    public class SpriteModule : MonoBehaviour
    {
        public static int SetSpriteColour(string controllerName, float r, float g, float b, float a)
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

                // Get the sprite renderer
                var sprite = controller.obj.GetComponent<SpriteRenderer>();

                if (sprite == null) throw new System.Exception("No sprite added");

                // Change the sprite colour
                Color newColour = new Color(r, g, b, a);

                // Set sprite colour
                sprite.color = newColour;

                // Return 0 - success
                return 0;
            }

            // Return 1 - error
            return 1;
        }
    }
}