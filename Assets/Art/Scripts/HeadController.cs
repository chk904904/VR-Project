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
    //public GameObject txt; 
    public bool start_game;

    private bool rodCol = false; 
    private bool faceCol = false; 
    private int ctr_to_start = 10;
    private bool not_started = true;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MeshRenderer>().enabled = false;
        start_game = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(ctr_to_start > 0){
            ctr_to_start -= 1;
        }
        else{
            if(not_started){
                GameObject.Find("Left_pivot").GetComponent<RodController>().startRodPos();
                GameObject.Find("Face").GetComponent<FaceController>().startFacePos();
                start_game = true;
                not_started = false;
            }
        }
        //transform.position = headAnchor.transform.position;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Rods")){
            // string ori_txt = txt.GetComponent<Text>().text; 
            // if(ori_txt != "No Collition"){
            //     txt.GetComponent<Text>().text = ori_txt + "Rod Collision!";
            // }
            // else{
            //     txt.GetComponent<Text>().text = "Rod Collision!";
            // }
            rodCol = true;
        }
        if(other.gameObject.CompareTag("Faces")){
            // string ori_txt = txt.GetComponent<Text>().text; 
            // if(ori_txt != "No Collition"){
            //     txt.GetComponent<Text>().text = ori_txt + "Face Collision!";
            // }
            // else{
            //     txt.GetComponent<Text>().text = "Face Collision!";
            // }
            faceCol = true;
        }
    }

    void OnTriggerExit(Collider other){
        if(other.gameObject.CompareTag("Rods")){
            rodCol = false;
            // if(!rodCol && !faceCol){
            //     txt.GetComponent<Text>().text = "No Collition";
            // }
            // if(rodCol && !faceCol){
            //     txt.GetComponent<Text>().text = "Rod Collition! No face collision!";
            // }
            // if(!rodCol && faceCol){
            //     txt.GetComponent<Text>().text = "No rod Collition! Face collision!";
            // }
            // if(rodCol && faceCol){
            //     txt.GetComponent<Text>().text = "Rod Collition! Face collision!";
            // }
        }
        if(other.gameObject.CompareTag("Faces")){
            //txt.GetComponent<Text>().text = "No Collition";
            faceCol = false; 
            // if(!rodCol && !faceCol){
            //     txt.GetComponent<Text>().text = "No Collition";
            // }
            // if(rodCol && !faceCol){
            //     txt.GetComponent<Text>().text = "Rod Collition! No face collision!";
            // }
            // if(!rodCol && faceCol){
            //     txt.GetComponent<Text>().text = "No rod Collition! Face collision!";
            // }
            // if(rodCol && faceCol){
            //     txt.GetComponent<Text>().text = "Rod Collition! Face collision!";
            // }

        }
    }

}
