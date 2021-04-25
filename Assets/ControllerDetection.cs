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
    public GameObject trainingReminder;
    public GameObject restartTrainingButton;
    public GameObject[] tutorialReminder = new GameObject[3];
    public GameObject restartTutorialButton;




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
    private bool startCount = false;
    private int startCounter = 0;

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
        restartTutorialButton.SetActive(true);
        StartCoroutine(PlayVideo(12f, 1));

    }

    public void restartTutorial()
    {
        tutorialIndex = 1;
        status = 1;
        jabvideoPlayer.SetActive(false);
        tutorialReminder[0].SetActive(false);
        hookvideoPlayer.SetActive(false);
        tutorialReminder[1].SetActive(false);
        uppercutvideoPlayer.SetActive(false);
        tutorialReminder[2].SetActive(false);
        videoScreen.SetActive(false);
        isPlayingVideo = false;
        punchingBag.GetComponent<Renderer>().material = bagMaterials[0];
        StartCoroutine(PlayVideo(12f, 1));
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!isHitted)
        {
            isHitted = true;
            startCount = true;
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
            if (hitValue != 0 && startCounter < 20)
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
            if(startCounter < 20)
            {
                StartCoroutine(halfSecondTimer());
            } else
            {
                isHitted = false;
            }
            
            startCount = false;
            GF = null;
        }
 
    }
    private void Update()
    {
        if (startCount)
        {
            startCounter++;
        }
        else
        {
            startCounter = 0;
        }
    }
    IEnumerator halfSecondTimer()
    {
        yield return new WaitForSeconds(0.2f);
        isHitted = false;
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
            punch.text = "";
            comboName.text = "";
            StartCoroutine(BagBouncing(punchingNumber, true, 0));
        }
        else
        {
            if (punchingNumber == tutorialIndex)
            {
                switch (tutorialIndex)
                {
                    case 2:
                        ac.PlayOneShot(audios[4], audios[4].length);
                        punch.text = "";
                        comboName.text = "";
                        StartCoroutine(BagBouncing(tutorialIndex, true, 0));
                        StartCoroutine(PlayVideo(12f, tutorialIndex + 1));
                        break;
                    case 4:
                        ac.PlayOneShot(audios[6], audios[6].length);
                        punch.text = "";
                        comboName.text = "";
                        StartCoroutine(BagBouncing(tutorialIndex, true, 0));
                        StartCoroutine(PlayVideo(12f, tutorialIndex + 1));
                        break;
                    case 6:
                        ac.PlayOneShot(audios[5], audios[5].length);
                        punch.text = "";
                        comboName.text = "Now Let's try Combos";
                        StartCoroutine(BagBouncing(tutorialIndex, true, 0));
                        restartTutorialButton.SetActive(false);
                        StartCoroutine(startPractice());
                        status = 0;
                        break;
                    default:
                        punch.text = punches[tutorialIndex + 1];
                        comboName.text = (tutorialIndex + 1).ToString();
                        StartCoroutine(BagBouncing(tutorialIndex, true, tutorialIndex + 1));
                        break;

                }
                tutorialIndex++;
            } else
            {
                punch.text = punches[tutorialIndex];
                comboName.text = tutorialIndex.ToString();
                StartCoroutine(BagBouncing(punchingNumber, false, tutorialIndex));
            }
        }
    }

    IEnumerator startPractice()
    {
        yield return new WaitForSeconds(2.0f);
        status = 2;
        generateComboText(0, 0, combos, false);
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
                if (curCombo < 4)
                {
                    ac.PlayOneShot(audios[5 + (curCombo % 2)], audios[5 + (curCombo % 2)].length);
                    generateComboText(0, curCombo + 1, combos, false);
                    StartCoroutine(BagBouncing(punchingNumber, true, combos[curCombo + 1][0]));
                } else if( curCombo == 4)
                {
                    ac.PlayOneShot(audios[7], audios[7].length);
                    generateComboText(0, curCombo + 1, combos, true);
                    StartCoroutine(BagBouncing(punchingNumber, true, combos[curCombo + 1][0]));
                } else
                {
                    ac.PlayOneShot(audios[6], audios[6].length);
                    punch.text = "";
                    comboName.text = "Awesome!";
                    StartCoroutine(BagBouncing(punchingNumber, true, 0));
                    StartCoroutine(startTest());
                }
                curCombo++;
            } 
            else
            {
                if (curCombo != 5)
                {
                    generateComboText(curPunching, curCombo, combos, false);
                    StartCoroutine(BagBouncing(punchingNumber, true, combos[curCombo][curPunching]));
                } else
                {
                    generateComboText(curPunching, curCombo, combos, true);
                    StartCoroutine(BagBouncing(punchingNumber, true, 0));
                    if (getRepeatedIndex(curCombo, combos) != curPunching)
                    {
                        StartCoroutine(BagBouncing(punchingNumber, true, combos[curCombo][curPunching]));
                    }
                    else
                    {
                        StartCoroutine(BagBouncing(punchingNumber, true, 0));
                    }
                }
            }
        }
        else
        {
            curPunching = 0;
            if (curCombo != 5)
            {
                generateComboText(0, curCombo, combos, false);
                StartCoroutine(BagBouncing(punchingNumber, false, combos[curCombo][curPunching]));
            }
            else
            {
                generateComboText(0, curCombo, combos, true);
                StartCoroutine(BagBouncing(punchingNumber, false, 0));
            }


    }

    }

    public void _startTest()
    {
        status = 3;
        timerCounter = 0;
        StartCoroutine(startTest());
    }

    IEnumerator startTest()
    {
        yield return new WaitForSeconds(1.0f);
        trainingReminder.SetActive(true);
        comboName.text = "";
        punch.text = "";
        curCombo = 0;
        curPunching = 0;
        timer.gameObject.transform.parent.gameObject.SetActive(true);
        timer.text = "3";
        yield return new WaitForSeconds(1.0f);
        timer.text = "2";
        yield return new WaitForSeconds(1.0f);
        timer.text = "1";
        yield return new WaitForSeconds(1.0f);
        trainingReminder.SetActive(false);
        status = 3;
        ac.PlayOneShot(audios[8], 2.0f);
        generateComboText(0,0, testCombos, false);
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
                            generateComboText(0, curCombo + 1, testCombos, true);
                            StartCoroutine(BagBouncing(punchingNumber, true, testCombos[curCombo + 1][0]));
                            break;
                        case 1:
                            ac.PlayOneShot(audios[6], 2.0f);
                            generateComboText(0, curCombo + 1, testCombos,false);
                            StartCoroutine(BagBouncing(punchingNumber, true, testCombos[curCombo + 1][0]));
                            break;
                    }
                    curCombo++;
                } else
                {
                    ac.PlayOneShot(audios[6], 2.0f);
                    punch.text = "";
                    comboName.text = "";
                    StartCoroutine(BagBouncing(punchingNumber, true, 0));
                    StopCoroutine(startTimer());
                    restartTrainingButton.SetActive(true);
                    status = 0;
                }
                
                
            }
            else
            {
                if (curCombo % 2 == 0)
                {
                    generateComboText(curPunching, curCombo, testCombos, false);
                    StartCoroutine(BagBouncing(punchingNumber, true, testCombos[curCombo][curPunching]));
                }
                else
                {
                    generateComboText(curPunching, curCombo, testCombos, true);
                    if (getRepeatedIndex(curCombo, testCombos) != curPunching)
                    {
                        StartCoroutine(BagBouncing(punchingNumber, true, testCombos[curCombo][curPunching]));
                    }
                    else
                    {
                        StartCoroutine(BagBouncing(punchingNumber, true, 0));
                    }
                }

            }
        }
        else
        {
            curPunching = 0;
            if (curCombo % 2 == 0)
            {
                generateComboText(0, curCombo, testCombos, false);
                StartCoroutine(BagBouncing(punchingNumber, false, testCombos[curCombo][curPunching]));
            }
            else
            {
                generateComboText(0, curCombo, testCombos, true);
                StartCoroutine(BagBouncing(punchingNumber, false, testCombos[curCombo][curPunching]));
            }
        }
    }

    private int getRepeatedIndex(int _curCombo, int[][] _combos)
    {
        for (int i = 1; i < _combos[_curCombo].Length; i++)
        {
            if ((_combos[_curCombo][i] == _combos[_curCombo][i - 1]) && ((_combos[_curCombo][i] == 3) || (_combos[_curCombo][i] == 4)))
            {
                return i;
            }
        }
        return -1;
    }
    private void generateComboText(int hitNumber, int _curCombo, int[][] _combos, bool isRepeated)
    {
        int repeatedIndex = -1;
        if (isRepeated)
        {
            repeatedIndex = getRepeatedIndex(_curCombo, _combos);
        }
        sb.Clear();
        for (int i = 0; i < _combos[_curCombo].Length; i++)
        {
            if (i == hitNumber)
            {
                sb.Append("<size=9><color=#FFC166>");
                if (i == repeatedIndex) {
                    sb.Append("?");
                } else {
                    sb.Append(_combos[_curCombo][i]);
                }
                sb.Append("</color></size>");
            }
            else
            {
                sb.Append("<size=6><color=#777777>");
                if (i == repeatedIndex)
                {
                    sb.Append("?");
                }
                else
                {
                    sb.Append(_combos[_curCombo][i]);
                }
                sb.Append("</color></size>");
            }
            if ((i + 1) != _combos[_curCombo].Length)
            {
                sb.Append("<size=6><color=#777777> - </color></size>");
            }
        }
        if (_combos[_curCombo].Length - 2 * hitNumber - 1 < 0)
        {
            for(int i = 0; i < -(_combos[_curCombo].Length - 2 * hitNumber - 1); i++)
            {
                sb.Append("<size=6><color=#777777>     </color></size>");
            }
        }
        if (_combos[_curCombo].Length - 2 * hitNumber - 1 > 0)
        {
            for (int i = 0; i < _combos[_curCombo].Length - 2 * hitNumber - 1; i++)
            {
                sb.Insert(0, "<size=6><color=#777777>     </color></size>");
            }
        }
        if (isRepeated && (hitNumber == repeatedIndex))
        {
            punch.text = "What's next?";
        } else
        {
            punch.text = punches[_combos[_curCombo][hitNumber]].ToString();
        }
        comboName.text = sb.ToString();
        
    }

    IEnumerator PlayVideo(float duration, int _tutorialIndex)
    {
        isPlayingVideo = true;
        comboName.text = "";
        punch.text = "";
        videoScreen.SetActive(true);
        switch (_tutorialIndex)
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
        switch (_tutorialIndex)
        {
            case 1:
                punch.text = "";
                comboName.text = "";
                tutorialReminder[0].SetActive(true);
                break;
            case 3:
                punch.text = "";
                comboName.text = "";
                tutorialReminder[1].SetActive(true);
                break;
            case 5:
                punch.text = "";
                comboName.text = "";
                tutorialReminder[2].SetActive(true);
                break;
        }
        yield return new WaitForSeconds(duration);
        switch (_tutorialIndex)
        {
            case 1:
                jabvideoPlayer.SetActive(false);
                tutorialReminder[0].SetActive(false);
                break;
            case 3:
                hookvideoPlayer.SetActive(false);
                tutorialReminder[1].SetActive(false);
                break;
            case 5:
                uppercutvideoPlayer.SetActive(false);
                tutorialReminder[2].SetActive(false);
                break;
        }
        videoScreen.SetActive(false);
        isPlayingVideo = false;
        punch.text = punches[_tutorialIndex];
        comboName.text = _tutorialIndex.ToString();
        punchingBag.GetComponent<Renderer>().material = bagMaterials[_tutorialIndex];
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
                ac.PlayOneShot(audios[0], audios[0].length);
                yield return new WaitForSeconds(0.2f);
                anim.SetBool("jab", false);
                break;
            case 2:
                anim.SetBool("cross", true);
                ac.PlayOneShot(audios[0], audios[0].length);
                yield return new WaitForSeconds(0.2f);
                anim.SetBool("cross", false);
                break;
            case 3:
                anim.SetBool("hook", true);
                ac.PlayOneShot(audios[1], audios[1].length);
                yield return new WaitForSeconds(0.2f);
                anim.SetBool("hook", false);
                break;
            case 4:
                anim.SetBool("hook", true);
                ac.PlayOneShot(audios[2], audios[2].length);
                yield return new WaitForSeconds(0.2f);
                anim.SetBool("hook", false);
                break;
            case 5:
                anim.SetBool("uppercut", true);
                ac.PlayOneShot(audios[3], audios[3].length);
                yield return new WaitForSeconds(0.2f);
                anim.SetBool("uppercut", false);
                break;
            case 6:
                anim.SetBool("uppercut", true);
                ac.PlayOneShot(audios[3], audios[3].length);
                yield return new WaitForSeconds(0.2f);
                anim.SetBool("uppercut", false);
                break;
            default:
                break;
        }
        punchingBag.GetComponent<Renderer>().material = bagMaterials[materialIndex];
    }

}
