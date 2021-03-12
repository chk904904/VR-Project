using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelController : MonoBehaviour
{
	public GameObject bag1;
	public GameObject bag2;
	public GameObject bag3; 
	public GameObject bag4; 
	public GameObject bag5; 
	public GameObject bag6; 
	public GameObject bag7; 
	public GameObject bagSuccess;
	//This is the placeholder for the instantiated object. For deletion only
	public GameObject model;
	List<GameObject> models = new List<GameObject>();
	public static ModelController MC;
	private GameObject originalModel;
	private Vector3 originalModelPos;

	void Awake(){
		MC = this;
		originalModel = GameObject.Find("00punchingBag_Rigged").transform.GetChild(0).gameObject;
		originalModelPos = originalModel.transform.position;
		models.Add(bag1);
        models.Add(bag2);
        models.Add(bag3);
        models.Add(bag4);
        models.Add(bag5);
        models.Add(bag6);
        models.Add(bag7);
        models.Add(bagSuccess);

	}

    // Start is called before the first frame update
    void Start()
    {
    }

    public void instantiateModel(int modelIndex){
    	if(originalModel.activeInHierarchy){
    		originalModel.SetActive(false);
    	}
    	//instantiate a model
    	GameObject modelToInstantiate = models[modelIndex];
    	Debug.Log(originalModelPos);
		model = Instantiate(modelToInstantiate, originalModelPos, Quaternion.identity);
		model.transform.localScale = new Vector3(0.3f*50, 0.3f*50, 0.3f*50);
    }

    public void recoverOriginalModel(){
    	//remove the instantiated model
    	Destroy (model);
    	GameObject originalModel = GameObject.Find("00punchingBag_Rigged").transform.GetChild(0).gameObject;
    	originalModel.SetActive(true);
    }
}
