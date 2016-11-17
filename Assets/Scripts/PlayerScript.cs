using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Lean.Touch;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
	//	Camera myCamera;

	public float speed;
	Quaternion rotation;
	Quaternion oldRot;

	bool isRotating;



	[Tooltip ("The text element we will display the swipe information in")]
	public Text InfoText;

	protected virtual void OnEnable ()
	{
		// Hook into the events we need
		LeanTouch.OnFingerSwipe += OnFingerSwipe;
	}

	protected virtual void OnDisable ()
	{
		// Unhook the events
		LeanTouch.OnFingerSwipe -= OnFingerSwipe;
	}

	public void OnFingerSwipe (LeanFinger finger)
	{
		// Make sure the info text exists
		if (InfoText != null) {
			// Store the swipe delta in a temp variable
			var swipe = finger.SwipeDelta;
			var left = new Vector2 (-1.0f, 0.0f);
			var right = new Vector2 (1.0f, 0.0f);
			var down = new Vector2 (0.0f, -1.0f);
			var up = new Vector2 (0.0f, 1.0f);
			
			if (SwipedInThisDirection (swipe, left) == true) {
				InfoText.text = "You swiped left!";
				rotateRightToLeft ();
			}
			
			if (SwipedInThisDirection (swipe, right) == true) {
				InfoText.text = "You swiped right!";
				rotateLeftToRight ();
			}
			
			if (SwipedInThisDirection (swipe, down) == true) {
				InfoText.text = "You swiped down!";
				if(finger.StartScreenPosition.x < Screen.width/2){
					rotateLeftToDown ();
				} else {
					rotateRightToDown ();
				}

			}
			
			if (SwipedInThisDirection (swipe, up) == true) {
				InfoText.text = "You swiped up!";
				if(finger.StartScreenPosition.x < Screen.width/2){
					rotateLeftToUp ();
				} else {
					rotateRightToUp ();
				}

			}

			if (SwipedInThisDirection (swipe, left + up) == true) {
				InfoText.text = "You swiped left and up!";
				rotateRightToUp ();
			}

			if (SwipedInThisDirection (swipe, left + down) == true) {
				InfoText.text = "You swiped left and down!";
				rotateLeftToDown ();
			}

			if (SwipedInThisDirection (swipe, right + up) == true) {
				InfoText.text = "You swiped right and up!";
				rotateLeftToUp ();
			}

			if (SwipedInThisDirection (swipe, right + down) == true) {
				InfoText.text = "You swiped right and down!";
				rotateRightToDown ();
			}
		}
	}

	private bool SwipedInThisDirection (Vector2 swipe, Vector2 direction)
	{
		// Find the normalized dot product between the swipe and our desired angle (this will return the acos between the vectors)
		var dot = Vector2.Dot (swipe.normalized, direction.normalized);

		// With 8 directions, each direction takes up 45 degrees (360/8), but we're comparing against dot product, so we need to halve it
		var limit = Mathf.Cos (22.5f * Mathf.Deg2Rad);

		// Return true if this swipe is within the limit of this direction
		return dot >= limit;
	}






	// Use this for initialization
	void Start ()
	{
//		myCamera = FindObjectOfType<Camera> ();

		oldRot = transform.rotation;
		rotation = Quaternion.AngleAxis (0f, Vector3.up);

	}

	void Update ()
	{
		
		// get keyboard input for the actual game
		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			rotateRightToLeft ();
		}
		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			rotateLeftToRight ();
		}
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			if (Input.GetKey (KeyCode.LeftShift)) {
				rotateLeftToUp ();
			} else {	
				rotateRightToUp ();
			}
		}
		if (Input.GetKeyDown (KeyCode.DownArrow)) {
			if (Input.GetKey (KeyCode.LeftShift)) {
				rotateLeftToDown ();
			} else {
				rotateRightToDown ();
			}
			
		}
		
		if (isRotating) {
			transform.rotation = Quaternion.Slerp (transform.rotation, rotation * oldRot, Time.deltaTime * speed);

		}



	}

	void rotateRightToLeft ()
	{
		finishRotating ();
		isRotating = true;
		oldRot = transform.rotation;
		rotation = Quaternion.AngleAxis (90f, Vector3.up);
	}

	void rotateLeftToRight ()
	{
		finishRotating ();
		isRotating = true;
		oldRot = transform.rotation;
		rotation = Quaternion.AngleAxis (90f, Vector3.down);
	}

	void rotateLeftToUp ()
	{
		finishRotating ();
		isRotating = true;
		oldRot = transform.rotation;
		rotation = Quaternion.AngleAxis (90f, Vector3.back);
	}

	void rotateLeftToDown ()
	{
		finishRotating ();
		isRotating = true;
		oldRot = transform.rotation;
		rotation = Quaternion.AngleAxis (90f, Vector3.forward);
	}

	void rotateRightToUp ()
	{
		finishRotating ();
		isRotating = true;
		oldRot = transform.rotation;
		rotation = Quaternion.AngleAxis (90f, Vector3.right);
	}

	void rotateRightToDown ()
	{
		finishRotating ();
		isRotating = true;
		oldRot = transform.rotation;
		rotation = Quaternion.AngleAxis (90f, Vector3.left);
	}



	public void finishRotating ()
	{
		isRotating = false;
		Vector3 curRot = transform.rotation.eulerAngles;
//		print ("Previous rotation: " + curRot);
		if (curRot.x > 0f && curRot.x < 45f) {
			curRot.x = 0f;
		} else if (curRot.x > 45f && curRot.x < 135f) {
			curRot.x = 90f;
		} else if (curRot.x > 135 && curRot.x < 225) {
			curRot.x = 180f;
		} else if (curRot.x > 225 && curRot.x < 315) {
			curRot.x = 270f;
		} else if (curRot.x > 315 && curRot.x < 405) {
			curRot.x = 0;
		}

		if (curRot.y > 0f && curRot.y < 45f) {
			curRot.y = 0f;
		} else if (curRot.y > 45f && curRot.y < 135f) {
			curRot.y = 90f;
		} else if (curRot.y > 135 && curRot.y < 225) {
			curRot.y = 180f;
		} else if (curRot.y > 225 && curRot.y < 315) {
			curRot.y = 270f;
		} else if (curRot.y > 315 && curRot.y < 405) {
			curRot.y = 0;
		}

		if (curRot.z > 0f && curRot.z < 45f) {
			curRot.z = 0f;
		} else if (curRot.z > 45f && curRot.z < 135f) {
			curRot.z = 90f;
		} else if (curRot.z > 135 && curRot.z < 225) {
			curRot.z = 180f;
		} else if (curRot.z > 225 && curRot.z < 315) {
			curRot.z = 270f;
		} else if (curRot.z > 315 && curRot.z < 405) {
			curRot.z = 0;
		}
//		print ("New rotation: " + curRot);
		transform.rotation = Quaternion.Euler (curRot);

	

	}
}
