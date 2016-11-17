using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Facet : MonoBehaviour
{

	public bool isOccupied;

	public string isOccupiedBy;
	private GameObject occupier;

	public GameObject Occupier{
		get {
			return occupier;
		}
		set {
			occupier = value;
			isOccupiedBy = value.tag;
			isOccupied = true;
		}
	}


	public static Dictionary<string, Facet> playingFacets = new Dictionary<string, Facet> ();
	public static Dictionary<string, string> faceColor = new Dictionary<string, string> ();

	Collider[] neighbors;

	public void CheckNeighbors (string tileName)
	{
		Vector3 extents = FindObjectOfType<PopulateLocations> ().extents;
		float divisions = FindObjectOfType<PopulateLocations> ().sqrtPointsPerFace;
		float radius = 2f * extents.x / divisions;
		neighbors = Physics.OverlapSphere (transform.position, radius);
		int similarAdjacents = 0;

		foreach (Collider neighbor in neighbors) {
			int similarCoordinates = 0;
			if (neighbor.CompareTag ("Facet")) { // If the neighbor is a facet
				if (Mathf.Abs (neighbor.transform.position.x - transform.position.x) < 0.1) { // and it shares
					similarCoordinates++;
				}
				if (Mathf.Abs (neighbor.transform.position.y - transform.position.y) < 0.1) { // two coordinates
					similarCoordinates++;
				}
				if (Mathf.Abs (neighbor.transform.position.z - transform.position.z) < 0.1) { // then this is an adjacent space
					similarCoordinates++;
				}
				// Is it occupied by a tile of the same name?
				if ((similarCoordinates == 2) && (neighbor.GetComponent<Facet> ().isOccupiedBy == tileName)) {
					similarAdjacents++;

				}
			}
		}
	}

	public void CheckFace ()
	{
		int sqrtPointsPerFace = FindObjectOfType<PopulateLocations> ().sqrtPointsPerFace;
		string face = name.Substring (0, 7);

		int numberOccupied = 0;
		string colorOfOccupier = "empty";
		bool faceIsOneColor = true;


		for (int i = 1; i <= sqrtPointsPerFace; i++) {
			for (int j = 1; j <= sqrtPointsPerFace; j++) {
				if (playingFacets [face+i+j].isOccupied){
					numberOccupied++;
					if (colorOfOccupier == "empty"){
						colorOfOccupier = playingFacets [face + i + j].isOccupiedBy;
					} else if(colorOfOccupier != playingFacets [face + i + j].isOccupiedBy) {
						faceIsOneColor = false;
					}
				}
			}
		}
		if (numberOccupied == sqrtPointsPerFace*sqrtPointsPerFace && faceIsOneColor){
			// face is full and is all one color
			faceColor [face] = colorOfOccupier;
			checkFaces ();
		}
		Debug.Log ("Number occupied = " + numberOccupied);

	}

	void checkFaces() { // are all faces one color? are they all different?
		Dictionary<string, bool> numberOfColors = new Dictionary<string, bool>();
		int totalNumberOfColors = FindObjectOfType<PieceController> ().fallingCube.Length;
		string[] faceNames = FindObjectOfType<PopulateLocations> ().faceNames;
		Debug.Log ("Face is full. faceColor.Count = " + faceColor.Count);
		if (faceColor.Count == 6){
			// Level beaten
			foreach (string face in faceNames){
				if (faceColor.ContainsKey (face)) {
					Debug.Log ("checkFaces is checking face " + faceColor [face]);
					numberOfColors [faceColor [face]] = true; // create a dictionary of colors to see if they're all present
				}
			}
			if (numberOfColors.Count == faceColor.Count){
				FindObjectOfType<LevelManager> ().GetComponent<LevelManager> ().LoadLevel ("Win");
			}
		}
	}


	// Use this for initialization
	void Start ()
	{
		playingFacets.Add (name, GetComponent<Facet> ());
	}

	void Awake ()
	{
		playingFacets.Clear ();
	}
	// Update is called once per frame
	void Update ()
	{
	
	}
}
