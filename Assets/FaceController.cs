using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceController : MonoBehaviour
{
	public float beatTempo = 0.06f;

	private Vector3 left; 
	private Vector3 right; 
	private float ori_y; 
	private float ori_x; 
	private float ori_z; 
    // Start is called before the first frame update
    void Start()
    {
    	ori_y = GameObject.Find("Head").transform.position.y;
        ori_x = GameObject.Find("Head").transform.position.x;
        ori_z = GameObject.Find("Head").transform.position.z;
        //left = new Vector3(ori_x + 0.687f, ori_y + 0f, ori_z - 15f);
        //right = new Vector3(ori_x - 0.803f, ori_y + 0f, ori_z - 15f);
        left = new Vector3(ori_x + 0f, ori_y + 0f, ori_z - 15f);
        right = new Vector3(ori_x + 0f, ori_y + 0f, ori_z - 15f);
        transform.position = right;
    }

    // Update is called once per frame
    void Update()
    {
    	if(transform.position.z <= ori_z){
    		transform.position += new Vector3(0f, 0f, (60)*beatTempo * Time.deltaTime);
    	}
        else{
        	float rand_num = Random.Range(-10.0f, 10.0f);
			if(rand_num >= 0.0f){
        		transform.position = right;
        	}
        	else{
        		transform.position = left;
        	}
        	GameObject.Find("Left_pivot").GetComponent<RodController>().start_over = true;
            float rand_num_1 = Random.Range(-10.0f, 10.0f);
			if(rand_num_1 >= 0.0f && rand_num < 2.5f){
				GameObject.Find("Left_pivot").GetComponent<RodController>().hold = false; 
				GameObject.Find("Left_pivot").GetComponent<RodController>().move_right_down = true;
				GameObject.Find("Left_pivot").GetComponent<RodController>().move_left_down = false;
			}
			else if(rand_num_1 >= 2.5f && rand_num <= 5.0f){
				GameObject.Find("Left_pivot").GetComponent<RodController>().hold = false; 
				GameObject.Find("Left_pivot").GetComponent<RodController>().move_left_down = true;
				GameObject.Find("Left_pivot").GetComponent<RodController>().move_right_down = false;
			}
			else{
				GameObject.Find("Left_pivot").GetComponent<RodController>().hold = true;
			}
        }
    }
}
