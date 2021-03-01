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
    public Text text;
    private Renderer m_renderer;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<ButtonController>().InteractableStateChanged.AddListener(InitiateEvent);
        m_renderer = GetComponent<Renderer>();
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
            if (System.Math.Abs(state.Tool.Velocity.z) > System.Math.Abs(state.Tool.Velocity.x) && System.Math.Abs(state.Tool.Velocity.z) > System.Math.Abs(state.Tool.Velocity.y))
            {
                if (state.Tool.IsRightHandedTool)
                {
                    text.text = "Right Straight";
                } else
                {
                    text.text = "Left Straight";
                }
                
            } else if (System.Math.Abs(state.Tool.Velocity.x) > System.Math.Abs(state.Tool.Velocity.y) && System.Math.Abs(state.Tool.Velocity.x) > System.Math.Abs(state.Tool.Velocity.z))
            {
                if (state.Tool.IsRightHandedTool)
                {
                    text.text = "Right Hook";
                }
                else
                {
                    text.text = "Left Hook";
                }
            } else
            {
                if (state.Tool.IsRightHandedTool)
                {
                    text.text = "Right Uppercut";
                }
                else
                {
                    text.text = "Left Uppercut";
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
