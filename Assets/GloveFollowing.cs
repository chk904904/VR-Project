using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GloveFollowing : MonoBehaviour
{
    bool isLeft = true;
    public Text text;
    private Vector3 m_lastPos;
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
        if (transform.localPosition.magnitude != 0.15f)
        {
            transform.localPosition = new Vector3(0, 0, -0.15f);
            if(isLeft)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 90);
            } else
            {
                transform.localRotation = Quaternion.Euler(0, 0, -90);
            }
            
        }
        
        velocity = (RelativePosition() - m_lastPos) / Time.deltaTime;
        m_lastPos = RelativePosition();
        if (!isLeft)
        {
            text.text = this.gameObject.name + m_lastPos;
        }
        

    }

    private Vector3 RelativePosition()
    {
        Vector3 distance = transform.position - root.position;
        Vector3 relativePosition = Vector3.zero;
        relativePosition.x = Vector3.Dot(distance, root.right.normalized);
        relativePosition.y = Vector3.Dot(distance, root.up.normalized);
        relativePosition.z = Vector3.Dot(distance, root.forward.normalized);
        return relativePosition;
    }
}
