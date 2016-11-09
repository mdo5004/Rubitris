using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour {
//	Camera myCamera;

	public float speed;
	Quaternion rotation;
	Quaternion oldRot;

	bool isRotating;
	float t;
	// Use this for initialization
	void Start () {
//		myCamera = FindObjectOfType<Camera> ();

		oldRot = transform.rotation;
		rotation = Quaternion.AngleAxis (0f, Vector3.up);
		t = 0f;
	}
	
	void Update ()
	{
	if(SceneManager.GetActiveScene().name == "Start Menu"){
	// autoplay for start menu appearances
		if (t > 3f) {
			finishRotating();
			isRotating = true;
			oldRot = transform.rotation;
			t = 0f;
			int i = (int)Random.Range (0, 4);
			switch(i) {
				case 0:
					rotation = Quaternion.AngleAxis (90f, Vector3.up);
					break;
				case 1:
					rotation = Quaternion.AngleAxis (90f, Vector3.down);
					break;
				case 2:
					rotation = Quaternion.AngleAxis (90f, Vector3.right);
					break;
				case 3:
					rotation = Quaternion.AngleAxis (90f, Vector3.left);
					break;
									}
		}
			t += Time.deltaTime;

	} else {
	// get input for the actual game
		if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow)){
			finishRotating();
			isRotating = true;
			oldRot = transform.rotation;
		}
		if(Input.GetKeyDown(KeyCode.LeftArrow))
		{
			rotation = Quaternion.AngleAxis (90f, Vector3.up);
		}
		if(Input.GetKeyDown(KeyCode.RightArrow))
		{
			rotation = Quaternion.AngleAxis (90f, Vector3.down);
		}
		if(Input.GetKeyDown(KeyCode.UpArrow))
		{
			if(Input.GetKey(KeyCode.LeftShift)){
					rotation = Quaternion.AngleAxis (90f, Vector3.forward);
			} else {	
				rotation = Quaternion.AngleAxis (90f, Vector3.right);
			}
		}
		if(Input.GetKeyDown(KeyCode.DownArrow))
		{
			if(Input.GetKey(KeyCode.LeftShift)){
			rotation = Quaternion.AngleAxis (90f, Vector3.back);
			}else{
			rotation = Quaternion.AngleAxis (90f, Vector3.left);
			}
			
		}
	}
		if (isRotating) {
			transform.rotation = Quaternion.Slerp (transform.rotation, rotation * oldRot, Time.deltaTime * speed);

		}



	}

	public void finishRotating(){
		isRotating = false;
		Vector3 curRot = transform.rotation.eulerAngles;
//		print ("Previous rotation: " + curRot);
		if (curRot.x > 0f && curRot.x < 45f){
			curRot.x = 0f;
		}else if(curRot.x > 45f && curRot.x < 135f){
			curRot.x = 90f;
		}else if(curRot.x > 135 && curRot.x < 225){
			curRot.x = 180f;
		}else if(curRot.x > 225 && curRot.x < 315){
			curRot.x = 270f;
		}else if(curRot.x > 315 && curRot.x < 405){
			curRot.x = 0;
		}

		if (curRot.y > 0f && curRot.y < 45f){
			curRot.y = 0f;
		}else if(curRot.y > 45f && curRot.y < 135f){
			curRot.y = 90f;
		}else if(curRot.y > 135 && curRot.y < 225){
			curRot.y = 180f;
		}else if(curRot.y > 225 && curRot.y < 315){
			curRot.y = 270f;
		}else if(curRot.y > 315 && curRot.y < 405){
			curRot.y = 0;
		}

		if (curRot.z > 0f && curRot.z < 45f){
			curRot.z = 0f;
		}else if(curRot.z > 45f && curRot.z < 135f){
			curRot.z = 90f;
		}else if(curRot.z > 135 && curRot.z < 225){
			curRot.z = 180f;
		}else if(curRot.z > 225 && curRot.z < 315){
			curRot.z = 270f;
		}else if(curRot.z > 315 && curRot.z < 405){
			curRot.z = 0;
		}
//		print ("New rotation: " + curRot);
		transform.rotation = Quaternion.Euler (curRot);

	

	}
}
