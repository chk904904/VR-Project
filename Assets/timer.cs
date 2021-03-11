using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timer : MonoBehaviour
{
    private Text text;
    private float oneSecond = 1.0f;
    private int counter = 60;
    public GameObject target;
    // Start is called before the first frame update
    private void Start()
    {
        text = GetComponent<Text>();
        text.text = "Hit Bag to start";
        
    }

    public int timeGetter()
    {
        return counter;
    }

    public void StartTimer()
    {
        StartCoroutine(timing());
    }
    // Update is called once per frame
    IEnumerator timing()
    {
        while(counter > 0)
        {
            yield return new WaitForSeconds(1.0f);
            counter--;
            text.text = counter.ToString() + "s";
        }
        if (counter == 0)
        {
            text.text = "";
            target.GetComponent<ButtonListener>().endTraining();
        }
    }
}
