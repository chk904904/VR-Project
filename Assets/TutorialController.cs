using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;
using UnityEngine.Events;
using UnityEngine.UI;
using System;

public class TutorialController : MonoBehaviour
{
    public GameObject leftHand; 
    public GameObject rightHand; 
	private int ctr = 0;
    private Vector3 initLeftPos;
    private Vector3 initRightPos;
    private Vector3 finalLeftPos;
    private Vector3 finalRightPos;
    private bool changeToHandsUp; 
    private bool changeToChinDown;
	Animator anim;


    private int frameCtr = 0; 
    private int frameNum = 41;
    private bool startSample;
    // Start is called before the first frame update
    void Start()
    {
        startSample = false; 
        anim = GetComponent<Animator>();
        anim.SetBool("isHandsUp",false);
        anim.SetBool("istart", false);
        anim.SetBool("isChinDown",false);
        changeToChinDown = false; 
        changeToHandsUp = false; 
        initLeftPos = leftHand.transform.position; 
        initRightPos = rightHand.transform.position;
    }

    // Update is called once per frame
    void Update()
    {   
        if(startSample){
            if(frameCtr == 0){
                initLeftPos = leftHand.transform.position; 
                initRightPos = rightHand.transform.position;
            }
            frameCtr ++;
            if(frameCtr == frameNum){
                finalLeftPos = leftHand.transform.position; 
                finalRightPos = rightHand.transform.position;
                startSample = false;
                frameCtr = 0;
            }
        }

        if(ctr < 4){
            if(ctr == 0){
            }
            else if(ctr == 1){
                anim.SetBool("isChinDown",true);
            }
            else if(ctr == 2){
                anim.SetBool("isHandsUp",true);
            }
            else{
                anim.SetBool("isStart", true);
                ctr = 4;
            }

            if(ctr == 0){
            }
            else if(ctr == 1 ){
                startSample = true;
                if(checkHandsUp()){
                    ctr = 2;
                }
            }
            else if(ctr == 2){
                startSample = true;
                if(checkHandsUp()){
                    ctr = 3;
                }
            }
        }
    }

    bool checkHandsUp(){
        if((finalLeftPos.y - initLeftPos.y >= 2)&&(finalRightPos.y - initRightPos.y >= 2)){
            return true;
        }
        else{
            return  false;
        }
    }

}
