using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RodController : MonoBehaviour
{   
    public GameObject leftRod; 
    public GameObject rightRod;
    public float smooth = 0.005f;

    public bool l_horizontal_rot; 
    public bool l_horizontal_rot_back;
    public bool l_start_over; 
    public bool l_hold;
    public bool l_is_horizontal;
    public bool l_up;
    public bool l_down;

    public bool r_horizontal_rot; 
    public bool r_horizontal_rot_back;
    public bool r_start_over; 
    public bool r_hold;
    public bool r_is_horizontal;
    public bool r_up;
    public bool r_down;
    // Start is called before the first frame update
    void Start()
    {
        l_horizontal_rot = false; 
        l_horizontal_rot_back = false;
        l_start_over = false; 
        l_hold = false;
        l_is_horizontal = false;
        l_up = false;
        l_down = false;

        r_horizontal_rot = false; 
        r_horizontal_rot_back = false;
        r_start_over = false; 
        r_hold = false;
        r_is_horizontal = false;
        r_up = false;
        r_down = false;
    }

    public void startRodPos(){
        float ori_y = GameObject.Find("Head").transform.position.y;
        float ori_x = GameObject.Find("Head").transform.position.x;
        float ori_z = GameObject.Find("Head").transform.position.z;
        Vector3 left = new Vector3(ori_x + 0.687f, ori_y + 0.26f, ori_z - 0.1f);
        Vector3 right = new Vector3(ori_x - 0.803f, ori_y + 0.24f, ori_z - 0.1f);
        //Debug.Log(leftRod.transform.position);
        //Debug.Log(rightRod.transform.position);
        leftRod.transform.position = left;
        rightRod.transform.position = right;
        leftRod.transform.rotation = Quaternion.Euler(180, 0, 0);
        rightRod.transform.rotation = Quaternion.Euler(180, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.Find("Head").GetComponent<HeadController>().start_game){
            move_left(); 
            move_right();
        }
    }


    void move_left(){
        if(l_start_over){
            l_start_over = false; 
            leftRod.transform.rotation = Quaternion.Euler(180, 90, 270);
        }
        else{
            if(l_horizontal_rot){
                Quaternion target;
                if(l_hold){
                    target = Quaternion.Euler(180, 90, 270);
                }
                else{
                    target = Quaternion.Euler(180, 0, 270);
                }
                leftRod.transform.rotation = Quaternion.Slerp(leftRod.transform.rotation, target,  Time.deltaTime * smooth);
            }
            else if(l_horizontal_rot_back){
                Quaternion target;
                if(l_hold){
                    target = Quaternion.Euler(180, 90, 270);
                }
                else{
                    target = Quaternion.Euler(180, 90, 270);
                }
                leftRod.transform.rotation = Quaternion.Slerp(leftRod.transform.rotation, target,  Time.deltaTime * smooth);
            }
            else if(l_is_horizontal){
                Quaternion target;
                if(l_hold){
                    target = Quaternion.Euler(180, 90, 270);
                }
                else{
                    target = Quaternion.Euler(180, 0, 270);
                }
                leftRod.transform.rotation = Quaternion.Slerp(leftRod.transform.rotation, target,  Time.deltaTime * smooth);
            }
            else if(l_up){
                Quaternion target;
                if(l_hold){
                    target = Quaternion.Euler(180, 90, 270);
                }
                else{
                    target = Quaternion.Euler(180, 0, 0);
                }
                leftRod.transform.rotation = Quaternion.Slerp(leftRod.transform.rotation, target,  Time.deltaTime * smooth);
            }
            else if(l_down){
                Quaternion target;
                if(l_hold){
                    target = Quaternion.Euler(180, 90, 270);
                }
                else{
                    target = Quaternion.Euler(0, 0, 0);
                }
                leftRod.transform.rotation = Quaternion.Slerp(leftRod.transform.rotation, target,  Time.deltaTime * smooth);
            }
        }   
    }


    void move_right(){
        if(r_start_over){
            r_start_over = false; 
            rightRod.transform.rotation = Quaternion.Euler(180, 90, 270);
        }
        else{
            if(r_horizontal_rot){
                Quaternion target;
                if(r_hold){
                    target = Quaternion.Euler(180, 90, 270);
                }
                else{
                    target = Quaternion.Euler(270, 90, 0);
                }
                rightRod.transform.rotation = Quaternion.Slerp(leftRod.transform.rotation, target,  Time.deltaTime * smooth);
            }
            else if(r_horizontal_rot_back){
                Quaternion target;
                if(r_hold){
                    target = Quaternion.Euler(180, 90, 270);
                }
                else{
                    target = Quaternion.Euler(180, 90, 270);
                }
                rightRod.transform.rotation = Quaternion.Slerp(leftRod.transform.rotation, target,  Time.deltaTime * smooth);
            }
            else if(r_is_horizontal){
                Quaternion target;
                if(l_hold){
                    target = Quaternion.Euler(180, 90, 270);
                }
                else{
                    target = Quaternion.Euler(270, 90, 0);
                }
                rightRod.transform.rotation = target;
            }
            else if(r_up){
                Quaternion target;
                if(r_hold){
                    target = Quaternion.Euler(180, 90, 270);
                }
                else{
                    target = Quaternion.Euler(180, 0, 0);
                }
                rightRod.transform.rotation = Quaternion.Slerp(leftRod.transform.rotation, target,  Time.deltaTime * smooth);
            }
            else if(r_down){
                Quaternion target;
                if(r_hold){
                    target = Quaternion.Euler(180, 90, 270);
                }
                else{
                    target = Quaternion.Euler(0, 0, 0);
                }
                rightRod.transform.rotation = Quaternion.Slerp(leftRod.transform.rotation, target,  Time.deltaTime * smooth);
            }
        }   
    }
}   