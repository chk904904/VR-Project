using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GloveFollowing : MonoBehaviour
{
    public Transform root;
    public OVRInput.Controller m_controller;
    void Update()
    {
        if (transform.localPosition.magnitude != 0.15f)
        {
            transform.localPosition = new Vector3(0, 0, -0.15f);
            if(this.gameObject.name.Equals("LeftGlove"))
            {
                transform.localRotation = Quaternion.Euler(0, 0, 90);
            } else
            {
                transform.localRotation = Quaternion.Euler(0, 0, -90);
            }
        }
    }

    public Vector3 RelativePosition()
    {
        Vector3 distance = transform.position - root.position;
        Vector3 relativePosition = Vector3.zero;
        relativePosition.x = Vector3.Dot(distance, root.right.normalized);
        relativePosition.y = Vector3.Dot(distance, root.up.normalized);
        relativePosition.z = Vector3.Dot(distance, root.forward.normalized);
        return relativePosition;
    }
}
