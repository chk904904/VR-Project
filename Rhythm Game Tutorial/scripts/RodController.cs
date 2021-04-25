using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RodController : MonoBehaviour
{	
	public GameObject leftRod; 
	public GameObject rightRod;
    public float smooth = 0.005f;

    public bool move_left_down; 
    public bool move_right_down;
    public bool start_over; 
    public bool hold;
    // Start is called before the first frame update
    void Start()
    {
        float ori_y = GameObject.Find("Head").transform.position.y;
        float ori_x = GameObject.Find("Head").transform.position.x;
        float ori_z = GameObject.Find("Head").transform.position.z;
        Vector3 left = new Vector3(ori_x + 0.687f, ori_y + 0.26f, ori_z - 0.188f);
        Vector3 right = new Vector3(ori_x - 0.803f, ori_y + 0.24f, ori_z - 0.188f);
        //Debug.Log(leftRod.transform.position);
        //Debug.Log(rightRod.transform.position);
    	leftRod.transform.position = left;
    	rightRod.transform.position = right;
        leftRod.transform.rotation = Quaternion.Euler(180, 0, 0);
        rightRod.transform.rotation = Quaternion.Euler(180, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(start_over){
            start_over = false; 
            leftRod.transform.rotation = Quaternion.Euler(180, 0, 0);
            rightRod.transform.rotation = Quaternion.Euler(180, 0, 0);
        }
        else{
            if(move_left_down){
                Quaternion target;
                if(hold){
                    target = Quaternion.Euler(190, 0, 0);
                }
                else{
                    target = Quaternion.Euler(285, 0, -90);
                }
                leftRod.transform.rotation = Quaternion.Slerp(leftRod.transform.rotation, target,  Time.deltaTime * smooth);
            }

            if(move_right_down){
                Quaternion target;
                if(hold){
                    target = Quaternion.Euler(190, 0, 0);
                }
                else{
                    target = Quaternion.Euler(285, 0, 90);
                }
                rightRod.transform.rotation = Quaternion.Slerp(rightRod.transform.rotation, target,  Time.deltaTime * smooth);
            }

        }
    }
}