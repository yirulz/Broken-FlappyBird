using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlappyBird
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bird : MonoBehaviour
    {
        public float upForce;           // Upward force of the "flap"
        private bool isDead = false;    // Has the player collider with the wall? 
        private Rigidbody2D rigid;
        public float turnUp = 45;
        public float turnDown = -45;
        public bool turned = false;
        public float flapped;

        public Vector2 vel;
        // Use this for initialization
        void Start()
        {
            rigid = GetComponent<Rigidbody2D>();
        }
        private void Update()
        {
            TurnBird();
            vel = rigid.velocity;
        }

        public void Flap()
        {
            // Only flap if the Bird isn't dead yet
            if (!isDead)
            {
                rigid.velocity = Vector2.zero;
                // Give the bird some upward force
                rigid.AddForce(new Vector2(0, upForce), ForceMode2D.Impulse);
                flapped = rigid.transform.position.y;

                if (!turned)
                {
                    transform.rotation = Quaternion.Euler(0, 0, turnUp);
                    turned = true;
                }
            }
        }

        void OnCollisionEnter2D(Collision2D other)
        {
            // Cancel velocity
            rigid.velocity = Vector2.zero;
            // Bird is now dead
            isDead = true;
            // Tell the GameManager about it
            GameManager.Instance.BirdDied();
        }

        void TurnBird()
        {
            if (rigid.velocity.y <= -2)
            {
                transform.rotation = Quaternion.Euler(0, 0, turnDown);
                turned = false; 
            }

            
        }
    }
}