using UnityEngine;
using System.Collections;

public class StartMenuGraphics : MonoBehaviour {

	public float maxFallingRate=10f;
	public float gravity = -0.5f;
	public GameObject[] fallingCube;
	public Vector3 extents = new Vector3 (20f, 20f, 20f);
	int sqrtPointsPerFace;
	private float timeSinceLastPiece;
	public float pieceTime;




	void Start(){
		sqrtPointsPerFace = 4;

		timeSinceLastPiece = 2.1f;
		pieceTime = 2f;
		NewPiece ();

	}

	void Update(){
		timeSinceLastPiece += Time.deltaTime;
//		if (timeSinceLastPiece > 1.5*pieceTime){
//			NewPiece ();  // This is a simple way to keep the pieces moving... 
//		}
	}
	public void NewPiece () {
		if (timeSinceLastPiece > pieceTime) {
			timeSinceLastPiece = 0f;
			float i = Mathf.Round (Random.Range (0.51f, (float)sqrtPointsPerFace + 0.49f));
			float k = Mathf.Round (Random.Range (0.51f, (float)sqrtPointsPerFace + 0.49f));
			int originFace = Random.Range (0, 3);
			int cubeInd = Random.Range (0, fallingCube.Length);
			GameObject cube;
			Vector3 loc;
			switch (originFace)
			{
			case 0:
				loc = new Vector3 (-extents.x + (2 * i - 1) * extents.x / sqrtPointsPerFace, 40f, -extents.z + (2 * k - 1) * extents.z / sqrtPointsPerFace);
				cube = (GameObject)Instantiate (fallingCube [cubeInd], loc, Quaternion.identity);
				cube.GetComponent<FallingCube>().downDirection = Vector3.down;
				break;
			case 1:
				loc = new Vector3 (-40f, -extents.x + (2 * i - 1) * extents.x / sqrtPointsPerFace, -extents.z + (2 * k - 1) * extents.z / sqrtPointsPerFace);
				cube = (GameObject)Instantiate (fallingCube [cubeInd], loc, Quaternion.Euler(0f,0f,90f));
				cube.GetComponent<FallingCube>().downDirection = Vector3.right;
				break;
			case 2:
				loc = new Vector3 (-extents.x + (2 * i - 1) * extents.x / sqrtPointsPerFace, -extents.z + (2 * k - 1) * extents.z / sqrtPointsPerFace, -40f);
				cube = (GameObject)Instantiate (fallingCube [cubeInd], loc, Quaternion.Euler(-90f,0f,0f));
				cube.GetComponent<FallingCube>().downDirection = Vector3.forward;
				break;
			}




			pieceTime -= 0.05f; // accelerate the game
		}

	}
}
