using UnityEngine;
using System.Collections;

public class SetVolumeOnLoad : MonoBehaviour {

private MusicManager musicManager;

	// Use this for initialization
	void Start ()
	{
		musicManager = GameObject.FindObjectOfType<MusicManager> ();
		if (musicManager) {
			musicManager.ChangeVolume (PlayerPrefsManager.GetMasterVolume ());
		} else {
			Debug.LogWarning("No music manager found");
		}

	}
	

}
