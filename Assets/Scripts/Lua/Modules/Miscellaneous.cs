using System.Diagnostics;
using UnityEngine;

namespace Lua.Modules
{
    public class Miscellaneous : MonoBehaviour
    {
        /// <summary>
        /// Print a message to the in-game console
        /// </summary>
        /// <param name="message">The message to print</param>
        public static void Print(string message) => GUI.Console.Instance.AddMessage(message);
    }
}