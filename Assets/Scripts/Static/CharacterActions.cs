using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Static
{
    public static class CharacterActions
    {
        public static Vector2 ChangeDirection(bool left, SpriteRenderer spriteRenderer, Vector2 currentDirection)
        {
            if (left)
            {
                if(currentDirection != Vector2.left)
                {
                    spriteRenderer.flipX = !spriteRenderer.flipX;
                }
                return Vector2.left;
            }
            if (!left)
            {
                if (currentDirection != Vector2.right)
                {
                    spriteRenderer.flipX = !spriteRenderer.flipX;
                }
                return Vector2.right;
            }
            throw new Exception("No direction specified");
        }

        public static void FireArrow(Vector2 direction, Rigidbody2D arrow, float arrowSpeed)
        {
            if (direction == Vector2.left)
            {
                var arrowRenderer = arrow.gameObject.GetComponent<SpriteRenderer>();
                arrowRenderer.flipX = true;
            }
            arrow.AddForce(direction * arrowSpeed);
        }

        public static void SwingSword(Vector2 direction, Sword sword, Transform parent, UnityAction swordDestroyed)
        {
            if (direction == Vector2.left)
            {
                var swordRenderer = sword.gameObject.GetComponent<SpriteRenderer>();
                swordRenderer.flipX = true;
            }
            sword.transform.parent = parent;
            sword.SwordDestroyedEvent.AddListener(swordDestroyed);
        }

        public static void PlungingAttack(Sword sword, Transform parent, UnityAction swordDestroyed)
        {
            sword.Midair = true;
            sword.transform.parent = parent;
            sword.SwordDestroyedEvent.AddListener(swordDestroyed);
        }
    }
}
