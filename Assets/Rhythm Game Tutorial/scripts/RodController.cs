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
	private int ctr; 
    // Start is called before the first frame update
    void Start()
    {
        xLeftAngle = 75; 
        yLeftAngle = -30;
        xRightAngle = 75;
        yRightAngle = 45; 
    }

    // Update is called once per frame
    void Update()
    {
    	ctr = ctr + 1; 
        leftRod.transform.Rotate(ctr%xLeftAngle, ctr%yLeftAngle,0);
        rightRod.transform.Rotate(ctr%xRightAngle, ctr%yRightAngle,0);
    }
}
