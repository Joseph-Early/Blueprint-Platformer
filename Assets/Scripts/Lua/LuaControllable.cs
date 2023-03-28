using System.Reflection;
using System.Text;
using UnityEngine;
using TMPro;
namespace Lua
{

    public class LuaControllable : MonoBehaviour
    {
        public string IdentifierInLevel = "__UNDEFINED_OBJECT_NAME__";

        private GameObject IdentifierNamePopUp;

        #region Serialize fields for enabling allowed functions on an object
        [SerializeField] public bool SetPositionRelative = false;
        [SerializeField] public bool SetPositionAbsolute = false;
        [SerializeField] public bool SetRotationRelative = false;
        [SerializeField] public bool SetRotationAbsolute = false;
        [SerializeField] public bool SetScaleRelative = false;
        [SerializeField] public bool SetScaleAbsolute = false;

        #endregion

        // Start is called before the first frame update
        private void Awake()
        {
            // Change the gameobject name to the IdentifierInLevel name
            name = IdentifierInLevel;

            // Add LuaControllable to find all LuaControllable's
            tag = "LuaControllable";
        }

        // On mouse over over game object, display name of IdentifierInLevel
        private void OnMouseEnter()
        {
            print(IdentifierInLevel);

            // Create pop up with the name of the IdentifierInLevel
            IdentifierNamePopUp = Instantiate(Managers.Globals.Instance.IdentifierNamePopUp, transform.position, Quaternion.identity);
            IdentifierNamePopUp.GetComponent<TextMeshPro>().text = $"\"{IdentifierInLevel}\"";
        }

        private void OnMouseExit()
        {
            // Destroy the IdentifierNamePopUp
            if (IdentifierNamePopUp != null)
            {
                Destroy(IdentifierNamePopUp);
                IdentifierNamePopUp = null;
            }
        }

        /// <summary>
        /// Returns the legality of an operation on that object.
        /// Operation name must match the variable name within this script and the LuaManager script to find the value.
        /// </summary>
        /// <param name="operation">Operation name to find the value of</param>
        /// <returns>
        /// Returns true/false if the operation is allowed and can return null and display a Unity error message if the field cannot be found.
        /// </returns>
        public bool? CheckOperationLegality(string operation)
        {
            // Get the property info for the property
            var fieldInfo = GetType().GetField(operation);

            // If the property info is not null, get the value of the property
            if (fieldInfo != null)
            {
                var value = (bool)fieldInfo.GetValue(this);
                return value;
            }

            // If the property info is null, return false and log an error
            UnityEngine.Debug.LogError($"CheckOperationLegality errored, tried accessing the property {operation}");
            return null;
        }
    }
}