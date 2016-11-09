using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Panel : MonoBehaviour {

	public float fadeInDuration;
	private Image image;

	// Use this for initialization
	void Start () {
		image = GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update () {
//		Color colorNow = image.color;
		image.CrossFadeAlpha (0f, fadeInDuration, false);

	}
}
