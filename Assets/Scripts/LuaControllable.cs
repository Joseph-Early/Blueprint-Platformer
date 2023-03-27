using UnityEngine;
using TMPro;

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
    void OnMouseEnter()
    {
        print(IdentifierInLevel);

        // Create pop up with the name of the IdentifierInLevel
        IdentifierNamePopUp = Instantiate(Managers.Globals.Instance.IdentifierNamePopUp, transform.position, Quaternion.identity);
        IdentifierNamePopUp.GetComponent<TextMeshPro>().text = $"\"{IdentifierInLevel}\"";
    }

    void OnMouseExit()
    {
        // Destroy the IdentifierNamePopUp
        if (IdentifierNamePopUp != null)
        {
            Destroy(IdentifierNamePopUp);
            IdentifierNamePopUp = null;
        }
    }

    // Check operation legality
    public bool CheckOperationLegality(string variableGetGetValueOf)
    {
        // Get the property info for the property
        var propertyInfo = GetType().GetProperty(variableGetGetValueOf);

        // If the property info is not null, get the value of the property
        if (propertyInfo != null)
        {
            var value = (bool)propertyInfo.GetValue(this, null);
            return value;
        }

        // If the property info is null, return false and log an error
        UnityEngine.Debug.LogError($"CheckOperationLegality errored, tried accessing the property {variableGetGetValueOf}");
        return false;
    }
}
