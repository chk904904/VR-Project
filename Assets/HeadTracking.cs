using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeadTracking : MonoBehaviour
{
    public GameObject headset;
    public GloveFollowing rh;
    public GloveFollowing lh;
    public Text instruction;
    public Text result;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //instruction.text = "Right: " + rh.RelativePosition().ToString();
        //result.text = "Left: " + lh.RelativePosition().ToString();
    }
}
