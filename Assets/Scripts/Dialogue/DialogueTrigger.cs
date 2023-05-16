using UnityEngine;

namespace Dialogue
{
    public class DialogueTrigger : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other) {
            DialogueSystem.Instance.Triggered = true;
        }
    }

}