using UnityEngine;
using System.Collections;

public class Background : MonoBehaviour {
	float h;
	// Use this for initialization
	void Start () {
		h = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<SpriteRenderer> ().color = Color.HSVToRGB (h / 255f, 1f, 1f);

		if (h < 254){
			h+=0.1f;
		} else {
			h = 0f;
		}
	}
}
