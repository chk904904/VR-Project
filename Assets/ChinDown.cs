using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChinDown : MonoBehaviour
{
    public GameObject handsUp;
    public GameObject screen;
    private void Start()
    {
        StartCoroutine(colliderDelay());
    }

    IEnumerator colliderDelay()
    {
        this.GetComponentInChildren<BoxCollider>().enabled = false;
        yield return new WaitForSeconds(3f);
        this.GetComponentInChildren<BoxCollider>().enabled = true;
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
        handsUp.SetActive(true);
        screen.transform.rotation = Quaternion.Euler(0, 0, 0);
        this.gameObject.SetActive(false);
    }
}
