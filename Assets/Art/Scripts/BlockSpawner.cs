using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
	public float init_z; 
	public float final_z; 

	public GameObject face_middle_block;
	public GameObject face_left_block; 
	public GameObject face_right_block;

	private Vector3 left; 
	private Vector3 middle; 
	private Vector3 right;
	private float speed; 
	private GameObject cur_block; 


    // Start is called before the first frame update
    void Start()
    {
    	init_z = GameObject.Find("left_anchor").transform.position.z;
    	Debug.Log("init_z"+init_z);
    	final_z = GameObject.Find("Head").transform.position.z;
    	Debug.Log("final_z" + final_z);
    	speed = (final_z - init_z)/30;
    	//TODO: set left, middle, right position   
    	left = GameObject.Find("left_anchor").transform.localPosition;
    	GameObject.Find("left_anchor").GetComponent<MeshRenderer>().enabled = false;
    	middle = GameObject.Find("middle_anchor").transform.localPosition;
    	GameObject.Find("middle_anchor").GetComponent<MeshRenderer>().enabled = false;
    	right = GameObject.Find("right_anchor").transform.localPosition;
    	GameObject.Find("right_anchor").GetComponent<MeshRenderer>().enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void spawn_left(int typeIndex){
    	if(typeIndex == 0){
			GameObject.Find("middlePadL").transform.localPosition = left;
			GameObject.Find("middlePadL").transform.localPosition += new Vector3(0f, 0f, 0f);
		}
		else if(typeIndex == 1){
			GameObject.Find("leftPadL").transform.localPosition = left;
			GameObject.Find("leftPadL").transform.localPosition += new Vector3(0f, 0f, 0f);
		}
		else if(typeIndex == 2){
			GameObject.Find("rightPadL").transform.localPosition = left;
			GameObject.Find("rightPadL").transform.localPosition += new Vector3(0f, 0f, 0f);
		}
	}

	public void spawn_middle(int typeIndex){
    	if(typeIndex == 0){
			GameObject.Find("middlePadM").transform.localPosition = middle;
			GameObject.Find("middlePadM").transform.localPosition += new Vector3(0f, 0f, 0f);
		}
		else if(typeIndex == 1){
			GameObject.Find("leftPadM").transform.localPosition = middle;
			GameObject.Find("leftPadM").transform.localPosition += new Vector3(0f, 0f, 0f);

		}
		else if(typeIndex == 2){
			GameObject.Find("rightPadM").transform.localPosition = middle;
			GameObject.Find("rightPadM").transform.localPosition += new Vector3(0f, 0f, 0f);

		}
	}

	public void spawn_right(int typeIndex){
    	if(typeIndex == 0){
			GameObject.Find("middlePadR").transform.localPosition = right;
			GameObject.Find("middlePadR").transform.localPosition += new Vector3(0f, 0f, 0f);
		}
		else if(typeIndex == 1){
			GameObject.Find("leftPadR").transform.localPosition = right;
			GameObject.Find("leftPadR").transform.localPosition += new Vector3(0f, 0f, 0f);

		}
		else if(typeIndex == 2){
			GameObject.Find("rightPadR").transform.localPosition = right;
			GameObject.Find("rightPadR").transform.localPosition += new Vector3(0f, 0f, 0f);

		}
	}

}
