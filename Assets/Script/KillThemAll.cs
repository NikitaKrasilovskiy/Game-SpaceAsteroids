using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillThemAll : MonoBehaviour
{
    [SerializeField] private Rigidbody2D silentBullet;
    private AudioSource au;
    private GameObject[] enemy;
    private GameObject[] asteroids;
    
    private Collider2D col;
    private AudioSource boomAu;
    // Start is called before the first frame update
    void Start()
    {
        au = GetComponent<AudioSource>();
        
        col = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            au.Play();
            col.enabled = false;
            StartCoroutine(Alliluya());
            boomAu = GameObject.FindGameObjectWithTag("boom").GetComponent<AudioSource>();
        }
    }

    IEnumerator Alliluya()
    {
        yield return new WaitForSeconds(1.5f);
       
        asteroids = GameObject.FindGameObjectsWithTag("Asteroid");

        if (asteroids != null)
        {
            for (int i = 0; i < asteroids.Length; i++)
            {
                if (asteroids[i].GetComponent<Asteroid>() != null)
                { 
                    asteroids[i].GetComponent<Asteroid>().DeatroyAndBild(); 
                }
                else 
                {
                    asteroids[i].GetComponent<SmallAsteroids>().DeatroyAndBild();
                }
            }
            Destroy(this.gameObject);
            boomAu.Play();
        }        
    }
}
