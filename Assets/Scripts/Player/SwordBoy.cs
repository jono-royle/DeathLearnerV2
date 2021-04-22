using Assets.Scripts.Static;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwordBoy : MonoBehaviour
{
    public Rigidbody2D Arrow;
    public Sword Sword;
    public float Speed = 5;
    public float Jump = 7;
    public float PlungeSpeed = -7f;
    public float ArrowSpeed = 35;
    public float ArrowCooldown = 1.5f;
    public float HitTime = 0.3f;
    public int PlayerHealth = 3;

    private bool isGrounded = true;
    private Vector2 direction = Vector2.right;
    private float arrowTimer = 0;
    private bool hitLeft = false;
    private bool hitRight = false;
    private float hitTimer = 0;
    private bool swordExists = false;
    private bool plunging = false;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimers();
        if (plunging)
        {
            Plunge();
            return;
        }

        //Jumping
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            if (isGrounded)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, Jump);
                isGrounded = false;
            }
        }

        float moveVelocity = 0;

        //Left Right Movement
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            moveVelocity = -Speed;
            direction = CharacterActions.ChangeDirection(true, spriteRenderer, direction);
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            moveVelocity = Speed;
            direction = CharacterActions.ChangeDirection(false, spriteRenderer, direction);
        }
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

        //Fire Arrow
        if (Input.GetKey(KeyCode.F) && arrowTimer <= 0)
        {
            FireCharacterArrow();
        }

        //Swing Sword
        if (Input.GetKeyDown(KeyCode.Space) && !swordExists)
        {
            if (isGrounded)
            {
                SwingSword();
            }
            else if(!plunging)
            {
                PlungingAttack();
                Plunge();
                return;

            }
        }

        GetComponent<Rigidbody2D>().velocity = new Vector2(moveVelocity, GetComponent<Rigidbody2D>().velocity.y);
    }

    private void SwingSword()
    {
        var position = transform.position;
        position.x += direction.x;
        Sword sword = Instantiate(Sword, position, transform.rotation);
        if (direction == Vector2.left)
        {
            var swordRenderer = sword.gameObject.GetComponent<SpriteRenderer>();
            swordRenderer.flipX = true;
        }
        sword.transform.parent = gameObject.transform;
        sword.SwordDestroyedEvent.AddListener(swordDestroyed);
        swordExists = true;
    }

    private void PlungingAttack()
    {
        var position = transform.position;
        position.y -= 1;
        var rotate = Quaternion.Euler(0, 0, -90);
        Sword sword = Instantiate(Sword, position, rotate);
        //sword.transform.Rotate(0, 90, 0);
        sword.Midair = true;
        sword.transform.parent = gameObject.transform;
        sword.SwordDestroyedEvent.AddListener(swordDestroyed);
        swordExists = true;
    }

    private void Plunge()
    {
        plunging = true;
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, PlungeSpeed);
    }

    private void FireCharacterArrow()
    {
        if(arrowTimer <= 0)
        {
            var position = transform.position;
            Rigidbody2D arrow = Instantiate(Arrow, position, transform.rotation);
            CharacterActions.FireArrow(direction, arrow, ArrowSpeed);

            arrowTimer = ArrowCooldown;
        }
    }

    private void swordDestroyed()
    {
        plunging = false;
        swordExists = false;
    }

    private void UpdateTimers()
    {
        arrowTimer -= Time.deltaTime;
        hitTimer -= Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy" && collision.otherCollider.gameObject.name != "Sword(Clone)")
        {
            PlayerHealth--;
            if(PlayerHealth <= 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }

            var enemyX = collision.gameObject.transform.position.x;
            if (enemyX > transform.position.x)
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
        else if(collision.gameObject.tag == "Scenery")
        {
            isGrounded = true;
        }
    }
}
