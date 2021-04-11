using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;
using UnityEngine.Events;
using UnityEngine.UI;
using System;
using System.Text;

public class TutorialController : MonoBehaviour
{
    public GameObject punchbag;
    public Text text;
    public Text combo;
    public GameObject leftHand;
    public GameObject rightHand;
    public GameObject head;
    public AudioClip[] audios = new AudioClip[3];
    public GameObject startGlove;


    private int ctr = 0;
    private Vector3 initLeftPos;
    private Vector3 initRightPos;
    private Vector3 finalLeftPos;
    private Vector3 finalRightPos;
    private Vector3 initChin; 
    private Vector3 finalChin;
    private bool changeToHandsUp;
    private bool changeToChinDown;
    private bool startChinSample;
    
    private AudioSource ac;
    private int frameCtr = 0;
    private int frameChinCtr = 0;
    private int frameNum = 41;
    private bool startSample;
    // Start is called before the first frame update
    void Start()
    {
        startSample = false;
        startChinSample = false;
        changeToChinDown = false;
        changeToHandsUp = false;
        initLeftPos = leftHand.transform.position;
        initRightPos = rightHand.transform.position;
        transform.Rotate(0, 180, 0);
        ac = this.GetComponentInChildren<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
        
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

        if (startChinSample)
        {
            if (frameChinCtr == 0)
            {
                initChin = head.transform.position;
                finalChin = head.transform.position;
            }
            frameChinCtr++;
            if (frameChinCtr == frameNum)
            {
                finalChin = head.transform.position;
                //startChinSample = false;
                frameChinCtr = 0;
            }
        }

        if (ctr < 4)
        {
            if (ctr == 0)
            {
                //text.text = "0";
            }
            else if (ctr == 1)
            {
                //text.text = "1";
                transform.rotation = Quaternion.Euler(0, 90f, 0);
                //transform.rotation = Quaternion.Euler(0,90f,0);
                //anim.SetBool("isChinDown", true);
            }
            else if (ctr == 2)
            {
                //text.text = "2";
                transform.rotation = Quaternion.Euler(0, 0, 0);
                
                //transform.rotation = Quaternion.Euler(0, 180f, 0);
                //anim.SetBool("isHandsUp", true);
            }
            else
            {
                //text.text = "4";
                //transform.rotation = Quaternion.Euler(0, 270f, 0);
                transform.rotation = Quaternion.Euler(0, 180f, 0);
                //anim.SetBool("isStart", true);
                ctr = 4;
                
                StartCoroutine(activateStartGlove());

            }
           
            if (ctr == 0)
            {
                ctr = 1;
            }
            else if (ctr == 1)
            {
                startChinSample = true;
                if (checkChinDown())
                {
                    StartCoroutine(great());
                    ctr = 2;
                    initChin = finalChin;
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
    IEnumerator great()
    {
        ac.PlayOneShot(audios[0], audios[0].length);
        yield return new WaitForSeconds(audios[0].length);
    }

    IEnumerator activateStartGlove()
    {
        ac.PlayOneShot(audios[1], audios[1].length);
        yield return new WaitForSeconds(audios[1].length);
        ac.PlayOneShot(audios[2], audios[2].length);
        yield return new WaitForSeconds(audios[2].length);
        startGlove.SetActive(true);
    }

    
    bool checkHandsUp()
    {
        if ((finalLeftPos.y - finalChin.y >= 0.1f) && (finalRightPos.y - finalChin.y >= 0.1f))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    bool checkChinDown()
    {
        //combo.text = finalChin.y.ToString() + " " + initChin.y.ToString();
        if (finalChin.y - initChin.y <= -0.05f)
        {
            return true;
        }
        else{
            return false;
        }
    }

}
