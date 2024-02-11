using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceMove : MonoBehaviour
{
    public float speed;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {        
        if (this.transform.position.y <= -6.9f)
        {           
            this.transform.position = new Vector3(0,7,1);
        }
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, transform.position.y-10),Time.deltaTime*speed);
    }
}
