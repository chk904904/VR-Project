using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public Text instruction;
    public Text result;
    public Transform prefab;
    private string[] punches =
    {
        "Jab",
        "Cross",
        "Left Hook",
        "Right Hook",
    };
    private int curPunch = 0;
    private Transform curCube;
    private int score = 0;
    void Start()
    {
        instruction.text = punches[curPunch];
        curCube = Instantiate(prefab);
    }

    // Update is called once per frame
    void Update()
    {
        if (curCube != null)
        {
            if (curCube.position.z > 2)
            {
                Destroy(curCube.gameObject);
                result.text = "Miss";
                StartCoroutine(createCube());
            }
            else
            {
                curCube.Translate(Vector3.forward * Time.deltaTime * 2, Space.Self);
            }
        }
    }

    public void changeScore(bool isRight)
    {
        if (isRight)
        {
            score++;
            GameObject.Find("debug").GetComponent<Text>().text = score.ToString();
            result.text = "Correct";
        } else
        {
            result.text = "Wrong";
        }
        StartCoroutine(createCube());
    }
    IEnumerator createCube()
    {
        if (curPunch == 3)
        {
            curPunch = 0;
        } else
        {
            curPunch++;
        }
        yield return new WaitForSeconds(0.8f);
        curCube = Instantiate(prefab);
        curCube.GetComponent<MovementDetection>().punchName = punches[curPunch];
        instruction.text = punches[curPunch];
        result.text = "";
    }
}
