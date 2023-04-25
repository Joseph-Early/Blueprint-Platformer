using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Actor.Player
{
    public class Locomotion : MonoBehaviour
    {
        [SerializeField] private float speed = 5; // Player speed
        [SerializeField] private float JumpHeight = 10; // Jump height
        [SerializeField] private LayerMask groundLayer; // Ground layer

        // Grounded
        private bool Grounded
        {
            get
            {
                // Ground check
                RaycastHit hit;
                if (Physics.Raycast(groundCheck.position, Vector2.down, out hit, 1f, groundLayer))
                    return true;
                else
                    return false;
            }
        }

        private Transform trans, groundCheck;

        private Rigidbody2D rb;

        private void Awake()
        {
            // Get the transform
            trans = GetComponent<Transform>();
            if (trans == null) throw new Exception("Unable to get transform");
            
            // Get a reference to the rigidbody2D
            rb = GetComponent<Rigidbody2D>();
            if (rb == null) throw new Exception("Unable to get transform");

            // Get a reference to the sub-game-object GroundCheck's transform
            groundCheck = GameObject.Find("Player/GroundCheck").GetComponent<Transform>();
            if (trans == null) throw new Exception("Unable to get transform of GroundCheck");
        }

        private void Update()
        {
            #region Movement
            float translation = Input.GetAxis("Horizontal") * speed * Time.deltaTime; // Check if the player has pressed left or right and calculate speed
            trans.Translate(translation, 0, 0); // Translate the player
            #endregion

            // Check if the player jumped
            if (Input.GetButtonDown("Jump"))
            {
                // Apply a force
                rb.AddForce(new Vector2(0f, -JumpHeight));
                print($"Grounded status {Grounded}");
                
            }
        }
    }
}
