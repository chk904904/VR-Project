using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;
using UnityEngine.Events;
using UnityEngine.UI;

public class PatternController : MonoBehaviour
{
    // Start is called before the first frame update
    public List<int> patternIndexes = new List<int>();


    void Start()
    {
    	for(int i = 0; i < patternIndexes.Count; i++){
    		int curPatternIndex = patternIndexes[i];
    		GameObject curObject = this.gameObject.transform.GetChild(curPatternIndex).gameObject;
    		curObject.SetActive(false);
    	}
    }

    // Update is called once per frame
    void Update()
    {
		StartCoroutine(playPattern());
    }

    IEnumerator playPattern(){
    	for(int i = 0; i < patternIndexes.Count; i++){
    		int curPatternIndex = patternIndexes[i];
    		GameObject curObject = this.gameObject.transform.GetChild(curPatternIndex).gameObject;
    		curObject.SetActive(true);
    		yield return new WaitForSeconds(0.5f);
    	}
    }
}
