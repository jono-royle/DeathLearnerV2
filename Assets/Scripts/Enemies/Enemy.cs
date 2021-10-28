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
        public Sword Sword;
        public float HitTime = 0.3f;
        public float PlungeSpeed = -7f;
        public Vector2 Direction = Vector2.left;
        public Ghost Ghost;
        public GoblinHead GoblinHead;

        protected SpriteRenderer spriteRenderer;
        protected float arrowTimer = 0;
        protected bool hitLeft = false;
        protected bool hitRight = false;
        protected float hitTimer = 0;
        protected float moveVelocity = 0;
        protected bool isGrounded = false;
        protected bool swordExists = false;
        protected bool plunging = false;
        protected bool isBoss = false;

        protected virtual void Start()
        {
            spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            if(Direction.x > 0)
            {
                CharacterActions.ChangeDirection(Direction != Vector2.left, spriteRenderer, Direction);
            }
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

            if (collision.gameObject.tag == "Scenery")
            {
                isGrounded = true;
            }
        }

        protected bool FireEnemyArrow()
        {
            if (!hitLeft && !hitRight && arrowTimer <= 0)
            {
                var position = transform.position;
                Rigidbody2D arrow = Instantiate(Arrow, position, transform.rotation);
                CharacterActions.FireArrow(Direction, arrow, ArrowSpeed);

                arrowTimer = ArrowCooldown;
                return true;
            }
            return false;
        }

        protected void EnemyJump()
        {
            if (isGrounded)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, Jump);
            }
        }

        protected void SwingEnemySword()
        {
            var position = transform.position;
            position.x += Direction.x;
            Sword sword = Instantiate(Sword, position, transform.rotation);
            CharacterActions.SwingSword(Direction, sword, gameObject.transform, swordDestroyed);
            swordExists = true;
        }

        protected void PlungingAttack()
        {
            var position = transform.position;
            position.y -= 1;
            var rotate = Quaternion.Euler(0, 0, -90);
            Sword sword = Instantiate(Sword, position, rotate);
            CharacterActions.PlungingAttack(sword, gameObject.transform, swordDestroyed);
            swordExists = true;
        }

        protected void Plunge()
        {
            plunging = true;
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, PlungeSpeed);
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Scenery")
            {
                isGrounded = false;
            }
        }

        private void HealthCheck()
        {

            if (Health <= 0)
            {
                EnemyDeathEvent.Invoke();
                CreateDeathEffects();
                Destroy(gameObject);
            }
        }

        private void CreateDeathEffects()
        {
            Ghost ghost = Instantiate(Ghost, transform.position, transform.rotation);
            ghost.EndPosition = new Vector2(transform.position.x, transform.position.y + 15);
            if (!isBoss)
            {
                Instantiate(GoblinHead, transform.position, transform.rotation);
            }
        }

        private void UpdateTimers()
        {
            arrowTimer -= Time.deltaTime;
            hitTimer -= Time.deltaTime;
        }

        private void swordDestroyed()
        {
            plunging = false;
            swordExists = false;
        }
    }
}
