using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;
using UnityEngine.Events;
using UnityEngine.UI;
using System;

public class TutorialController : MonoBehaviour
{
    public Text text;
    public Text combo;
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
        anim.SetBool("isHandsUp", false);
        anim.SetBool("istart", false);
        anim.SetBool("isChinDown", false);
        changeToChinDown = false;
        changeToHandsUp = false;
        initLeftPos = leftHand.transform.position;
        initRightPos = rightHand.transform.position;
        transform.Rotate(0, 180, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
        combo.text = finalRightPos.y.ToString() + " " + initRightPos.y.ToString();
        if (startSample)
        {
            if (frameCtr == 0)
            {
                initLeftPos = leftHand.transform.position;
                initRightPos = rightHand.transform.position;
            }
            frameCtr++;
            if (frameCtr == frameNum)
            {
                finalLeftPos = leftHand.transform.position;
                finalRightPos = rightHand.transform.position;
                startSample = false;
                frameCtr = 0;
            }
        }

        if (ctr < 4)
        {
            if (ctr == 0)
            {
                text.text = "0";
            }
            else if (ctr == 1)
            {
                text.text = "1";
                //transform.rotation = Quaternion.Euler(0,90f,0);
                //anim.SetBool("isChinDown", true);
            }
            else if (ctr == 2)
            {
                text.text = "2";
                transform.rotation = Quaternion.Euler(0, 180f, 0);
                //anim.SetBool("isHandsUp", true);
            }
            else
            {
                text.text = "4";
                transform.rotation = Quaternion.Euler(0, 270f, 0);
                //anim.SetBool("isStart", true);
                ctr = 4;
            }

            if (ctr == 0)
            {
                ctr = 1;
            }
            else if (ctr == 1)
            {
                startSample = true;
                if (checkHandsUp())
                {
                    ctr = 2;
                    initLeftPos = finalLeftPos;
                    initRightPos = finalRightPos;
                }
            }
            else if (ctr == 2)
            {
                startSample = true;
                if (checkHandsUp())
                {
                    ctr = 3;
                    initLeftPos = finalLeftPos;
                    initRightPos = finalRightPos;
                }
            }
        }
    }

    bool checkHandsUp()
    {
        if ((finalLeftPos.y - initLeftPos.y >= 0.5f) && (finalRightPos.y - initRightPos.y >= 0.5f))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
