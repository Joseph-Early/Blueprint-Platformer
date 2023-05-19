using System;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Health))]
    public class Locomotion : MonoBehaviour
    {
        [SerializeField] private float speed = 5; // Player speed

        private Transform trans;

        private void Awake()
        {
            // Get the transform
            trans = GetComponent<Transform>();
        }

        private void Update()
        {
            // Check if fell off world
            if (transform.position.y <= -10)
                GetComponent<Health>().health = 0;

            // If the game is not in the playing state, return
            if (Managers.GameState.Instance.CurrentState != Managers.GameState.State.Playing) return;

            // Move the player left or right
            float translation = Input.GetAxis("Horizontal") * speed * Time.deltaTime; // Check if the player has pressed left or right and calculate speed
            trans.Translate(translation, 0, 0); // Translate the player
        }
    }
}
