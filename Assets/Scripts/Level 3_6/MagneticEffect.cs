using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level3_6
{
    public class MagneticEffect : MonoBehaviour
    {
        public float attractionForce = 50f;
        public float maxDistance = 50f; // The maximum distance at which the magnetic force is applied
        public float duration = 5f; // The duration of the magnetic effect in seconds

        private Transform player; // The transform of the main player
        private float timer; // The timer for the magnetic effect
        public PlayerMovement playerMovement;
        private bool functionCalled = false;
        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").transform; // Get the transform of the main player
        }
        private void Update()
        {
            if (playerMovement.hasMagnet && !functionCalled)
            {
                StartMagneticEffect();
            }
        }
        private void FixedUpdate()
        {
            if (timer > 0)
            {
                // Find all game objects with the "Magnetic" tag
                GameObject[] magneticObjects = GameObject.FindGameObjectsWithTag("food");

                // Apply magnetic force to each magnetic object
                foreach (GameObject magneticObject in magneticObjects)
                {
                    Rigidbody2D magneticRigidbody = magneticObject.GetComponent<Rigidbody2D>();

                    if (magneticRigidbody != null)
                    {
                        Vector2 direction = new Vector2(player.position.x, player.position.y) - magneticRigidbody.position; // Calculate the direction towards the player
                        float distance = direction.magnitude; // Calculate the distance between the object and the player

                        // Apply the magnetic force if the object is within the maxDistance
                        if (distance <= maxDistance)
                        {
                            float forceMagnitude = attractionForce; // Calculate the force magnitude based on the distance
                            Vector2 force = direction.normalized * forceMagnitude; // Calculate the force vector
                            magneticRigidbody.AddForce(force); // Apply the force to the object
                        }
                    }
                }

                // Update the timer
                timer -= Time.fixedDeltaTime;
            }
        }

        // Start the magnetic effect
        public void StartMagneticEffect()
        {
            timer = duration;
            functionCalled = true;
        }

        // Reset the magnetic effect
        private void ResetMagneticEffect()
        {
            timer = 0;
        }
    }
}
