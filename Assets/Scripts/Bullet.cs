using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Camera _cam;
    private Vector2 min;
    private Vector2 max;
    // Start is called before the first frame update
    void Start()
    {
        _cam = Camera.main;
        min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > max.x || transform.position.x < min.x || transform.position.y > max.y || transform.position.y < min.y)
        {
            Destroy(this.gameObject);
        }
        
    }
}
