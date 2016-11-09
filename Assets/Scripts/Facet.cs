using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Facet : MonoBehaviour
{

	public bool isOccupied { get; set; }

	public string isOccupiedBy { get; set; }

	public int occupierID { get; set; }

	public GameObject Occupier {
		get 
		{
			return Occupier;
		}

		set
		{
			isOccupied = true;
			isOccupiedBy = value.tag;
			//CheckNeighbors ();
		}

	}


	public static Dictionary<string, Facet> playingFacets = new Dictionary<string, Facet> ();


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

		Debug.Log ("Check face " + face);
		for (int i = 1; i <= sqrtPointsPerFace; i++) {
			for (int j = 1; j <= sqrtPointsPerFace; j++) {
				if (playingFacets [face+i+j].isOccupied)
				Debug.Log (playingFacets [face+i+j].isOccupiedBy);
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
