using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;
using UnityEngine.Events;
using UnityEngine.UI;
using System;

public class ScoreCounter : MonoBehaviour
{
    public UnityEvent defaultEvent;
    public Text score;
    public Text comboName;
    public Animator anim;
    public Material[] bagMaterials = new Material[9];
    public GameObject punchingBag;
    public AudioClip[] audios = new AudioClip[4];

    private int punchingNumber;
    private int curPunching = 0;
    private int curCombo = 0;
    private AudioSource ac;
    private int[][] combos = {
        new int[]{1,2,3},
        new int[]{1,2,3,3},
        new int[]{1,4,1},
        new int[]{1,4,4,1},
        new int[]{2,1,4},
        new int[]{2,1,4,4}
    };
    private string[] instructions = { 
        "Repeat! 1 - 2 - 3",
        "Next! 1 - 2 - 3 - 3",
        "Repeat! 1 - 4 - 1",
        "Next! 1 - 4 - 4 - 1",
        "Repeat! 2 - 1 -4",
        "What's the next combo?"
    };


    // Start is called before the first frame update
    void Start()
    {
        punchingBag.GetComponent<Renderer>().material = bagMaterials[combos[curCombo][curPunching]];
        comboName.text = instructions[curCombo];
        GetComponent<ButtonController>().InteractableStateChanged.AddListener(InitiateEvent);
        ac = this.GetComponentInChildren<AudioSource>();
    }

    void InitiateEvent(InteractableStateArgs state)
    {

        if (state.NewInteractableState == InteractableState.ActionState)
        {
            
            StartCoroutine(BagBouncing(state));
        }
        else
        {
            defaultEvent.Invoke();
        }
    }
    private int punchingDetect(InteractableStateArgs state)
    {
        float x = System.Math.Abs(state.Tool.Velocity.x);
        float y = System.Math.Abs(state.Tool.Velocity.y);
        float z = System.Math.Abs(state.Tool.Velocity.z);

        if (z > x && z > y)
        {
            if (state.Tool.IsRightHandedTool)
            {
                return 2;
            }
            else
            {
                return 1;
            }

        }
        else if (x > y && x > z)
        {
            if (state.Tool.IsRightHandedTool)
            {
                return 4;
            }
            else
            {
                return 3;
            }
        }
        else
        {
            if (state.Tool.IsRightHandedTool)
            {
                return 6;
            }
            else
            {
                return 5;
            }
        }
    }

    private int numberDetect(float x, float y)
    {
        if(x > 0.41 && x < 0.51 && y < 1.23 && y > 1.07)
        {
            return 1;
        }
        if (x > 0.31 && x < 0.41 && y < 1.23 && y > 1.07)
        {
            return 2;
        }
        if (x > 0.47 && x < 0.59 && y < 1.1 && y > 0.94)
        {
            return 3;
        }
        if (x > 0.27 && x < 0.39 && y < 1.1 && y > 0.94)
        {
            return 4;
        }
        if (x > 0.41 && x < 0.51 && y < 1.00 && y > 0.84)
        {
            return 5;
        }
        if (x > 0.31 && x < 0.41 && y < 1.00 && y > 0.84)
        {
            return 6;
        }
        return 0;
    }
    // Update is called once per frame

    IEnumerator BagBouncing(InteractableStateArgs state)
    {
        punchingNumber = punchingDetect(state);
        float x = System.Math.Abs(state.Tool.InteractionPosition.x);
        float y = System.Math.Abs(state.Tool.InteractionPosition.y);
        if(curCombo < combos.Length)
        {
            if (combos[curCombo][curPunching] == numberDetect(x, y))
            {
                punchingBag.GetComponent<Renderer>().material = bagMaterials[7];
                curPunching++;
                comboName.text = instructions[curCombo];
            }
            else
            {
                curPunching = 0;
                punchingBag.GetComponent<Renderer>().material = bagMaterials[8];
                comboName.text = "Wrong Punch!" + Environment.NewLine + "Restart combo!";
            }
            if (curPunching == combos[curCombo].Length)
            {
                curPunching = 0;
                curCombo++;
                if (curCombo < combos.Length)
                {
                    comboName.text = instructions[curCombo];
                }
                else
                {
                    comboName.text = "Nice Job!";
                }
            }
        }
        
        
        switch (punchingNumber)
        {
            case 1:
                anim.SetBool("jab", true);
                ac.PlayOneShot(audios[0], 1.0f);
                yield return new WaitForSeconds(0.2f);
                anim.SetBool("jab", false);
                break;
            case 2:
                anim.SetBool("cross", true);
                ac.PlayOneShot(audios[0], 1.0f);
                yield return new WaitForSeconds(0.2f);
                anim.SetBool("cross", false);
                break;
            case 3:
                anim.SetBool("hook", true);
                ac.PlayOneShot(audios[1], 1.0f);
                yield return new WaitForSeconds(0.2f);
                anim.SetBool("hook", false);
                break;
            case 4:
                anim.SetBool("hook", true);
                ac.PlayOneShot(audios[2], 1.0f);
                yield return new WaitForSeconds(0.2f);
                anim.SetBool("hook", false);
                break;
            case 5:
                anim.SetBool("uppercut", true);
                ac.PlayOneShot(audios[3], 1.0f);
                yield return new WaitForSeconds(0.2f);
                anim.SetBool("uppercut", false);
                break;
            case 6:
                anim.SetBool("uppercut", true);
                ac.PlayOneShot(audios[3], 1.0f);
                yield return new WaitForSeconds(0.2f);
                anim.SetBool("uppercut", false);
                break;
            default:
                break;
        }
        if (curCombo < combos.Length - 1)
        {
            punchingBag.GetComponent<Renderer>().material = bagMaterials[combos[curCombo][curPunching]];
        } else
        {
            punchingBag.GetComponent<Renderer>().material = bagMaterials[0];
        }
        
    }

}
