using UnityEngine;
using System.Collections;

public class PooPooCowScript : MonoBehaviour {
	AudioClip pooPooCowClip;
	AudioSource pooPooCowSource;

	void Awake(){
		pooPooCowClip = Resources.Load ("poopoocow") as AudioClip;
		pooPooCowSource = AddAudio (pooPooCowClip, false, false, 1.0f);
	}

	void OnMouseEnter ()
	{
		if(!pooPooCowSource.isPlaying)
			pooPooCowSource.Play();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	AudioSource AddAudio (AudioClip clip, bool loop, bool playAwake, float vol)
	{
		AudioSource newAudio = gameObject.AddComponent<AudioSource> ();
		newAudio.clip = clip;
		newAudio.loop = loop;
		newAudio.playOnAwake = playAwake;
		newAudio.volume = vol;
		return newAudio;
	}
}
