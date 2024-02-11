using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private Rigidbody2D asteroidIn;
    [SerializeField] private float minNewSpeed, maxNewSpeed;
    [SerializeField] private Transform deadPoint1, deadPoint2;
    [SerializeField] private int points;
    
    Camera _cam;
    private Vector2 min;
    private Vector2 max;
    private float checkX;
    private float checkY;
    private Rigidbody2D rb;
    private float asteroidSpeed;
    private bool firstAwake;
    // Start is called before the first frame update
    void Start()
    {
        _cam = Camera.main;
        min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        rb = GetComponent<Rigidbody2D>();
        rb.AddTorque(0.02f, ForceMode2D.Impulse);
        asteroidSpeed = Random.Range(minNewSpeed, maxNewSpeed);
        firstAwake = true;
    }

    // Update is called once per frame
    void Update()
    {
        checkX = transform.position.x;
        checkY = transform.position.y;

        if (transform.position.x <= max.x && firstAwake == true)
        {            
            firstAwake = false;
        }
        if (firstAwake == false)
        {
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
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet")|| collision.gameObject.CompareTag("EnemyBullet"))
        {
            DeatroyAndBild();
           
            Destroy(collision.gameObject);
            if (collision.gameObject.CompareTag("Bullet"))
            {
                WorldData.points += points;
            }
        }
    }

    public void DeatroyAndBild()
    {
        Rigidbody2D asteroidInstance = Instantiate(asteroidIn, deadPoint1.position, Quaternion.identity) as Rigidbody2D;       
        asteroidInstance.velocity = deadPoint1.up * asteroidSpeed;

        Rigidbody2D asteroidInstance2 = Instantiate(asteroidIn, deadPoint2.position, Quaternion.identity) as Rigidbody2D;
        asteroidInstance2.velocity = deadPoint2.up * asteroidSpeed;
       
        Destroy(this.gameObject);
    }
}
