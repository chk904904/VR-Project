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

    // Start is called before the first frame update
    void Start()
    {
        models.Add(bag1);
        models.Add(bag2);
        models.Add(bag3);
        models.Add(bag4);
        models.Add(bag5);
        models.Add(bag6);
        models.Add(bag7);
        models.Add(bagSuccess);
    }

    public void instantiateModel(int modelIndex){
    	GameObject originalModel = GameObject.Find("punchingBag_Rigged").transform.GetChild(0).gameObject;
    	originalModel.SetActive(false);
    	Vector3 originalModelPos = originalModel.transform.position;
    	GameObject modelToInstantiate = models[modelIndex];
    	//instantiate a model
		model = Instantiate(modelToInstantiate, originalModelPos, Quaternion.identity);
    }

    public void recoverOriginalModel(){
    	//remove the instantiated model
    	Destroy (model);
    	GameObject originalModel = GameObject.Find("punchingBag_Rigged").transform.GetChild(0).gameObject;
    	originalModel.SetActive(true);
    }
}
