using Assets.Scripts.Static;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Enemies
{
    public abstract class Enemy : MonoBehaviour
    {
        public float Health = 3;
        public float Speed = 5;
        public float Jump = 7;
        public float ArrowSpeed = 35;
        public float ArrowCooldown = 1.5f;
        public UnityEvent EnemyDeathEvent;
        public Rigidbody2D Arrow;
        public Rigidbody2D Player;
        public float HitTime = 0.3f;

        protected SpriteRenderer spriteRenderer;
        protected Vector2 direction = Vector2.left;
        protected float arrowTimer = 0;
        protected bool hitLeft = false;
        protected bool hitRight = false;
        protected float hitTimer = 0;
        protected float moveVelocity = 0;
        protected bool isGrounded = true;

        void Start()
        {
            spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        }

        protected virtual void Update()
        {
            UpdateTimers();

            if (hitLeft)
            {
                moveVelocity = -2 * Speed;
            }
            if (hitRight)
            {
                moveVelocity = 2 * Speed;
            }
            if (hitTimer <= 0)
            {
                hitRight = false;
                hitLeft = false;
            }

            GetComponent<Rigidbody2D>().velocity = new Vector2(moveVelocity, GetComponent<Rigidbody2D>().velocity.y);
        }

        protected virtual void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.name == "GreenArrow(Clone)")
            {
                Health--;
                HealthCheck();
            }
            else if (collision.collider.gameObject.name == "Sword(Clone)")
            {
                Health = Health - 2;
                HealthCheck();
                if(Player.transform.position.y > transform.position.y)
                {
                    if (Player.transform.position.x > transform.position.x)
                    {
                        hitLeft = true;
                        hitTimer = HitTime;
                    }
                    else
                    {
                        hitRight = true;
                        hitTimer = HitTime;
                    }
                }
            }

            if (collision.gameObject.tag == "Scenery")
            {
                isGrounded = true;
            }
        }

        protected void FireEnemyArrow()
        {
            if (!hitLeft && !hitRight && arrowTimer <= 0)
            {
                var position = transform.position;
                Rigidbody2D arrow = Instantiate(Arrow, position, transform.rotation);
                CharacterActions.FireArrow(direction, arrow, ArrowSpeed);

                arrowTimer = ArrowCooldown;
            }
        }

        protected void EnemyJump()
        {
            if (isGrounded)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, Jump);
                isGrounded = false;
            }
        }
        private void HealthCheck()
        {
            if (Health <= 0)
            {
                EnemyDeathEvent.Invoke();
                Destroy(gameObject);
            }
        }

        private void UpdateTimers()
        {
            arrowTimer -= Time.deltaTime;
            hitTimer -= Time.deltaTime;
        }

    }
}
