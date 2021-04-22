using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

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
    }
}
