using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartButton : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject chinDown;
    public GameObject screen;
    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(TriggerVibration(other.GetComponent<GloveFollowing>().m_controller));
        
    }

    IEnumerator TriggerVibration(OVRInput.Controller controller)
    {
        OVRInput.SetControllerVibration(1f, 1f, controller);
        yield return new WaitForSeconds(0.1f);
        OVRInput.SetControllerVibration(1f, 0f, controller);
        chinDown.SetActive(true);
        screen.transform.rotation = Quaternion.Euler(0, 90f, 0);
        this.gameObject.transform.parent.gameObject.SetActive(false);
    }


}
