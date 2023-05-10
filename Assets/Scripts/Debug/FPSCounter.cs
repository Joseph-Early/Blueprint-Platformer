using UnityEngine;
using TMPro;

#if DEBUG
namespace Debug
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
#endif