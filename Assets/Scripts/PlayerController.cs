using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speedRotation;
    [SerializeField] private float speedMove;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float bulletInSec;
    [SerializeField] private Rigidbody2D bullet;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private Transform gunPoint;
    [SerializeField] private Slider lifeBar;
    Camera _cam;
    [SerializeField] private int hp;
    

    private AudioSource shoot;
    
    // блок переменных для перемещения
    private Rigidbody2D rb;
    private Vector2 min;        // координаты границ экрана
    private Vector2 max;
    private float x;
    private float y;
    private float checkX;
    private float checkY;
    private float speedQTR;     // Квадрат максимальной скорости
    private float realspeedQTR;//переменная реальной скорости объекта в квадрате
    public bool mouseController;
    private Vector2 mousePos, direction, currentDirection, thisPlayer;


    //Блок переменных стрельбы
    private float timeOut;
    private float curenTime;

    //Переменные урона

    private bool damageIn;
    [SerializeField] private float nonDamageTime;
    [SerializeField] private GameObject shield;
    [SerializeField] private GameObject fire;
    private bool dead;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _cam = Camera.main;
        min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        speedQTR = maxSpeed * maxSpeed;
        timeOut = 1 / bulletInSec;
        curenTime = 0;
        shoot = GetComponent<AudioSource>();
        damageIn = false;
        shield.SetActive(false);
        fire.SetActive(false);
        dead = false;
        WorldData.hp = hp;
        lifeBar.value = 1;
        mouseController = WorldData.mouseController;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");
        if (dead == false && mouseController == false)
        {
            Movement();
        }
        else if (dead == false && mouseController == true)
        {           
            MouseMovement();
        }
    }

    private void Update()
    {      
        if ( hp <= 0)
        {
            dead = true;
            fire.SetActive(true);
            damageIn = true;
            shield.SetActive(false);
            WorldData.hp = 0;
        }
        curenTime += Time.deltaTime;

        Fire();
        CheckMonitor();
    }

    private void Movement()
    {
        if (x != 0)
        {
            transform.Rotate(0, 0, -speedRotation * x);
        }


        realspeedQTR = rb.velocity.x * rb.velocity.x + rb.velocity.y * rb.velocity.y; //вычисляем квадрат реальной скорости

        if (realspeedQTR < speedQTR && y > 0)
        {
            rb.AddForce(transform.up * speedMove * y, ForceMode2D.Impulse);
        }
    }

    private void MouseMovement()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        thisPlayer = this.transform.position;
        direction = mousePos - thisPlayer;
        direction.Normalize();

        currentDirection = Vector2.Lerp(currentDirection, direction, speedRotation * Time.deltaTime);
        this.transform.up = currentDirection;

        realspeedQTR = rb.velocity.x * rb.velocity.x + rb.velocity.y * rb.velocity.y; //вычисляем квадрат реальной скорости

        if (realspeedQTR < speedQTR && Input.GetKey(KeyCode.Mouse1))
        {           
            rb.AddForce(transform.up * speedMove, ForceMode2D.Impulse);
        }
    }
    
    private void Fire()
    {
        if (Input.GetKeyDown(KeyCode.Space) && curenTime >= timeOut && dead == false && mouseController == false)
        {
            curenTime = 0;
            Rigidbody2D bulletInstance = Instantiate(bullet, gunPoint.position, Quaternion.identity) as Rigidbody2D;
            bulletInstance.velocity = gunPoint.up * bulletSpeed;
            shoot.Play();
        }
        else if (Input.GetKeyDown(KeyCode.Mouse0) && curenTime >= timeOut && dead == false && mouseController == true)
        {
            curenTime = 0;
            Rigidbody2D bulletInstance = Instantiate(bullet, gunPoint.position, Quaternion.identity) as Rigidbody2D;
            bulletInstance.velocity = gunPoint.up * bulletSpeed;
            shoot.Play();
        }
    }

    //проверка границ экрана

    private void CheckMonitor()
    { 
        checkX = transform.position.x;
        checkY = transform.position.y;

        if (transform.position.x > max.x)
        {
            transform.position = new Vector3(min.x, checkY,-1);
        }
        else if (transform.position.x < min.x)
        {
            transform.position = new Vector3(max.x, checkY,-1);
        }

        if (transform.position.y > max.y)
        {
            transform.position = new Vector3(checkX, min.y,-1);
        }
        else if (transform.position.y < min.y)
        {
            transform.position = new Vector3(checkX, max.y,-1);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.CompareTag("Asteroid") || collision.gameObject.CompareTag("EnemyBullet")) && damageIn == false)
        { 
            hp--;
            shield.SetActive(true);
            damageIn = true;
            lifeBar.value -=0.2f;
            StartCoroutine(NotDamage());
        }
        else if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            Destroy(collision.gameObject);           
        }
        else if (collision.gameObject.CompareTag("hp"))
        {
            hp = WorldData.hp;
            lifeBar.value = 1;
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("tmp"))
        {
            timeOut = timeOut*0.8f;
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("pis"))
        {
           WorldData.points += 1000;
           Destroy(collision.gameObject);
        }
    }

    IEnumerator NotDamage()
    {
        yield return new WaitForSeconds(nonDamageTime);
        shield.SetActive(false);
        damageIn = false;
    }
}
