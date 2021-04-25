using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialWarning : MonoBehaviour
{
    public AudioClip _audio;
    public GameObject startGlove;
    public GameObject screen;
    private AudioSource ac;
    // Start is called before the first frame update
    void Start()
    {
        ac = this.GetComponent<AudioSource>();
        StartCoroutine(activateStartGlove());
    }

    IEnumerator activateStartGlove()
    {
        ac.PlayOneShot(_audio, _audio.length);
        yield return new WaitForSeconds(_audio.length);
        screen.transform.rotation = Quaternion.Euler(0, -90f, 0);
        startGlove.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
