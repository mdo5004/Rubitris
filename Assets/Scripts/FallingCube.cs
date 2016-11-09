using UnityEngine;
using System.Collections;

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
		newZdelta = playingSurfaceScale.z / n - transform.FindChild ("DisplayObject").transform.localScale.z;
		transform.FindChild("DisplayObject").transform.localScale += new Vector3(newXdelta , 0f, newZdelta);
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
				FindObjectOfType<PieceController> ().NewPiece ();


			if (other.GetComponent<Facet> ().isOccupied == true) {
				Destroy (other.GetComponent<Facet> ().Occupier);
			} 
			other.GetComponent<Facet> ().Occupier = gameObject;
			other.GetComponent<Facet> ().CheckFace ();
		}
	}

	void OnCollisionEnter(Collision collision){
		Debug.Log (collision.collider.tag);
	}

	void PlayExplosion(){
		Destroy (shadowInstance);
		Destroy (gameObject);
		FindObjectOfType<PieceController> ().NewPiece ();
	}
}
