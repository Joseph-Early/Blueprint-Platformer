# Blueprint Platformer - WIP
*Exploration game where you explore levels however objects in the world are incomplete and you must script them using Lua to solve puzzles and advance.*

## Important
**READ ME**: this project is still in development and most features are currently not complete and/or the project may be in an unplayable state 
For details of the development of the game, visit the [Trello board](https://trello.com/b/ZCXg9sbP/finale-blueprint-platformer). 

## Sections
- [Blueprint Platformer](#blueprint-platformer)
  - [Sections](#sections)
  - [Game systems - Basic Overview](#game-systems---basic-overview)
  - [Third-party Libraries](#third-party-libraries)
  - [API Documentation](#api-documentation)
    - [Adding new functions which can be invoked by Lua](#adding-new-functions-which-can-be-invoked-by-lua)

## Game systems - Basic Overview
- 2D, side-scroller player controller
    - Jump
    - Move left and right
    - Health
- Enemies
- Lua Manager
- Audio manager

## Third-party Libraries
This game uses the following libraries:
- [MoonSharp](https://www.moonsharp.org/) - C# Lua Interpreter

## API Documentation
**NOTE**: The API is subject to change during development!

### Adding new functions which can be invoked by Lua
Inside the Scripts/`Lua.Modules`, create a module file using this template:
```cs
using UnityEngine;

namespace Lua.Modules
{
    public class ModuleName : MonoBehaviour
    {
    }
}
```

To create an example method, you must provide a return type (optionally with parameters) e.g. (ensure that the method is static and public):<br>
**Note**: There is a way to bypass giving a return type if you use a parameter (see `Lua.Modules.Miscellaneous` and `Print` however this is not recommended)
```cs
// Example method 
public static int ExampleMethod(string message)
{
    UnityEngine.Debug.Log(message);

    return 0; // 0 - Success
}
```

To expose the method so it can be used in Lua, there are two more steps. Inside `Lua.LuaManager`, there is a region called "Expose". In there, create either a `Func` or `Action` with a reference to the functions in your module e.g.
```cs
// Miscellaneous
script.Globals["ExampleMethod"] = (Func<string, int>)ModuleName.ExampleMethod;
```

Finally, it is required to add a toggle to the method inside of `Lua.LuaControllable` if the method requires the use of either `Utils.ReturnLuaObject` or `LuaControllable.CheckOperationLegality`. To do this, go inside `Lua.LuaControllable` and then inside the region " Serialize fields for enabling allowed functions on an object". Then create a boolean value (private and SerializeField) with the exact name of the function created (requiring your function be named uniquely among all exposed methods) e.g.
```cs
[SerializeField] private bool ExampleMethod = false;
```

To use the method `LuaControllable.CheckOperationLegality`, you must also use `Utils.ReturnLuaObject` as LuaControllable.CheckOperationLegality requires a reference to that game object and its LuaController.

First the method created requires a string parameter (first parameter is the standard) such as this: `public static int SetPositionRelative(string controllerName, float x, float y)`

Secondary, a reference to the game object wanted can be obtained using "Utils.ReturnLuaObject" (requiring all game objects to have a unique name set in their `Lua.LuaControllable` serialized field)

And finally, it is possible to check if a command is allowed on that object using `LuaControllable.CheckOperationLegality`. This method should be called with the value `System.Reflection.MethodBase.GetCurrentMethod().Name`.

And example of all of this from one of basic modules:
```cs
                                    // String name used in Lua
public static int SetPositionRelative(string controllerName, float x, float y)
{
    // Get a reference to the controller
    var controller = Utils.ReturnLuaObject(controllerName);

    // Check the controller is not null
    // System.Reflection.MethodBase.GetCurrentMethod().Name is required to be passed
    if (controller.controller != null)
    {
        // Check the operation is allowed
        switch (controller.controller.CheckOperationLegality(System.Reflection.MethodBase.GetCurrentMethod().Name))
        {
            case false:
                return 2; // 2 - Illegal operation
            case null:
                return 3; // 3 - Unknown operation
        }

        // Any code here

        return 0; // 0 - Command success
    }

    return 1; // 1 - Command failed
}
```
