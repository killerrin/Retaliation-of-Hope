using UnityEngine;
using System.Collections;

[RequireComponent (typeof(AudioSource))]
public class AudioManager : MonoBehaviour {
	public static AudioManager me;

	public AudioSource audioSource;
	public AudioClip[] audioClips;

	void Start () {
		DontDestroyOnLoad(this);

		audioSource.loop = true;
		me = this;
	}
	
	public void PlayClip(int id)
	{
		if (audioSource.clip == audioClips[id]) { return; }

		audioSource.clip = audioClips[id];
		Play();
	}
	public void Play() { audioSource.Play(); }
	public void Pause() { audioSource.Pause(); }
	public void Stop() { audioSource.Stop(); }
}
