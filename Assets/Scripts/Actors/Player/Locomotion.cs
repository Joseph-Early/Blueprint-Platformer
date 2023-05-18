using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Actor.Player
{
    public class Locomotion : MonoBehaviour
    {
        [SerializeField] private float speed = 5; // Player speed

        private Transform trans;
        private Rigidbody2D rb;

        private void Awake()
        {
            // Get the transform
            trans = GetComponent<Transform>();
            if (trans == null) throw new Exception("Unable to get transform");

            // Get a reference to the rigidbody2D
            rb = GetComponent<Rigidbody2D>();
            if (rb == null) throw new Exception("Unable to get transform");
        }

        private void Update()
        {
            // Check if fell off world
            if (transform.position.y <= -10)
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

            // If the game is not in the playing state, return
            if (Managers.GameState.Instance.CurrentState != Managers.GameState.State.Playing) return;

            // Move the player left or right
            float translation = Input.GetAxis("Horizontal") * speed * Time.deltaTime; // Check if the player has pressed left or right and calculate speed
            trans.Translate(translation, 0, 0); // Translate the player
        }
    }
}
