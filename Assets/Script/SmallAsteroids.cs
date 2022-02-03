using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallAsteroids : MonoBehaviour
{
    [SerializeField] private int points;
    Camera _cam;
    private Vector2 min;
    private Vector2 max;
    private float checkX;
    private float checkY;
    // Start is called before the first frame update
    private void Start()
    {
        _cam = Camera.main;
        min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
    }

    void Update()
    {
        checkX = transform.position.x;
        checkY = transform.position.y;

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

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet") || collision.gameObject.CompareTag("EnemyBullet"))
        {
            
            if (collision.gameObject.CompareTag("Bullet"))
            {
                WorldData.points += points;
            }
            
            Destroy(collision.gameObject);
            DeatroyAndBild();
        }
    }
    public void DeatroyAndBild()
    {
        WorldData.killSmallAsteroids++;
        Destroy(this.gameObject);
    }
}
