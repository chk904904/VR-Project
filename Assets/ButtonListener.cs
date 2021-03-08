using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;
using UnityEngine.Events;
using UnityEngine.UI;

public class ButtonListener : MonoBehaviour
{
    public UnityEvent actionEvent;
    public UnityEvent defaultEvent;
    public UnityEvent proximityEvent;
    public UnityEvent contactEvent;
    public GameObject textBox; 
    public Text text;
    private Renderer m_renderer;
    private List<int> patternIndexesLocal;
    private int curIndex;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<ButtonController>().InteractableStateChanged.AddListener(InitiateEvent);
        m_renderer = GetComponent<Renderer>();
        textBox = GameObject.Find("Text");
        //initialize indexes
        patternIndexesLocal = GameObject.Find("Text").GetComponent<PatternController>().patternIndexes;
        curIndex = 0;
    }

    void InitiateEvent(InteractableStateArgs state)
    {
        //if(state.NewInteractableState == InteractableState.ProximityState)
        //{
        //    proximityEvent.Invoke();
        //} else if(state.NewInteractableState == InteractableState.ContactState)
        //{
        //    contactEvent.Invoke();
        //} else 
        if (state.NewInteractableState == InteractableState.ActionState)
        {
            if (state.Tool.IsRightHandedTool)
            {
                m_renderer.material.color = Color.green;
            } else
            {
                m_renderer.material.color = Color.blue;
            }
            text.text = "";
            /*
            greenIndex = 0; 
            brownIndex = 1; 
            redIndex = 2; 
            pinkIndex = 3;
            purpleIndex = 4; 
            blueIndex = 5 
            */
            if (System.Math.Abs(state.Tool.Velocity.z) > System.Math.Abs(state.Tool.Velocity.x) && System.Math.Abs(state.Tool.Velocity.z) > System.Math.Abs(state.Tool.Velocity.y))
            {
                if (state.Tool.IsRightHandedTool)
                {
                    //TODO: check whether correct order
                    GameObject green = textBox.transform.transform.GetChild(0).gameObject; 
                    text.text = "Right Straight";
                    if(curIndex == 0){
                        green.SetActive(false);
                        curIndex += 1;
                    }
                    else{
                        for(int i = 0; i < patternIndexesLocal.Count ; i++){
                            GameObject curColor = textBox.transform.transform.GetChild(patternIndexesLocal[i]).gameObject;
                            curColor.SetActive(true);
                        }
                        curIndex = 0; 
                    }
                } else
                {
                    //TODO: check whether correct order
                    GameObject brown = textBox.transform.transform.GetChild(1).gameObject;
                    text.text = "Left Straight";
                    if(curIndex == 1){
                        brown.SetActive(false);
                        curIndex += 1;
                    }
                    else{
                        for(int i = 0; i < patternIndexesLocal.Count ; i++){
                            GameObject curColor = textBox.transform.transform.GetChild(patternIndexesLocal[i]).gameObject;
                            curColor.SetActive(true);
                        }
                        curIndex = 0; 
                    }
                }
                
            } else if (System.Math.Abs(state.Tool.Velocity.x) > System.Math.Abs(state.Tool.Velocity.y) && System.Math.Abs(state.Tool.Velocity.x) > System.Math.Abs(state.Tool.Velocity.z))
            {
                if (state.Tool.IsRightHandedTool)
                {
                    GameObject red = textBox.transform.GetChild(2).gameObject;
                    text.text = "Right Hook";
                    if(curIndex == 2){
                        red.SetActive(false);
                        curIndex += 1;
                    }
                    else{
                        for(int i = 0; i < patternIndexesLocal.Count ; i++){
                            GameObject curColor = textBox.transform.transform.GetChild(patternIndexesLocal[i]).gameObject;
                            curColor.SetActive(true);
                        }
                        curIndex = 0;
                    }
                }
                else
                {
                    GameObject pink = textBox.transform.GetChild(3).gameObject;
                    text.text = "Left Hook";
                    if(curIndex == 3){
                        pink.SetActive(false);
                        curIndex += 1;
                    }
                    else{
                        for(int i = 0; i < patternIndexesLocal.Count ; i++){
                            GameObject curColor = textBox.transform.transform.GetChild(patternIndexesLocal[i]).gameObject;
                            curColor.SetActive(true);
                        }
                        curIndex = 0;
                    }
                }
            } else
            {
                if (state.Tool.IsRightHandedTool)
                {
                    GameObject purple = textBox.transform.GetChild(4).gameObject;
                    text.text = "Right Uppercut";
                    if(curIndex == 4){
                        purple.SetActive(false);
                        curIndex += 1;
                    }
                    else{
                        for(int i = 0; i < patternIndexesLocal.Count ; i++){
                            GameObject curColor = textBox.transform.transform.GetChild(patternIndexesLocal[i]).gameObject;
                            curColor.SetActive(true);
                        }
                        curIndex = 0; 
                    }
                }
                else
                {
                    GameObject blue = textBox.transform.GetChild(5).gameObject;
                    text.text = "Left Uppercut";
                    if(curIndex == 5){
                        blue.SetActive(false);
                        curIndex += 1;
                    }
                    else{
                        for(int i = 0; i < patternIndexesLocal.Count ; i++){
                            GameObject curColor = textBox.transform.transform.GetChild(patternIndexesLocal[i]).gameObject;
                            curColor.SetActive(true);
                        }
                        curIndex = 0; 
                    }
                }
            }
            //text.text = state.Tool.Velocity.x + " " + state.Tool.Velocity.y + " " + state.Tool.Velocity.z;
            //actionEvent.Invoke();
        }
        else
        {
            defaultEvent.Invoke();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
