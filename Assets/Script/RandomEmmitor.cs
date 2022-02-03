using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEmmitor : MonoBehaviour
{
    Camera _cam;
    private Vector2 min;       
    private Vector2 max;
    private float RandX;
    private bool needEmmitor;
    [SerializeField] private Rigidbody2D bigAsteroid;
    [SerializeField] private float maxSpeedAsteroids, minSpeedAsteroids;
    private float speedAsteroids;
    Vector2 spawn;
    // Start is called before the first frame update
    void Start()
    {
        _cam = Camera.main;
        min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        needEmmitor = true;
    }

    // Update is called once per frame
    void Update()
    {      

        if (WorldData.killSmallAsteroids >= WorldData.needToKill)
        {
            needEmmitor = true;
            WorldData.resAsteroids ++;
            WorldData.needToKill += 4;
            WorldData.killSmallAsteroids = 0;
        }
        
        

        if (needEmmitor == true)
        {
            for (int i = 0; i <= WorldData.resAsteroids-1; i++)
            {            
                
                RandX = Random.Range(min.x * 0.8f, max.x * 0.8f);
                spawn = new Vector2(RandX, -1);
                speedAsteroids = Random.Range(minSpeedAsteroids, maxSpeedAsteroids);
                Rigidbody2D asteroid = Instantiate(bigAsteroid, spawn, Quaternion.identity) as Rigidbody2D;
                asteroid.velocity = -1 * transform.up * speedAsteroids;
            }
            needEmmitor = false;
        }
    }
}
