using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Splash : MonoBehaviour {

public AudioClip startClip;

	// Use this for initialization
	void Start () {
		AudioSource.PlayClipAtPoint (startClip, Vector3.zero);
		Invoke("EndSplash",4f);
	}
	void EndSplash() {
		SceneManager.LoadScene ("Start Menu");
	}
	// Update is called once per frame
	void Update () {
	
	}
}
