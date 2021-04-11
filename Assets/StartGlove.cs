using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class StartGlove : MonoBehaviour
{
    public GameObject punchbag;
    public Text text;
    public Text combo;
    private Vector3 position = Vector3.zero;
    private Quaternion rotation = new Quaternion(0, 0, 0, 0);
    public AudioClip _audio;
    private AudioSource ac;
    private void Start()
    {
        ac = this.GetComponentInChildren<AudioSource>();
        position = transform.position;
        rotation = transform.rotation;
    }
    private void Update()
    {
        transform.position = position;
        transform.rotation = new Quaternion(0, rotation.y, 0, rotation.w);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("LeftGlove") || other.gameObject.name.Equals("RightGlove"))
        {
            punchbag.SetActive(true);
            text.gameObject.SetActive(true);
            combo.gameObject.SetActive(true);
            punchbag.GetComponent<ControllerDetection>()._TriggerVibration(other.GetComponent<GloveFollowing>().m_controller);
            ac.PlayOneShot(_audio, _audio.length);
            gameObject.SetActive(false);
        }
    }
}
