﻿using System.Collections;
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
    // Start is called before the first frame update
    void Start()
    {
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
                tutorialHandsUp();
                if(changeToHandsUp){
                    ctr = 1;
                }
            }
            else if(ctr == 1 ){
                tutorialHandsUp();
                if(changeToHandsUp){
                    ctr = 2;
                }
            }
            else if(ctr == 2){
                switchToStart();
                ctr = 3;
            }
        }
    }

    
    void switchToStart(){
        StartCoroutine(waitToStart());
    }

    void tutorialHandsUp(){
        initLeftPos = leftHand.transform.position; 
        initRightPos = rightHand.transform.position;
        StartCoroutine(handsUpChecking());
        if((finalLeftPos.y > initLeftPos.y)&&(finalRightPos.y > initRightPos.y)){
            changeToHandsUp = true;
        }
        else{
            changeToHandsUp =  false;
        }
    }

    IEnumerator handsUpChecking(){
        yield return new WaitForSeconds(1.5f);
        finalLeftPos = leftHand.transform.position; 
        finalRightPos = rightHand.transform.position;
    }

    IEnumerator waitToStart(){
        yield return new WaitForSeconds(2.5f);
    }

}
