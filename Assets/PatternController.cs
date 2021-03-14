using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;
using UnityEngine.Events;
using UnityEngine.UI;

public class PatternController : MonoBehaviour
{
    // Start is called before the first frame update
    //public List<int> patternIndexes = new List<int>();
    public ButtonListener listener;
    [SerializeField]
    private int[][] combos = { 
        new int[]{1,1}, 
        new int[]{1,1,2},
        new int[]{1,2},
        new int[]{1,2,1},
        new int[]{1,2,1,2},
        new int[]{1,2,3,2},
        new int[]{1,6,3,2}
    };

    private string[] punchings = { "Jab", "Right Cross", "Left Hook", "Right Hook", "Left Uppercut", "Right Uppercut" };
    private Color[] punchingColors = { Color.red, Color.green, Color.blue, Color.yellow, Color.white, Color.grey};
    private GameObject[] squares = new GameObject[6];
    void Start()
    {
    	//for(int i = 0; i < patternIndexes.Count; i++){
    	//	int curPatternIndex = patternIndexes[i];
    	//	GameObject curObject = this.gameObject.transform.GetChild(curPatternIndex).gameObject;
    	//	curObject.SetActive(false);
    	//}
        for (int i = 0; i < 6; i++)
        {
            squares[i] = this.gameObject.transform.GetChild(i).gameObject;
            squares[i].SetActive(false);
        }
        ModelController.MC.recoverOriginalModel();
    }

    //IEnumerator lightUpModel(i){
    //    ModelController.MC.instantiateModel(i);
    //    yield return new WaitForSeconds(0.5f);
    //}

    public void generatingCombo()
    {
        System.Random rd = new System.Random();
        int comboIndex = rd.Next(0, combos.Length);
        for (int i = 0; i < combos[comboIndex].Length; i++)
        {
            //swap the model
            //ModelController.MC.instantiateModel(i);
            squares[i].GetComponent<Image>().color = punchingColors[combos[comboIndex][i] - 1];
            squares[i].GetComponentInChildren<Text>().text = punchings[combos[comboIndex][i] - 1];
            squares[i].SetActive(true);

        }
        for (int i = 0; i < combos[comboIndex].Length; i++)
        {
            //StartCoroutine(lightUpModel(i));
        }
        
        listener.comboSetter(combos[comboIndex]);
    }
    // Update is called once per frame
    void Update()
    {
		//StartCoroutine(playPattern());
    }

    //IEnumerator playPattern(){
    //	for(int i = 0; i < patternIndexes.Count; i++){
    //		int curPatternIndex = patternIndexes[i];
    //		GameObject curObject = this.gameObject.transform.GetChild(curPatternIndex).gameObject;
    //		curObject.SetActive(true);
    //		yield return new WaitForSeconds(0.5f);
    //	}
    //}
    
}
