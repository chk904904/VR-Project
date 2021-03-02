using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class headPosTracker : MonoBehaviour
{
    GameObject centerEye;
    Vector3 headsetPos;
    Vector3 initialPos;
    bool shouldDodge; 
    float responseTime; 
    //This is the vector that guest should move in order to dodge
    //curPos - initialPos
    Vector3 dodgeVector; 

    void Start(){
        centerEye = GameObject.Find("CenterEyeAnchor");
        shouldDodge = false;
        initialPos = centerEye.transform.position;
        responseTime = 1.75f; 
    }

    void Update(){
        headsetPos = centerEye.transform.position;
        if(shouldDodge){
        	dodgeTimer();
        }
    }

  	void dodgeTimer()
  	{
    	StartCorountine(CheckDodge());
  	}

 
 	IEnumerator CheckDodge()
 	{
      bool success = false;
      while( success == false && timer > 0f )
      {
             timer -= Time.deltaTime; // reduce timer 
             success = successDodge();
             //Input.GetKeyDown(KeyCode.R);
             yield return null;
      }
      if(success == false) 
      {
             Debug.Log("Lost");
             yield break;
      }
      success = false;
      timer = 1.75f;
      while( success == false && timer > 0f )
      {
             timer -= Time.deltaTime; // reduce timer 
             success = successDodge();
             //Input.GetKeyDown(KeyCode.V);
             yield return null;
      }
      if(success == false && timer <= 0f ) 
      {
             Debug.Log("Lost");
             yield break;
      }
      Debug.Log("Won");
 	}

 	bool successDodge(){
 		bool isSuccessful = true;
 		float xDiff = headsetPos.x - initialPos.x; 
 		float yDiff = headsetPos.y - initialPos.y; 
 		float zDiff = headsetPos.z - initialPos.z;
 		if(dodgeVector.x < 0){
 			if(xDiff > dodgeVector.x){
 				isSuccessful = false;
 			}
 		} 
 		else{
 			if(xDiff < dodgeVector.x){
 				isSuccessful = false;
 			}
 		}

 		if(dodgeVector.y < 0){
 			if(yDiff > dodgeVector.y){
 				isSuccessful = false;
 			}
 		}
 		else{
 			if(yDiff < dodgeVector.y){
 				isSuccessful = false;
 			}
 		}

 		if(dodgeVector.z < 0){
 			if(zDiff > dodgeVector.z){
 				isSuccessful = false;
 			}
 		}
 		else{
 			if(zDiff < dodgeVector.z){
 				isSuccessful = false;
 			}
 		}
 		return isSuccessful;
 	}
}
