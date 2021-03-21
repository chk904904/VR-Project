using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GloveFollowing : MonoBehaviour
{
    bool isLeft = true;
    public Text text;
    private Vector3 m_lastPos;
    private Vector3 m_lastVelocity;
    public float speed = 0f;
    public Vector3 velocity = new Vector3(0, 0, 0);
    public Transform root;
    // Start is called before the first frame update
    void Start()
    {
        if (this.gameObject.name.Equals("RightGlove"))
        {
            isLeft = false;
        }
        m_lastPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localPosition.magnitude != 0)
        {
            transform.localPosition = new Vector3(0, 0, 0);
            if(isLeft)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 90);
            } else
            {
                transform.localRotation = Quaternion.Euler(0, 0, -90);
            }
            
        }
        //Vector3 distance = transform.position - origin.position;
        //Vector3 relativePosition = Vector3.zero;
        //relativePosition.x = Vector3.Dot(distance, origin.right.normalized);
        //relativePosition.y = Vector3.Dot(distance, origin.up.normalized);
        //relativePosition.z = Vector3.Dot(distance, origin.forward.normalized);
        velocity = (transform.position - m_lastPos) / Time.deltaTime;
        m_lastPos = transform.position;

    }
}
