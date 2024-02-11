using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UfoController : MonoBehaviour
{
    [SerializeField] private float maxSpeed;
    [SerializeField] private float acselerate;
    [SerializeField] private Rigidbody2D boolet;

    [SerializeField] private float bulletInSec;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private Transform gunPoint;
    private float timeOut;
    private float cureTime;

    //Бонусы от убийства
    [SerializeField] private Rigidbody2D[] bonus;

    
    private GameObject player;   
    private Vector3 targetPos;
    private Vector3 thisPos;
    private float angle;

    Camera _cam;
    private Rigidbody2D rb;
    private Vector2 min;        // координаты границ экрана
    private Vector2 max;    
    private float checkX;
    private float checkY;
    private float speedQTR;     // Квадрат максимальной скорости
    private float realspeedQTR;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        speedQTR = maxSpeed * maxSpeed;
        timeOut = 1 / bulletInSec;
        cureTime = timeOut;
    }

    // Update is called once per frame
    void Update()
    {
        cureTime += Time.deltaTime;

        if(cureTime >= timeOut)
        {
            cureTime = 0;
            Rigidbody2D bulletInstance = Instantiate(boolet, gunPoint.position, Quaternion.identity) as Rigidbody2D;
            bulletInstance.velocity = gunPoint.right * bulletSpeed;
        }
        LookAtPlayer();
        Movement();
    }


    private void LookAtPlayer()
    {
        //Поворот на игрока
        targetPos = player.transform.position;
        thisPos = transform.position;
        targetPos.x = targetPos.x - thisPos.x;
        targetPos.y = targetPos.y - thisPos.y;
        angle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    private void Movement()
    {
        //Движение слева на право
        realspeedQTR = rb.velocity.x * rb.velocity.x + rb.velocity.y * rb.velocity.y; //вычисляем квадрат реальной скорости

        if (realspeedQTR < speedQTR)
        {
            rb.AddForce(Vector2.right * acselerate, ForceMode2D.Impulse);
        }
        checkX = transform.position.x;
        checkY = transform.position.y;
        //проверка границ экрана
        if (transform.position.x > max.x)
        {
            transform.position = new Vector3(min.x, checkY, -1);
        }
        else if (transform.position.x < min.x)
        {
            transform.position = new Vector3(max.x, checkY, -1);
        }

        if (transform.position.y > max.y)
        {
            transform.position = new Vector3(checkX, min.y, -1);
        }
        else if (transform.position.y < min.y)
        {
            transform.position = new Vector3(checkX, max.y, -1);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
        {           
            WorldData.needUfo = true;
            
        }
        else if (collision.gameObject.CompareTag("Bullet"))
        {           
            Destroy(collision.gameObject);
            WorldData.points += 200;
            WorldData.needUfo = true;
        }

        int i = Random.Range(0, bonus.Length);
        Rigidbody2D bonuses = Instantiate(bonus[i], this.transform.position, Quaternion.identity) as Rigidbody2D;
        bonuses.velocity = Vector2.zero;
        Destroy(this.gameObject);
    }
}
