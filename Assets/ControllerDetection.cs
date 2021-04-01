using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ControllerDetection : MonoBehaviour
{
    public Text punch;
    public Text comboName;
    public Text timer;
    public Material[] bagMaterials = new Material[9];
    public GameObject punchingBag;
    public AudioClip[] audios = new AudioClip[11];
    public GameObject rightHandModel;
    public GameObject leftHandModel;
    public List<Vector3> trajectoryPosition = new List<Vector3>();
    public List<Quaternion> trajectoryRotation = new List<Quaternion>();
    public GameObject videoScreen;
    public GameObject jabvideoPlayer;
    public GameObject hookvideoPlayer;
    public GameObject uppercutvideoPlayer;



    private GloveFollowing GF = null;
    private Vector3 enterPosition = Vector3.zero;
    private Vector3 exitPosition = Vector3.zero;
    private Animator anim;
    private int curPunching = 0;
    private int curCombo = 0;
    private AudioSource ac;
    private bool isHitted = false;
    private int tutorialIndex = 1;
    private bool isPlayingVideo = false;
    private int status = 1;
    private float timerCounter = 0f;
    private StringBuilder sb = new StringBuilder();

    private int[][] combos = {
        new int[]{1,2,3},
        new int[]{1,2,3,3},
        new int[]{1,4,1},
        new int[]{1,4,4,1},
        new int[]{2,1,4},
        new int[]{2,1,4,4}
    };
    private int[][] testCombos = {
        new int[]{1,2,4},
        new int[]{1,2,4,4},
        new int[]{1,3,2},
        new int[]{1,3,3,2},
        new int[]{1,2,3},
        new int[]{1,2,3,3},
        new int[]{2,3,2},
        new int[]{2,3,3,2},
        new int[]{2,1,4},
        new int[]{2,1,4,4},
        new int[]{1,4,1},
        new int[]{1,4,4,1},
        new int[]{1,6,3,2},
        new int[]{1,6,3,3,2},
        new int[]{1,2,3,2},
        new int[]{1,2,3,3,2}
    };
    private string[] instructions = {
        "Repeat! 1 - 2 - 3",
        "Next! 1 - 2 - 3 - 3",
        "Repeat! 1 - 4 - 1",
        "Next! 1 - 4 - 4 - 1",
        "Repeat! 2 - 1 - 4",
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
        comboName.text = "";
        punch.text = "";
        ac = this.GetComponentInChildren<AudioSource>();
        anim = this.GetComponent<Animator>();
        StartCoroutine(PlayVideo(5f));

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
                StartCoroutine(TriggerVibration(GF.m_controller));
                switch (status)
                {
                    case 1:
                        tutorialPunchDetection(hitValue);
                        break;
                    case 2:
                        practicePunchDetection(hitValue);
                        break;
                    case 3:
                        testPunchDetection(hitValue);
                        break;
                    default:
                        StartCoroutine(BagBouncing(hitValue, true, 0));
                        break;
                }
            }
            isHitted = false;
            GF = null;
        }
 
    }
 
    IEnumerator startTimer()
    {
        while (status == 3)
        {
            yield return new WaitForSeconds(0.1f);
            timerCounter += 0.1f;
            timer.text = timerCounter.ToString("f1") + "s";
        }
    }

    private void tutorialPunchDetection(int punchingNumber)
    {
        if (isPlayingVideo)
        {
            StartCoroutine(BagBouncing(punchingNumber, true, 0));
        }
        else
        {
            if (punchingNumber == tutorialIndex)
            {
                switch (tutorialIndex)
                {
                    case 2:
                        ac.PlayOneShot(audios[4], 2.0f);
                        punch.text = "Great!";
                        break;
                    case 4:
                        ac.PlayOneShot(audios[6], 2.0f);
                        punch.text = "Awesome!";
                        break;
                    case 6:
                        ac.PlayOneShot(audios[5], 2.0f);
                        punch.text = "Great!";
                        comboName.text = "Now Let's try Combos";
                        break;
                }
                if(tutorialIndex == 6)
                {
                    StartCoroutine(BagBouncing(tutorialIndex, true, 0));
                    StartCoroutine(startPractice());
                    status = 0;
                } else
                {
                    tutorialIndex++;
                    if (tutorialIndex == 3 || tutorialIndex == 5)
                    {
                        StartCoroutine(BagBouncing(tutorialIndex - 1, true, 0));
                        StartCoroutine(PlayVideo(5.0f));
                    } else
                    {
                        StartCoroutine(BagBouncing(tutorialIndex - 1, true, tutorialIndex));
                    }
                    
                }
                
            } else
            {
                StartCoroutine(BagBouncing(punchingNumber, false, tutorialIndex));
            }
        }
    }

    IEnumerator startPractice()
    {
        yield return new WaitForSeconds(2.0f);
        status = 2;
        comboName.text = instructions[0];
        punch.text = "";
        punchingBag.GetComponent<Renderer>().material = bagMaterials[combos[0][0]];
    }
    private void practicePunchDetection(int punchingNumber)
    {
        
        if (combos[curCombo][curPunching] == punchingNumber)
        {
            curPunching++;
            if (curPunching == combos[curCombo].Length)
            {
                curPunching = 0;
                switch (curCombo)
                {
                    case 0:
                        ac.PlayOneShot(audios[5], 2.0f);
                        punch.text = "Great!";
                        comboName.text = instructions[curCombo + 1];
                        StartCoroutine(BagBouncing(punchingNumber, true, combos[curCombo + 1][0]));
                        break;
                    case 1:
                        ac.PlayOneShot(audios[6], 2.0f);
                        punch.text = "Awesome!";
                        comboName.text = instructions[curCombo+1];
                        StartCoroutine(BagBouncing(punchingNumber, true, combos[curCombo + 1][0]));
                        break;
                    case 2:
                        ac.PlayOneShot(audios[4], 2.0f);
                        punch.text = "Great!";
                        comboName.text = instructions[curCombo + 1];
                        StartCoroutine(BagBouncing(punchingNumber, true, combos[curCombo + 1][0]));
                        break;
                    case 3:
                        ac.PlayOneShot(audios[6], 2.0f);
                        punch.text = "Awesome!";
                        comboName.text = instructions[curCombo + 1];
                        StartCoroutine(BagBouncing(punchingNumber, true, combos[curCombo + 1][0]));
                        break;
                    case 4:
                        ac.PlayOneShot(audios[7], 2.0f);
                        punch.text = "";
                        comboName.text = "What's the next combo?";
                        StartCoroutine(BagBouncing(punchingNumber, true, 0));
                        break;
                    case 5:
                        ac.PlayOneShot(audios[6], 2.0f);
                        punch.text = "Awesome!";
                        comboName.text = "";
                        StartCoroutine(BagBouncing(punchingNumber, true, 0));
                        StartCoroutine(startTest());
                        break;
                }
                curCombo++;
            } 
            else
            {
                if (curCombo != 5)
                {
                    punch.text = "";
                    StartCoroutine(BagBouncing(punchingNumber, true, combos[curCombo][curPunching]));
                } else
                {
                    punch.text = "";
                    StartCoroutine(BagBouncing(punchingNumber, true, 0));
                }
                
            }
        }
        else
        {
            curPunching = 0;
            punch.text = "Wrong Punch!";
            if (curCombo != 5)
            {
                StartCoroutine(BagBouncing(punchingNumber, false, combos[curCombo][curPunching]));
            }
            else
            {
                StartCoroutine(BagBouncing(punchingNumber, false, 0));
            }
        }

    }


    IEnumerator startTest()
    {
        yield return new WaitForSeconds(1.0f);
        comboName.text = "Try to complete all the combos" + Environment.NewLine + " as fast as you can!";
        punch.text = "";
        curCombo = 0;
        curPunching = 0;
        timer.gameObject.SetActive(true);
        timer.text = "3";
        yield return new WaitForSeconds(1.0f);
        timer.text = "2";
        yield return new WaitForSeconds(1.0f);
        timer.text = "1";
        yield return new WaitForSeconds(1.0f);
        status = 3;
        ac.PlayOneShot(audios[8], 2.0f);
        punch.text = "Follow the Combo!";
        comboName.text = "1 - 2 - 4";
        StartCoroutine(startTimer());
        punchingBag.GetComponent<Renderer>().material = bagMaterials[testCombos[0][0]];
    }


    private void testPunchDetection(int punchingNumber)
    {
        if (testCombos[curCombo][curPunching] == punchingNumber)
        {
            curPunching++;
            if (curPunching == testCombos[curCombo].Length)
            {
                curPunching = 0;
                if(curCombo + 1 < testCombos.Length)
                {
                    switch (curCombo % 2)
                    {
                        case 0:
                            ac.PlayOneShot(audios[5], 2.0f);
                            punch.text = "Great!";
                            comboName.text = "What's next?";
                            StartCoroutine(BagBouncing(punchingNumber, true, 0));
                            break;
                        case 1:
                            ac.PlayOneShot(audios[6], 2.0f);
                            punch.text = "Awesome!";
                            sb.Clear();
                            for (int i = 0; i < testCombos[curCombo + 1].Length; i++)
                            {
                                sb.Append(testCombos[curCombo + 1][i]);
                                if ((i + 1) != testCombos[curCombo + 1].Length)
                                {
                                    sb.Append(" - ");
                                }
                            }
                            comboName.text = sb.ToString();
                            StartCoroutine(BagBouncing(punchingNumber, true, testCombos[curCombo + 1][0]));
                            break;
                    }
                    curCombo++;
                } else
                {
                    ac.PlayOneShot(audios[6], 2.0f);
                    punch.text = "Awesome!";
                    comboName.text = "";
                    StartCoroutine(BagBouncing(punchingNumber, true, 0));
                    StopCoroutine(startTimer());
                    status = 0;
                }
                
                
            }
            else
            {
                if (curCombo % 2 == 0)
                {
                    punch.text = "";
                    StartCoroutine(BagBouncing(punchingNumber, true, testCombos[curCombo][curPunching]));
                }
                else
                {
                    punch.text = "";
                    StartCoroutine(BagBouncing(punchingNumber, true, 0));
                }

            }
        }
        else
        {
            curPunching = 0;
            punch.text = "Wrong Punch!";
            if (curCombo % 2 == 0)
            {
                StartCoroutine(BagBouncing(punchingNumber, false, testCombos[curCombo][curPunching]));
            }
            else
            {
                StartCoroutine(BagBouncing(punchingNumber, false, 0));
            }
        }
    }



    IEnumerator PlayVideo(float duration)
    {
        isPlayingVideo = true;
        comboName.text = "";
        punch.text = "";
        videoScreen.SetActive(true);
        switch (tutorialIndex)
        {
            case 1:
                jabvideoPlayer.SetActive(true);
                break;
            case 3:
                hookvideoPlayer.SetActive(true);
                break;
            case 5:
                uppercutvideoPlayer.SetActive(true);
                break;
        }
        
        yield return new WaitForSeconds(duration);
        switch (tutorialIndex)
        {
            case 1:
                jabvideoPlayer.SetActive(false);
                break;
            case 3:
                hookvideoPlayer.SetActive(false);
                break;
            case 5:
                uppercutvideoPlayer.SetActive(false);
                break;
        }
        videoScreen.SetActive(false);
        isPlayingVideo = false;
        switch (tutorialIndex )
        {
            case 1:
                comboName.text = "1 is Jab(Left Straight) and 2 is Cross(Right Straight)";
                punchingBag.GetComponent<Renderer>().material = bagMaterials[1];
                break;
            case 3:
                comboName.text = "3 is Left Hook and 4 is Right Hook";
                punchingBag.GetComponent<Renderer>().material = bagMaterials[3];
                break;
            case 5:
                comboName.text = "5 is Left Uppercut and 6 is Right Uppercut";
                punchingBag.GetComponent<Renderer>().material = bagMaterials[5];
                break;
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

    public void _TriggerVibration(OVRInput.Controller controller)
    {
        StartCoroutine(TriggerVibration(controller));
    }
    IEnumerator TriggerVibration(OVRInput.Controller controller)
    {
        OVRInput.SetControllerVibration(1f, 1f, controller);
        yield return new WaitForSeconds(0.1f);
        OVRInput.SetControllerVibration(1f, 0f, controller);
    }

    IEnumerator BagBouncing(int punchingNumber, bool isRightPunch, int materialIndex)
    {
        if (isRightPunch)
        {
            punchingBag.GetComponent<Renderer>().material = bagMaterials[7];
        }
        else
        {
            punchingBag.GetComponent<Renderer>().material = bagMaterials[8];
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
        punchingBag.GetComponent<Renderer>().material = bagMaterials[materialIndex];
    }

}
