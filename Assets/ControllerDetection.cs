using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerDetection : MonoBehaviour
{
    public Text punch;
    public Text comboName;
    public Material[] bagMaterials = new Material[9];
    public GameObject punchingBag;
    public AudioClip[] audios = new AudioClip[4];

    private GloveFollowing GF = null;
    private Vector3 enterPosition = Vector3.zero;
    private Vector3 exitPosition = Vector3.zero;
    private Animator anim;
    private int curPunching = 0;
    private int curCombo = 0;
    private AudioSource ac;
    private bool isHitted = false;
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
    private string[] punches = {
        "Not Valid Punch",
        "Jab",
        "Cross",
        "Left Hook",
        "Right Hook",
        "Left Uppercut",
        "Right Uppercut"
    };
    void Start()
    {
        punchingBag.GetComponent<Renderer>().material = bagMaterials[combos[curCombo][curPunching]];
        comboName.text = instructions[curCombo];
        ac = this.GetComponentInChildren<AudioSource>();
        anim = this.GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!isHitted)
        {
            isHitted = true;
            GF = other.gameObject.GetComponent<GloveFollowing>();
            enterPosition = GF.RelativePosition();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isHitted && GF != null && other.gameObject.GetComponent<GloveFollowing>().m_controller.Equals(GF.m_controller))
        {
            exitPosition = GF.RelativePosition();
            int hitValue = PunchRecognition(exitPosition - enterPosition, GF.gameObject.name);
            if (hitValue != 0)
            {
                StartCoroutine(BagBouncing(hitValue));
                punch.text = punches[hitValue];
                StartCoroutine(TriggerVibration(GF.m_controller));
            } else
            {
                punch.text = punches[hitValue];
            }
            isHitted = false;
            GF = null;
        }
    }

    private int PunchRecognition(Vector3 displacement, string name)
    {
        if(Mathf.Abs(displacement.x) > 0.2f && Mathf.Abs(displacement.x) > Mathf.Abs(displacement.y))
        {
            if (name.Equals("LeftGlove"))
            {
                return 3;
            }
            else
            {
                return 4;
            }
        }

        if (Mathf.Abs(displacement.y) > 0.2f && Mathf.Abs(displacement.y) > Mathf.Abs(displacement.x))
        {
            if (name.Equals("LeftGlove"))
            {
                return 5;
            }
            else
            {
                return 6;
            }
        }

        if (displacement.magnitude < 0.2f && displacement.magnitude > 0.01f)
        {
            if (name.Equals("LeftGlove"))
            {
                return 1;
            }
            else
            {
                return 2;
            }

        } else
        {
            return 0;
        }
    }

    IEnumerator TriggerVibration(OVRInput.Controller controller)
    {
        OVRInput.SetControllerVibration(1f, 1f, controller);
        yield return new WaitForSeconds(0.1f);
        OVRInput.SetControllerVibration(1f, 0f, controller);
    }

    IEnumerator BagBouncing(int punchingNumber)
    {
        if (curCombo < combos.Length)
        {
            if (combos[curCombo][curPunching] == punchingNumber)
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
        }
        else
        {
            punchingBag.GetComponent<Renderer>().material = bagMaterials[0];
        }

    }

}
