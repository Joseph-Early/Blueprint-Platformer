using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            #region Movement
            float translation = Input.GetAxis("Horizontal") * speed * Time.deltaTime; // Check if the player has pressed left or right and calculate speed
            trans.Translate(translation, 0, 0); // Translate the player
            #endregion
        }
    }
}
