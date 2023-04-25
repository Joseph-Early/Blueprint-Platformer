using System.Diagnostics;
using UnityEngine;

namespace Lua.Modules
{
    public class Miscellaneous : MonoBehaviour
    {
        public static void Print(string message) => UnityEngine.Debug.Log($"{message}");
    }
}