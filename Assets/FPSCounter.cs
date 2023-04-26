using UnityEngine;
using TMPro;

namespace GUI
{

    public class FPSCounter : MonoBehaviour
    {
        private TextMeshProUGUI fpsCounter;
        private void Awake() => fpsCounter = GetComponent<TextMeshProUGUI>();

        // Update is called once per frame
        void Update()
        {
            fpsCounter.text = $"FPS: {1f / Time.smoothDeltaTime}";
        }
    }

}