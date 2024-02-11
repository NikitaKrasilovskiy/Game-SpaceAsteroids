using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEmmitorUfo : MonoBehaviour
{
    Camera _cam;
    private Vector2 min;
    private Vector2 max;
    private float RandY;
    public bool needEmmitor;
    [SerializeField] private Rigidbody2D ufo;
    Vector2 spawn;
    // Start is called before the first frame update
    void Start()
    {
        _cam = Camera.main;
        min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        needEmmitor = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (needEmmitor == true)
        {           
                RandY = Random.Range(min.y * 0.8f, max.y * 0.8f);
                spawn = new Vector2(transform.position.x, RandY);                
                Rigidbody2D asteroid = Instantiate(ufo, spawn, Quaternion.identity) as Rigidbody2D;
                asteroid.velocity = Vector2.zero;
           
            needEmmitor = false;
        }
    }
}
