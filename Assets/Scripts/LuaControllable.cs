using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LuaControllable : MonoBehaviour
{
    public string IdentifierInLevel = "__UNDEFINED_OBJECT_NAME__";

    private GameObject IdentifierNamePopUp;

    // Start is called before the first frame update
    private void Awake()
    {
        // Change the gameobject name to the IdentifierInLevel name
        name = IdentifierInLevel;

        // Add LuaControllable to find all LuaControllable's
        tag = "LuaControllable";
    }

    // Update is called once per frame
    private void Update()
    {
        
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
}
