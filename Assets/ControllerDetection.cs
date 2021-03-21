using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerDetection : MonoBehaviour
{
    // Start is called before the first frame update
    public Text text;
    private int counter = 0;
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        GloveFollowing GF = other.gameObject.GetComponent<GloveFollowing>();
        text.text = other.gameObject.name+GF.velocity;
    }
    void OnCollisionEnter(Collision collision)
    {
        text.text = collision.gameObject.name + "collision";
        //text.text = collision.relativeVelocity.x + ", " + collision.relativeVelocity.y + ", " + collision.relativeVelocity.z;
        //text.text = collision.gameObject.name;
        //text.text = collision.gameObject.name + ": " + collision.relativeVelocity.x.ToString() + " " + collision.relativeVelocity.y.ToString() + " " + collision.relativeVelocity.z.ToString();
    }
}
