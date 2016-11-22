using UnityEngine;
using System.Collections;

public class ShadowScript : MonoBehaviour {
	Renderer rend;
	float lifeTime;
	// Use this for initialization
	void Start () {
		rend = GetComponent<Renderer> ();
		lifeTime = 0;

	}
	
	void FixedUpdate () {
		Color color = rend.material.color;
		color.a = (Mathf.Sin (3*lifeTime) + 2)/4;
		rend.material.color = color;
		lifeTime += Time.deltaTime;
	}

}
