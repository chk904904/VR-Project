using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;
using UnityEngine.Events;
using UnityEngine.UI;
using System;

public class ButtonListener : MonoBehaviour
{
    public UnityEvent actionEvent;
    public UnityEvent defaultEvent;
    public UnityEvent proximityEvent;
    public UnityEvent contactEvent;
    public GameObject textBox; 
    public Text text;
    public GameObject timer;
    public Animator anim;



    private Renderer m_renderer;
    private List<int> patternIndexesLocal;
    private int curIndex;
    private InteractableTool hand;
    private int curPunching = 0;
    private int numOfPunching = -1;
    private int[] curCombo = new int[6];
    private int score = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<ButtonController>().InteractableStateChanged.AddListener(InitiateEvent);
        m_renderer = GetComponent<Renderer>();
        text.text = "Score: " + score.ToString();
        //initialize indexes
        //patternIndexesLocal = GameObject.Find("Text").GetComponent<PatternController>().patternIndexes;
        //curIndex = 0;
    }

    public void endTraining()
    {
        GetComponent<ButtonController>().InteractableStateChanged.RemoveListener(InitiateEvent);
        foreach(Transform child in textBox.transform)
        {
            child.gameObject.SetActive(false);
        }
    }
    void InitiateEvent(InteractableStateArgs state)
    {
        
        if (state.NewInteractableState == InteractableState.ActionState)
        {
            if (timer.GetComponent<timer>().timeGetter() == 60)
            {
                timer.GetComponent<timer>().StartTimer();
                textBox.GetComponent<PatternController>().generatingCombo();
            }
            if (curCombo[curPunching] == punchingDetect(state))
            {
                textBox.transform.GetChild(curPunching).gameObject.SetActive(false);
                curPunching++;
            }

            if(curPunching == numOfPunching)
            {
                numOfPunching = -1;
                curPunching = 0;
                Array.Clear(curCombo, 0, curCombo.Length);
                score++;
                text.text = "Score: " + score.ToString();
                textBox.GetComponent<PatternController>().generatingCombo();
            }
        }
        else
        {
            defaultEvent.Invoke();
        }
        //anim.SetBool("isPunched", false);
    }
    public void comboSetter(int[] temp)
    {
        Array.Clear(curCombo, 0, curCombo.Length);
        temp.CopyTo(curCombo, 0);
        numOfPunching = temp.Length;
        curPunching = 0;

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
                StartCoroutine(BagBouncing(2));
                return 2;
            }
            else
            {
                StartCoroutine(BagBouncing(1));
                return 1;
            }

        }
        else if (x > y && x > z)
        {
            if (state.Tool.IsRightHandedTool)
            {
                StartCoroutine(BagBouncing(4));
                return 4;
            }
            else
            {
                StartCoroutine(BagBouncing(3));
                return 3;

            //    if (state.Tool.IsRightHandedTool)
            //    {
            //        //TODO: check whether correct order
            //        GameObject green = textBox.transform.transform.GetChild(0).gameObject; 
            //        text.text = "Right Straight";
            //        if(curIndex == 0){
            //            green.SetActive(false);
            //            curIndex += 1;
            //        }
            //        else{
            //            for(int i = 0; i < patternIndexesLocal.Count ; i++){
            //                GameObject curColor = textBox.transform.transform.GetChild(patternIndexesLocal[i]).gameObject;
            //                curColor.SetActive(true);
            //            }
            //            curIndex = 0; 
            //        }
            //    } else
            //    {
            //        //TODO: check whether correct order
            //        GameObject brown = textBox.transform.transform.GetChild(1).gameObject;
            //        text.text = "Left Straight";
            //        if(curIndex == 1){
            //            brown.SetActive(false);
            //            curIndex += 1;
            //        }
            //        else{
            //            for(int i = 0; i < patternIndexesLocal.Count ; i++){
            //                GameObject curColor = textBox.transform.transform.GetChild(patternIndexesLocal[i]).gameObject;
            //                curColor.SetActive(true);
            //            }
            //            curIndex = 0; 
            //        }
            //    }
                
            //} else if (System.Math.Abs(state.Tool.Velocity.x) > System.Math.Abs(state.Tool.Velocity.y) && System.Math.Abs(state.Tool.Velocity.x) > System.Math.Abs(state.Tool.Velocity.z))
            //{
            //    if (state.Tool.IsRightHandedTool)
            //    {
            //        GameObject red = textBox.transform.GetChild(2).gameObject;
            //        text.text = "Right Hook";
            //        if(curIndex == 2){
            //            red.SetActive(false);
            //            curIndex += 1;
            //        }
            //        else{
            //            for(int i = 0; i < patternIndexesLocal.Count ; i++){
            //                GameObject curColor = textBox.transform.transform.GetChild(patternIndexesLocal[i]).gameObject;
            //                curColor.SetActive(true);
            //            }
            //            curIndex = 0;
            //        }
            //    }
            //    else
            //    {
            //        GameObject pink = textBox.transform.GetChild(3).gameObject;
            //        text.text = "Left Hook";
            //        if(curIndex == 3){
            //            pink.SetActive(false);
            //            curIndex += 1;
            //        }
            //        else{
            //            for(int i = 0; i < patternIndexesLocal.Count ; i++){
            //                GameObject curColor = textBox.transform.transform.GetChild(patternIndexesLocal[i]).gameObject;
            //                curColor.SetActive(true);
            //            }
            //            curIndex = 0;
            //        }
            //    }
            //} else
            //{
            //    if (state.Tool.IsRightHandedTool)
            //    {
            //        GameObject purple = textBox.transform.GetChild(4).gameObject;
            //        text.text = "Right Uppercut";
            //        if(curIndex == 4){
            //            purple.SetActive(false);
            //            curIndex += 1;
            //        }
            //        else{
            //            for(int i = 0; i < patternIndexesLocal.Count ; i++){
            //                GameObject curColor = textBox.transform.transform.GetChild(patternIndexesLocal[i]).gameObject;
            //                curColor.SetActive(true);
            //            }
            //            curIndex = 0; 
            //        }
            //    }
            //    else
            //    {
            //        GameObject blue = textBox.transform.GetChild(5).gameObject;
            //        text.text = "Left Uppercut";
            //        if(curIndex == 5){
            //            blue.SetActive(false);
            //            curIndex += 1;
            //        }
            //        else{
            //            for(int i = 0; i < patternIndexesLocal.Count ; i++){
            //                GameObject curColor = textBox.transform.transform.GetChild(patternIndexesLocal[i]).gameObject;
            //                curColor.SetActive(true);
            //            }
            //            curIndex = 0; 
            //        }
            //    }
            }
        }
        else
        {
            if (state.Tool.IsRightHandedTool)
            {
                StartCoroutine(BagBouncing(6));
                return 6;
            }
            else
            {
                StartCoroutine(BagBouncing(5));
                return 5;
            }
        }
    }
    // Update is called once per frame

    IEnumerator BagBouncing(int temp)
    {
        switch(temp)
        {
            case 1:
                anim.SetBool("jab", true);
                yield return new WaitForSeconds(0.5f);
                anim.SetBool("jab", false);
                break;
            case 2:
                anim.SetBool("cross", true);
                yield return new WaitForSeconds(0.5f);
                anim.SetBool("cross", false);
                break;
            case 3:
                anim.SetBool("hook", true);
                yield return new WaitForSeconds(0.5f);
                anim.SetBool("hook", false);
                break;
            case 4:
                anim.SetBool("hook", true);
                yield return new WaitForSeconds(0.5f);
                anim.SetBool("hook", false);
                break;
            case 5:
                anim.SetBool("uppercut", true);
                yield return new WaitForSeconds(0.5f);
                anim.SetBool("uppercut", false);
                break;
            case 6:
                anim.SetBool("uppercut", true);
                yield return new WaitForSeconds(0.5f);
                anim.SetBool("uppercut", false);
                break;
            default:
                break;

        }
        
    }
    void Update()
    {
        //if(hand != null)
        //{
        //    valueDisplay.text = (int)(100*hand.ToolTransform.position.x) + " " + (int)(100*hand.ToolTransform.position.y) + " " + (int)(100*hand.ToolTransform.position.z);
        //}
    }
}
