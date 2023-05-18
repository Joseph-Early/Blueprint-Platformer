using UnityEngine;
using TMPro;

namespace GUI
{
    public class MousePosition : MonoBehaviour
    {
        // References for the camera and text to update
        [SerializeField] private Camera _camera;
        [SerializeField] TextMeshProUGUI _text;

        // Update is called once per frame
        private void Update()
        {
            // Toggle the mouse position with the f1 key
            if (Input.GetKeyDown(KeyCode.F1))
                _text.text = _text.text == "" ? "Mouse Position" : ""; // Any string will work here

            // Return if text is empty
            if (_text.text == "") return;

            // Convert mouse position to world position (2D)
            Vector2 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);

            // Update the text
            _text.text = $"Mouse X: {System.Math.Round(mousePosition.x, 2)}\nMouse Y: {System.Math.Round(mousePosition.y, 2)}";
        }
    }

}