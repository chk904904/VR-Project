using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioController : MonoBehaviour
{
	public AudioSource audioPlayer;
	public static audioController AC;
	//public float maxBGMvol = 0.15f;
    float soundVel = 0;
    float smoothTime = 0.3f;
    private Dictionary<string, AudioClip> sounds;

    // Start is called before the first frame update
    void Awake()
    {
        AC = this;
        sounds = new Dictionary<string, AudioClip>();
        sounds.Add("1", Resources.Load("1", typeof(AudioClip)) as AudioClip);
        sounds.Add("2", Resources.Load("2", typeof(AudioClip)) as AudioClip);
        sounds.Add("3", Resources.Load("3", typeof(AudioClip)) as AudioClip);
        sounds.Add("4", Resources.Load("4", typeof(AudioClip)) as AudioClip);
        sounds.Add("5", Resources.Load("5", typeof(AudioClip)) as AudioClip);
        sounds.Add("6", Resources.Load("6", typeof(AudioClip)) as AudioClip);
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound(string soundID, float vol = 0.5f)
    {
        AudioClip clip = sounds[soundID];
        audioPlayer.PlayOneShot(clip, vol);
    }

    public void muteAll(){
        audioPlayer.volume = 0; 
    }

    public void unmuteAll(){
		audioPlayer.volume = 0.5f; 
    }

    public void StopSound(){
        audioPlayer.Stop();
    }

}

//How to use this:
//AudioController.AC.PlayPlayer3(audioName);
