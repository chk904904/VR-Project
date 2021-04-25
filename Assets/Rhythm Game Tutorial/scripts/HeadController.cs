using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;
using UnityEngine.Events;
using UnityEngine.UI;
using System;
using System.Text;

public class HeadController : MonoBehaviour
{
    public GameObject headAnchor;
    public GameObject txt; 
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MeshRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = headAnchor.transform.position;
    }

	void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Rods")){
            txt.GetComponent<Text>().text = "Collision!";
        }
    }

    void OnTriggerExit(Collider other){
        if(other.gameObject.CompareTag("Rods")){
            txt.GetComponent<Text>().text = "No Collition";
        }
    }

}
