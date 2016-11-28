using UnityEngine;
using System.Collections;

[RequireComponent (typeof(BoxCollider))]
[RequireComponent (typeof(Rigidbody))]

public class FallingCube : MonoBehaviour {
	
	RaycastHit rayHit;
	public GameObject shadow;
	GameObject shadowInstance;
	PlayerScript player;
	public LayerMask shadowMask;

	public Vector3 downDirection;
	float speed;
	bool falling;
	float maxSpeed;
	float gravity;


	Vector3 playingSurfaceScale;
	float n;

	void Start () {
		speed = 0f;

		falling = true;
		maxSpeed = FindObjectOfType<PieceController> ().maxFallingRate;
		gravity = FindObjectOfType<PieceController> ().gravity;

		player = FindObjectOfType<PlayerScript> ();
		playingSurfaceScale = player.transform.FindChild ("PlayingSurface").transform.localScale;

		n = (float)FindObjectOfType<PopulateLocations> ().sqrtPointsPerFace;

		float newXdelta, newZdelta;
		newXdelta = playingSurfaceScale.x / n - transform.FindChild ("DisplayObject").transform.localScale.x;
		newZdelta = playingSurfaceScale.y / n - transform.FindChild ("DisplayObject").transform.localScale.y;
		transform.FindChild("DisplayObject").transform.localScale += new Vector3(newXdelta , newZdelta,  0f);
		GetComponent<BoxCollider> ().size = new Vector3 (playingSurfaceScale.x / n, 0.2f, playingSurfaceScale.z / n);
		shadowInstance = (GameObject)Instantiate (shadow, rayHit.point, Quaternion.identity * transform.rotation);

		CastShadow ();
	}
	
	void Update ()
	{
		if (falling) {
			MovePiece ();
			CastShadow ();
		} else {
			int zed = 0;
			transform.localPosition = new Vector3 (zed, zed, zed);
			transform.localRotation = Quaternion.Euler (zed, zed, zed);
		}
	}

	void MovePiece(){
		transform.Translate (Vector3.down * speed *Time.deltaTime);
		if (speed <= maxSpeed){
			speed-= gravity;
		}

	}

	void CastShadow(){
		Ray landingRay = new Ray();
		landingRay.origin = transform.position;
		landingRay.direction = downDirection;
		Physics.Raycast (landingRay, out rayHit, Mathf.Infinity, shadowMask);
		Debug.DrawLine (transform.position, rayHit.point);
		if (shadowInstance){
			shadowInstance.transform.position = rayHit.point-0.1f*downDirection;
		}

		float newXdelta, newZdelta;
		newXdelta = playingSurfaceScale.x / n - shadowInstance.transform.localScale.x;
		newZdelta = playingSurfaceScale.z / n - shadowInstance.transform.localScale.z;
		shadowInstance.transform.localScale += new Vector3(newXdelta , 0f, newZdelta);

	}
	void OnTriggerEnter(Collider other){

		if (other.CompareTag("Facet")){
			
				player.BroadcastMessage ("finishRotating");

				speed = 0f;
				falling = false;


				transform.SetParent (other.transform,true);
				transform.localPosition.Set (0f, 0f, 0f);

				Destroy (shadowInstance);
				FindObjectOfType<PieceController> ().PieceHasLanded ();


			if (other.GetComponent<Facet> ().isOccupied == true) {
				if (other.GetComponent<Facet> ().isOccupiedBy != tag) {
					FindObjectOfType<ScoreKeeper> ().ChangeScoreBy (-1);
					Debug.Log ("Replaced by a different color");
				} else {
					Debug.Log ("Replaced by the same color");
				}

				Destroy (other.GetComponent<Facet> ().Occupier);
			} else {
				FindObjectOfType<ScoreKeeper> ().ChangeScoreBy (1);
			}

			other.GetComponent<Facet> ().Occupier = gameObject;
			other.GetComponent<Facet> ().CheckFace ();
		}
	}




}
