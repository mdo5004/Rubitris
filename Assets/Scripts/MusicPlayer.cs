using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour
{
	static MusicPlayer instance = null;

	public AudioClip startClip;
	public AudioClip gameClip;
	public AudioClip endClip;

	private AudioSource musicPlayer;

	void Start ()
	{
		if (instance != null && instance != this) {
			Destroy (gameObject);
			print ("Duplicate music player self-destructing!");
		} else {
			instance = this;
			GameObject.DontDestroyOnLoad (gameObject);
			musicPlayer = GetComponent<AudioSource> ();
		}
		
	}

	void OnLevelWasLoaded (int level)
	{
		Debug.Log ("Music player loaded level " + level);
		musicPlayer = GetComponent<AudioSource> ();
		musicPlayer.Stop ();
		switch (level) {
		case 0:
			musicPlayer.clip = startClip;
			break;
		case 1:
			musicPlayer.clip = gameClip;
			break;

		case 2:
			musicPlayer.clip = endClip;
			break;
		}
		musicPlayer.loop = true;
		musicPlayer.Play ();
	}
}
