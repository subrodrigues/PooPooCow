using UnityEngine;
using System.Collections;

public class FartScript : MonoBehaviour {
	AudioClip fart1Clip, fart2Clip, giggleClip;
	AudioSource fart1Source, fart2Source, giggleSource;

	int nextFartIndex;

	void Awake(){
		fart1Clip = Resources.Load ("fart2") as AudioClip;
		fart2Clip = Resources.Load ("fart3") as AudioClip;
		giggleClip = Resources.Load ("giggle") as AudioClip;

		fart1Source = AddAudio (fart1Clip, false, false, 1.0f);
		fart2Source = AddAudio (fart2Clip, false, false, 1.0f);
		giggleSource = AddAudio (giggleClip, false, false, 1.0f);

		nextFartIndex = getNextFartIndex();
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseUp ()
	{
		playFart();
	}

	void playFart(){
		switch(nextFartIndex){
			case 0:
				fart1Source.Play();
				nextFartIndex = getNextFartIndex();
			break;
			case 1:
				fart2Source.Play();
				nextFartIndex = getNextFartIndex();
			break;
			case 2:
				giggleSource.Play();
				nextFartIndex = getNextFartIndex();
			break;
		}
	}

	int getNextFartIndex(){
		return Random.Range(0, 3);
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
