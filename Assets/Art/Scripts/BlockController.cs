using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
	public float block_speed; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    	block_speed = -0.1f;
    	Debug.Log(block_speed);
        transform.position += new Vector3(0f, 0f, block_speed);
    }
}
