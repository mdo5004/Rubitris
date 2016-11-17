using UnityEngine;
using System.Collections;

public class PieceController : MonoBehaviour
{
	public float maxFallingRate = 10f;
	public float gravity = -0.5f;
	public GameObject[] fallingCube;
	Vector3 extents;
	int sqrtPointsPerFace;
	float timeSinceLastPiece;
	public float pieceTime;
	int activePieces = 0;
	public int maxActivePieces = 2;

	void Start ()
	{
		sqrtPointsPerFace = FindObjectOfType<PopulateLocations> ().sqrtPointsPerFace;
		extents = FindObjectOfType<PlayerScript> ().transform.GetComponent<BoxCollider> ().bounds.extents;
		timeSinceLastPiece = 2.1f;


	}

	void Update ()
	{
		timeSinceLastPiece += Time.deltaTime;
		if (activePieces < maxActivePieces && timeSinceLastPiece > pieceTime) {
			NewPiece ();
			timeSinceLastPiece = 0f;
		}
	}

	public void NewPiece ()
	{
		activePieces++;

		float i = Mathf.Round (Random.Range (0.51f, (float)sqrtPointsPerFace + 0.49f));
		float k = Mathf.Round (Random.Range (0.51f, (float)sqrtPointsPerFace + 0.49f));
		int originFace = Random.Range (0, 3);
		int cubeInd = Random.Range (0, fallingCube.Length);
		GameObject cube;
		Vector3 loc;
		switch (originFace) {
		case 0:
			loc = new Vector3 (-extents.x + (2 * i - 1) * extents.x / sqrtPointsPerFace, 40f, -extents.z + (2 * k - 1) * extents.z / sqrtPointsPerFace);
			cube = (GameObject)Instantiate (fallingCube [cubeInd], loc, Quaternion.identity);
			cube.GetComponent<FallingCube> ().downDirection = Vector3.down;
			break;
		case 1:
			loc = new Vector3 (-40f, -extents.x + (2 * i - 1) * extents.x / sqrtPointsPerFace, -extents.z + (2 * k - 1) * extents.z / sqrtPointsPerFace);
			cube = (GameObject)Instantiate (fallingCube [cubeInd], loc, Quaternion.Euler (0f, 0f, 90f));
			cube.GetComponent<FallingCube> ().downDirection = Vector3.right;
			break;
		case 2:
			loc = new Vector3 (-extents.x + (2 * i - 1) * extents.x / sqrtPointsPerFace, -extents.z + (2 * k - 1) * extents.z / sqrtPointsPerFace, -40f);
			cube = (GameObject)Instantiate (fallingCube [cubeInd], loc, Quaternion.Euler (-90f, 0f, 0f));
			cube.GetComponent<FallingCube> ().downDirection = Vector3.forward;
			break;
		}
		

	}

	public void PieceHasLanded ()
	{
		
		activePieces--;
	}



}

//1   -3.75
//2   -1.25
//3    1.25
//4    3.75