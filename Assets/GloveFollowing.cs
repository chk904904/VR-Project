using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GloveFollowing : MonoBehaviour
{
    public Transform root;
    public OVRInput.Controller m_controller;
    public bool isFollowing = true;
    public AudioClip woosh;
    private AudioSource ac;
    private Vector3 oldPosition = Vector3.zero;
    private int frameCounter = 0;
    private float speed = 0f;
    private bool isPlayed = false;


    private void Start()
    {
        ac = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (frameCounter == 5)
        {
            frameCounter = 0;
        } else
        {
            frameCounter++;
        }

        if((oldPosition.magnitude == 0) || (frameCounter == 0))
        {
            oldPosition = transform.position;
        }

        if((frameCounter == 2) && ((transform.position - oldPosition).magnitude > 0.15f) && !isPlayed){
            StartCoroutine(PlayWoosh());
        } 

        if (isFollowing)
        {
            if (transform.localPosition.magnitude != 0.15f)
            {
                transform.localPosition = new Vector3(0, 0, -0.15f);
                if (this.gameObject.name.Equals("LeftGlove"))
                {
                    transform.localRotation = Quaternion.Euler(0, 0, 90);
                }
                else
                {
                    transform.localRotation = Quaternion.Euler(0, 0, -90);
                }
            }
        }
        
    }

    IEnumerator PlayWoosh()
    {
        isPlayed = true;
        ac.PlayOneShot(woosh, 0.3f);
        yield return new WaitForSeconds(0.4f);
        isPlayed = false;
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
