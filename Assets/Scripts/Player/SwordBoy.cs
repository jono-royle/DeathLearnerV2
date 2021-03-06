using Assets.Scripts.Static;
using UnityEngine;
using UnityEngine.Events;
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
    public int PlayerHealth = 8;
    public Vector2 Direction = Vector2.right;
    public UnityEvent<int> PlayerHitEvent;
    public UnityEvent PlayerDeathEvent;

    private bool isGrounded = false;
    private float arrowTimer = 0;
    private bool hitLeft = false;
    private bool hitRight = false;
    private float hitTimer = 0;
    private bool swordExists = false;
    private bool plunging = false;
    private SpriteRenderer spriteRenderer;
    private int playerHealthMax;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        if (Direction.x < 0)
        {
            CharacterActions.ChangeDirection(Direction != Vector2.left, spriteRenderer, Direction);
        }
        playerHealthMax = PlayerHealth;
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
            PlayerJump();
        }

        float moveVelocity = 0;

        //Left Right Movement
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            moveVelocity = -Speed;
            Direction = CharacterActions.ChangeDirection(true, spriteRenderer, Direction);
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            moveVelocity = Speed;
            Direction = CharacterActions.ChangeDirection(false, spriteRenderer, Direction);
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
            FirePlayerArrow();
        }

        //Swing Sword
        if (Input.GetKeyDown(KeyCode.Space) && !swordExists)
        {
            if (isGrounded)
            {
                SwingPlayerSword();
            }
            else if (!plunging)
            {
                PlungingAttack();
                Plunge();
                return;
            }
        }

        GetComponent<Rigidbody2D>().velocity = new Vector2(moveVelocity, GetComponent<Rigidbody2D>().velocity.y);
    }

    private void PlayerJump()
    {
        if (isGrounded)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, Jump);
        }
    }

    private void SwingPlayerSword()
    {
        var position = transform.position;
        position.x += Direction.x;
        Sword sword = Instantiate(Sword, position, transform.rotation);
        CharacterActions.SwingSword(Direction, sword, gameObject.transform, swordDestroyed);
        swordExists = true;
    }

    private void PlungingAttack()
    {
        var position = transform.position;
        position.y -= 1;
        var rotate = Quaternion.Euler(0, 0, -90);
        Sword sword = Instantiate(Sword, position, rotate);
        CharacterActions.PlungingAttack(sword, gameObject.transform, swordDestroyed);
        swordExists = true;
    }

    private void Plunge()
    {
        plunging = true;
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, PlungeSpeed);
    }

    private void FirePlayerArrow()
    {
        if (arrowTimer <= 0)
        {
            var position = transform.position;
            Rigidbody2D arrow = Instantiate(Arrow, position, transform.rotation);
            CharacterActions.FireArrow(Direction, arrow, ArrowSpeed);

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
        if (collision.gameObject.tag == "Enemy" && collision.otherCollider.gameObject.name != "Sword(Clone)")
        {
            if(collision.collider.gameObject.name == "RedArrow(Clone)")
            {
                PlayerHealth--;
            }
            else
            {
                PlayerHealth = PlayerHealth - 2;
            }

            if (PlayerHealth <= 0)
            {
                PlayerDeath();
            }
            else
            {
                PlayerHitEvent.Invoke(PlayerHealth);
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
        else if (collision.gameObject.tag == "Scenery")
        {
            isGrounded = true;
        }
    }

    //On a normal level restart on death, in the final level need to reset manually
    private void PlayerDeath()
    {
        var activeScene = SceneManager.GetActiveScene();
        if(activeScene.name != "Level_Boss")
        {
            SceneManager.LoadScene(activeScene.buildIndex);
        }
        else
        {
            PlayerDeathEvent.Invoke();
            PlayerHealth = playerHealthMax;
            PlayerHitEvent.Invoke(PlayerHealth);
            gameObject.transform.position = new Vector2(14.3f, -2.15f);
            isGrounded = false;
            hitLeft = false;
            hitRight = false;
            hitTimer = -1;
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Scenery")
        {
            isGrounded = false;
        }
    }

    private void OnApplicationQuit()
    {
        MLTextWriter.DeleteTxtFile();
        MLEngineStarter.DeleteEngineFile();
    }
}
