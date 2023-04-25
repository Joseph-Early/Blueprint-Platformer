using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Actor.Components
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float InitialHealth = 100;
        [SerializeField] private float healthMax = 100;
        [HideInInspector] public float health;

        // Set the health on awake
        private void Awake()
        {
            health = InitialHealth;
        }

        // If the actor is at zero health, kill them
        void Update()
        {
            health = Mathf.Clamp(health, 0, healthMax);

            if (health < 1)
                Kill();
        }

        internal virtual void Kill() => Destroy(gameObject);
    }
}
