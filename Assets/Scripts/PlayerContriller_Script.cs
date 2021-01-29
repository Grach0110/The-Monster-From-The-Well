using UnityEngine;

public class PlayerContriller_Script : MonoBehaviour
{
    Rigidbody2D rb2D;
    SpriteRenderer spriteRenderer;
    Animator animator;
    public Sprite spriteDead;
    AudioSource audioSource;

    private float movementX;
    public float speed = 5f;

    private bool isGround;

    private float jumpSpeed = 7f;
    private float speedY;
    public float criticalSpeedY; // Ограничение высоты (игрок упал, разбился)

    public bool isDead;

    public bool stars = false; // собрал звезду
    public GameObject imageStars; // картинка звезды (подобрал)

    public float t = 2f; // время до появления меню (при смерти)

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        isDead = false;
        imageStars = GameObject.FindGameObjectWithTag("StarsImage");
        imageStars.SetActive(false);
    }

    private void Update()
    {
        movementX = Input.GetAxis("Horizontal");
        if (isGround && !isDead)
        {
            if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
        }
    }

    private void FixedUpdate()
    {
        speedY = rb2D.velocity.y;

        if (speedY < criticalSpeedY)
        {
            isDead = true;
            Dead();
        }
        if (!isDead)
        {
            Movement();
        }
        if (isDead)
        {
            t -= Time.deltaTime;

            if (t < 0)
            {
                GameObject finish = GameObject.FindGameObjectWithTag("Finish");
                finish.GetComponent<Finish_Script>().PlayerDead();
            }
        }
    }

    // Передвижение
    public void Movement()
    {
        Vector2 movement = new Vector2(movementX * speed, rb2D.velocity.y);

        if (rb2D.velocity.x < 0)
        {
            animator.SetTrigger("Run");
            spriteRenderer.flipX = true;
        }
        else if (rb2D.velocity.x > 0)
        {
            animator.SetTrigger("Run");
            spriteRenderer.flipX = false;
        }
        else if (rb2D.velocity.y == 0)
        {
            animator.SetTrigger("Idle");
        }

        rb2D.velocity = movement;
    }

    // Прыжок
    public void Jump()
    {
        rb2D.velocity = new Vector2(rb2D.velocity.x, jumpSpeed);
        
        isGround = false;
    }

    // Смерть игрока
    public void Dead()
    {
        audioSource.Play();
        animator.enabled = false;
        spriteRenderer.sprite = spriteDead;
        GameObject time = GameObject.FindGameObjectWithTag("Time");
        time.GetComponent<TimeGame_Script>().runTime = false;
    }

    // проверка земли
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;
        }
    }

    // Столкновения
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Stars"))
        {
            stars = transform;
            speed = 5f;
            imageStars.SetActive(true);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Danger"))
        {
            isDead = true;
            Dead();
        }
    }
}