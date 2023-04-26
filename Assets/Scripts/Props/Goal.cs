using Managers;
using UnityEngine;

namespace Props
{
    public class Goal : MonoBehaviour
    {
        // On collision with the player, set the goal reached flag to true
        private void OnCollisionEnter2D(Collision2D collider)
        {
            // The tag cannot be used as Player is a Lua controllable and its tag is automatically set to Lua Controllable at runtime
            if (collider.gameObject.name == "Player")
                Globals.Instance.GetComponent<LevelWinState>().goalReached = true;
        }
    }
}