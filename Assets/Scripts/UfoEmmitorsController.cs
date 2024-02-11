using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UfoEmmitorsController : MonoBehaviour
{
    [SerializeField] private RandomEmmitorUfo emmitorLeft, emmitorRight;
    [SerializeField] private float timer = 20;
    private float timeUp;
    // Start is called before the first frame update
    void Start()
    {
        emmitorLeft.needEmmitor = false;
        emmitorRight.needEmmitor = false;
        timeUp = 0;
        WorldData.needUfo = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (WorldData.needUfo == true)
        {
            timeUp += Time.deltaTime;
        }
        if (timeUp >= timer)
        {
            int rand = Random.Range(0, 2);
            WorldData.needUfo = false;
            if (rand == 0)
            {
                emmitorLeft.needEmmitor = true;
            }
            else
            {
                emmitorRight.needEmmitor = true;
            }
            timeUp = 0;
        }
    }
}
