using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialRestart : MonoBehaviour
{
    public ControllerDetection CD;
    private void Start()
    {
        StartCoroutine(colliderDelay());
    }

    IEnumerator colliderDelay()
    {
        this.GetComponent<BoxCollider>().enabled = false;
        yield return new WaitForSeconds(1f);
        this.GetComponent<BoxCollider>().enabled = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(TriggerVibration(other.GetComponent<GloveFollowing>().m_controller));

    }

    IEnumerator TriggerVibration(OVRInput.Controller controller)
    {
        OVRInput.SetControllerVibration(1f, 1f, controller);
        yield return new WaitForSeconds(0.1f);
        OVRInput.SetControllerVibration(1f, 0f, controller);
        CD.restartTutorial() ;
    }
}
