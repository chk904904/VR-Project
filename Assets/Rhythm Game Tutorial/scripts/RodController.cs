using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RodController : MonoBehaviour
{	
	public GameObject leftRod; 
	public GameObject rightRod;
	public int xLeftAngle; 
	public int yLeftAngle; 
	public int xRightAngle; 
	public int yRightAngle;
	private float ctr; 
	public float rotateAmount;
	private float xlCtr; 
   	private float ylCtr; 
    private float xrCtr; 
    private float yrCtr;
    private int xl; 
    private int yl; 
    private int xr; 
    private int yr;
    // Start is called before the first frame update
    void Start()
    {
        xLeftAngle = 75; 
        yLeftAngle = 0;
        xRightAngle = 75;
        yRightAngle = 0; 
        xlCtr = 0.0f; 
   		ylCtr = 0.0f; 
    	xrCtr = 0.0f; 
    	yrCtr = 0.0f;
    	xl = -1; 
    	yl = -1; 
    	xr = -1; 
    	yr = -1;
    	Vector3 temp = new Vector3(0,2.7f,0);
        float ori_y = GameObject.Find("Head").transform.position.y;
        float ori_x = GameObject.Find("Head").transform.position.x;
        float ori_z = GameObject.Find("Head").transform.position.z;
        Vector3 left = new Vector3(ori_x + 0.22f, ori_y - 0.1f, ori_z - 0.6f);
        Vector3 right = new Vector3(ori_x - 0.38f, ori_y - 0.1f, ori_z - 0.6f);
        //Debug.Log(leftRod.transform.position);
        //Debug.Log(rightRod.transform.position);
    	leftRod.transform.position = left;
    	rightRod.transform.position = right;
    }

    // Update is called once per frame
    void Update()
    {
    	xlCtr = xlCtr + xl * rotateAmount*Time.deltaTime;
    	//ylCtr = ylCtr  + yl * rotateAmount*Time.deltaTime;
    	xrCtr = xrCtr + xr * rotateAmount*Time.deltaTime;
    	//yrCtr = yrCtr  + yr * rotateAmount*Time.deltaTime;    	
    	if(xlCtr > xLeftAngle){
    		xlCtr = 0;
    		xl = xl * (-1);
    	} 
    	// ylCtr = ctr; 
    	// if(ylCtr > yLeftAngle){
    	// 	ylCtr = 0; 
    	// 	yl = yl * (-1);
    	// }
    	xrCtr = ctr; 
    	if(xrCtr > xRightAngle){
    		xrCtr = 0;
    		xr = xr * (-1);
    	}
    	// yrCtr = ctr;
    	// if(yrCtr > yRightAngle){
    	// 	yrCtr = 0;
    	// 	yr = yr * (-1);
    	// }
        leftRod.transform.Rotate(xlCtr, 0,0);
        rightRod.transform.Rotate(-xlCtr, 0,0);
    }
}
