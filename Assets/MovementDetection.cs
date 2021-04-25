using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovementDetection : MonoBehaviour
{
    public string punchName;
    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<GloveFollowing>().isHitted = true;
        other.GetComponent<GloveFollowing>().correctPunch = punchName;
        Destroy(gameObject);
    }
}
