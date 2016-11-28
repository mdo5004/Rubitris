using UnityEngine;
using System.Collections;

public class ShadowScript : MonoBehaviour {
	Material shadowMaterial;
	float t;
	// Use this for initialization
	void Start () {
		t = 0f;
		Renderer rend = GetComponentInChildren<Renderer> ();
		Material[] mats;
		mats = rend.materials;
		foreach (Material material in mats) {
			if(material.name.Contains("Shadow")){
				shadowMaterial = material;
			}
		}
	}
	
	void FixedUpdate () {
		t += Time.deltaTime;
		float alpha = Mathf.Abs (Mathf.Sin (2f*t)) + 0.1f;
		Color c = shadowMaterial.color;
		c.a = alpha;
		shadowMaterial.color = c;
	}

}
