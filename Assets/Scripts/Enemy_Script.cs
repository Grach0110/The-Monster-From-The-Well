using UnityEngine;

public class Enemy_Script : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    SpriteRenderer spriteRenderer;
    AudioSource audioSource;
    public GameObject danger;

    public Transform point; // Точка патрулирования
    private Transform player; // Игрок

    bool movingRight; // разворот

    private float speed;
    public float jumpSpeed;
    public float speedPatrol; // Скорость в патруле
    public float speedAttack; // Скорость в атаке
    public int distancePatrol; // Дистанция патруля от point
    public float stopDis; // Дистанция разрыва атаки (перестает приследовать игрока)

    bool jump;

    // Состояние
    bool patrol;
    bool attack;
    bool goBack;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        speed = speedPatrol;
        danger.SetActive(false);
        jump = false;
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, point.position) < distancePatrol && attack == false)
        {
            patrol = true;
        }
        if (Vector2.Distance(transform.position, player.transform.position) < stopDis)
        {
            attack = true;
            patrol = false;
            goBack = false;
            
        }
        if (Vector2.Distance(transform.position, player.transform.position) > stopDis)
        {
            goBack = true;
            attack = false;
        }

        if (patrol)
        {
            audioSource.Play();
            Patrol();
        }
        else if (attack)
        {
            danger.SetActive(true);
            Attack();
        }
        else if (goBack)
        {
            danger.SetActive(false);
            GoBack();
        }
    }

    private void FixedUpdate()
    {
        if (jump)
        {
            Jump();
            jump = false;
        }
    }

    // Патруль
    void Patrol()
    {
        if (transform.position.x > point.transform.position.x + distancePatrol)
        {
            movingRight = false;
        }
        else if (transform.position.x < point.transform.position.x - distancePatrol)
        {
            movingRight = true;
        }

        if (movingRight)
        {
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime,transform.position.y);
            spriteRenderer.flipX = false;
        }
        else
        {
            transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
            spriteRenderer.flipX = true;
        }
    }

    // Атака
    void Attack()
    {
        
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        if (player.transform.position.x < gameObject.transform.position.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
        speed = speedAttack;
    }

    // Возврат к патрулю
    void GoBack()
    {
        transform.position = Vector2.MoveTowards(transform.position, point.position, speed * Time.deltaTime);
        speed = speedPatrol;
        
    }

    void Jump()
    {
        rigidbody2D.AddForce(transform.up * jumpSpeed);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Jump"))
        {
            jump = true;
        }
    }
}