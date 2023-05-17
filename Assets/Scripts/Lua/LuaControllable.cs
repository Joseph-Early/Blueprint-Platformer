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

        #region Serialise fields for enabling allowed functions on an object
        #pragma warning disable 0414
        [SerializeField] private bool SetPositionRelative = false;
        [SerializeField] private bool SetPositionAbsolute = false;
        [SerializeField] private bool SetRotationRelative = false;
        [SerializeField] private bool SetRotationAbsolute = false;
        [SerializeField] private bool SetScaleRelative = false;
        [SerializeField] private bool SetScaleAbsolute = false;
        [SerializeField] private bool SetSpriteColour = false;
        #pragma warning restore 0414

        #endregion

        // Start is called before the first frame update
        private void Awake()
        {
            // Change the gameobject name to the IdentifierInLevel name
            name = IdentifierInLevel;

            // Add LuaControllable to find all LuaControllable's
            tag = "LuaControllable";
        }

        // On mouse over over object, update the information panel with the object info
        private void OnMouseEnter()
        {
            // Enable panel
            Managers.Globals.Instance.ObjectInformationPanel.SetActive(true);

            // Update panel
            Managers.Globals.Instance.ObjectInformationPanelObjectName.text = $"\"{IdentifierInLevel}\"";
            Managers.Globals.Instance.ObjectInformationPanelFunctions.text = "Move\nMoveAbs\nRotate\nRotateAbs\nScale\nScaleAbs\nSetColour";
            Managers.Globals.Instance.ObjectInformationPanelEnabled.text = $"{SetPositionRelative}\n{SetPositionAbsolute}\n{SetRotationRelative}\n{SetRotationAbsolute}\n{SetScaleRelative}\n{SetScaleAbsolute}\n{SetSpriteColour}".Replace("True", "Enabled").Replace("False", "Disabled");
        }

        // On mouse exit, clear the information panel and hide it
        private void OnMouseExit()
        {
            // Clear panel
            Managers.Globals.Instance.ObjectInformationPanelObjectName.text = "";
            Managers.Globals.Instance.ObjectInformationPanelFunctions.text = "";
            Managers.Globals.Instance.ObjectInformationPanelEnabled.text = "";

            // Disable panel
            Managers.Globals.Instance.ObjectInformationPanel.SetActive(false);

        }

        /// <summary>
        /// Returns the legality of an field on that object.
        /// Operation name must match the variable name within this script and the LuaManager script to find the value.
        /// </summary>
        /// <param name="field">Operation name to find the value of</param>
        /// <returns>
        /// Returns true/false if the field is allowed and can return null and display a Unity error message if the field cannot be found.
        /// </returns>
        public bool? CheckOperationLegality(string field)
        {
            // Get the property info for the property
            FieldInfo fieldInfo = null;
            foreach (var f in GetType().GetRuntimeFields())
            {
                if (f.Name != field) continue;

                fieldInfo = f;
                break;
            }

            // If the property info is not null, get the value of the property
            if (fieldInfo != null)
            {
                var value = (bool)fieldInfo.GetValue(this);
                return value;
            }

            // If the property info is null, return false and log an error
            UnityEngine.Debug.LogWarning($"CheckOperationLegality errored, tried accessing the field `{field}`");
            return null;
        }
    }
}