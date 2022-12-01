using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSystem : MonoBehaviour
{
	public static SoundSystem instance;
	public AudioSource ac;
	public GameObject soundProp;
	
	void Awake()
	{
		instance = this;
	}

	public void PlaySound(AudioClip soundClip){
        if (soundClip == null) { return; }

		GameObject sound = Instantiate(soundProp, transform.position, Quaternion.identity);
		ac = sound.GetComponent<AudioSource>();

		ac.clip = soundClip;
		
		ac.Play();
		
		Destroy(sound, soundClip.length);
	}
}
