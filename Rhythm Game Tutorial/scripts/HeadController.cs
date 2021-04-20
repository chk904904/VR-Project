using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;
using UnityEngine.Events;
using UnityEngine.UI;
using System;
using System.Text;
using Random=UnityEngine.Random;

public class HeadController : MonoBehaviour
{
    public GameObject headAnchor;
    public GameObject txt; 

    private bool rodCol = false; 
    private bool faceCol = false; 
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MeshRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = headAnchor.transform.position;
    }

	void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Rods")){
        	string ori_txt = txt.GetComponent<Text>().text; 
            txt.GetComponent<Text>().text = ori_txt + "Rod Collision!";
            rodCol = true;
        }
        if(other.gameObject.CompareTag("Faces")){
        	string ori_txt = txt.GetComponent<Text>().text; 
        	txt.GetComponent<Text>().text = ori_txt + "Face Collision!";
        	faceCol = true;
        }
    }

    void OnTriggerExit(Collider other){
        if(other.gameObject.CompareTag("Rods")){
        	rodCol = false;
        	if(!rodCol && !faceCol){
            	txt.GetComponent<Text>().text = "No Collition";
            }
            if(rodCol && !faceCol){
            	txt.GetComponent<Text>().text = "Rod Collition! No face collision!";
            }
            if(!rodCol && faceCol){
            	txt.GetComponent<Text>().text = "No rod Collition! Face collision!";
            }
            if(rodCol && faceCol){
            	txt.GetComponent<Text>().text = "Rod Collition! Face collision!";
            }
        }
        if(other.gameObject.CompareTag("Faces")){
            //txt.GetComponent<Text>().text = "No Collition";
            faceCol = false; 
            if(!rodCol && !faceCol){
            	txt.GetComponent<Text>().text = "No Collition";
            }
            if(rodCol && !faceCol){
            	txt.GetComponent<Text>().text = "Rod Collition! No face collision!";
            }
            if(!rodCol && faceCol){
            	txt.GetComponent<Text>().text = "No rod Collition! Face collision!";
            }
            if(rodCol && faceCol){
            	txt.GetComponent<Text>().text = "Rod Collition! Face collision!";
            }


     		GameObject.Find("Left_pivot").GetComponent<RodController>().start_over = true;
            float rand_num = Random.Range(-10.0f, 10.0f);
			if(rand_num >= 0.0f){
				GameObject.Find("Left_pivot").GetComponent<RodController>().move_right_down = true;
				GameObject.Find("Left_pivot").GetComponent<RodController>().move_left_down = false;
			}
			else{
				GameObject.Find("Left_pivot").GetComponent<RodController>().move_left_down = true;
				GameObject.Find("Left_pivot").GetComponent<RodController>().move_right_down = false;
			}
        }
    }

}
