using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour {

	public AudioClip[] levelMusicChangeArray;
	private AudioSource musicPlayer;

	void Awake() {
        DontDestroyOnLoad(transform.gameObject);
    }
    void Start() {
		musicPlayer = GetComponent<AudioSource>();
    }
	void OnLevelWasLoaded(int level) {
		AudioClip thisLevelsMusic = levelMusicChangeArray [level];

		if (thisLevelsMusic){
			musicPlayer.clip = thisLevelsMusic;
			musicPlayer.Play ();
			}
        
    }
	public void ChangeVolume(float val) {
		musicPlayer.volume = val;
	}


}

