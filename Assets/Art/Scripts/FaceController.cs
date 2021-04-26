using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceController : MonoBehaviour
{
    public bool face_right; 
    public bool face_left; 
    public bool face_middle; 
    public float face_distance;

    private Vector3 left; 
    private Vector3 right; 
    private Vector3 middle;
    private float ori_y; 
    private float ori_x; 
    private float ori_z; 
    // Start is called before the first frame update
    void Start()
    {
        face_right = false; 
        face_left = false; 
        face_middle = false;

    }

    public void startFacePos(){
        ori_y = GameObject.Find("Head").transform.position.y;
        ori_x = GameObject.Find("Head").transform.position.x;
        ori_z = GameObject.Find("Head").transform.position.z;
        left = new Vector3(ori_x + 0.4f, ori_y + 0f, ori_z - face_distance);
        right = new Vector3(ori_x - 0.4f, ori_y + 0f, ori_z - face_distance);
        middle = new Vector3(ori_x + 0f, ori_y + 0f, ori_z - face_distance);
        transform.position = right;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.Find("Head").GetComponent<HeadController>().start_game){
            Debug.Log("Face controller Hi");
            if(face_right){
                transform.position = right;
            }
            else if(face_left){
                transform.position = left;
            }
            else if(face_middle){
                transform.position = middle;
            }
        }
    }
}
