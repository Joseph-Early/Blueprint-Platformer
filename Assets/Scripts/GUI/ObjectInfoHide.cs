using UnityEngine;
using TMPro;

namespace GUI
{
    
    public class ObjectInfoHide : MonoBehaviour
    {
        [SerializeField] private GameObject objectInformation, ObjectInformationName;
        // Update is called once per frame
        void Update()
        {
            // If name is empty, hide the object information panel (fixes but caused by the text editor being opened when on an object and mouse moved after)
            if (ObjectInformationName.GetComponent<TextMeshProUGUI>().text == "")
                objectInformation.SetActive(false);
        }
    }
}