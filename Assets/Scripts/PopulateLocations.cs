using UnityEngine;
using System.Collections;

public class PopulateLocations : MonoBehaviour
{
	[HideInInspector]
	public Vector3 extents;
	public int sqrtPointsPerFace;
	public GameObject facet;
	public GameObject[,,] facets;
	GameObject facetInstance;
	public string[] faceNames = new string[] { "x+Facet", "x-Facet", "y+Facet", "y-Facet", "z+Facet", "z-Facet" };

	void Start ()
	{

		extents = GetComponentInChildren <BoxCollider> ().bounds.extents;


		// for the x faces
		for (int j = 1; j <= sqrtPointsPerFace; j++) {
			for (int k = 1; k <= sqrtPointsPerFace; k++) {
				Vector3 loc = new Vector3 (extents.x + 0.11f, -extents.y + (2 * j - 1) * extents.y / sqrtPointsPerFace, -extents.z + (2 * k - 1) * extents.z / sqrtPointsPerFace);
				facetInstance = (GameObject)Instantiate (facet, loc, Quaternion.identity);
				facetInstance.name = faceNames[0] + j + k;
				facetInstance.transform.RotateAround (facetInstance.transform.position, Vector3.forward, 270f);
				facetInstance.transform.SetParent (FindObjectOfType<PlayerScript> ().transform);

				loc = new Vector3 (-extents.x-0.11f, -extents.y + (2 * j - 1) * extents.y / sqrtPointsPerFace, -extents.z + (2 * k - 1) * extents.z / sqrtPointsPerFace);
				facetInstance = (GameObject)Instantiate (facet, loc, Quaternion.identity);
				facetInstance.name = faceNames[1] + j + k;
				facetInstance.transform.RotateAround (facetInstance.transform.position, Vector3.forward, 90f);
				facetInstance.transform.SetParent (FindObjectOfType<PlayerScript> ().transform);

			}
		}
		// for the y faces
		for (int i = 1; i <= sqrtPointsPerFace; i++) {
			for (int k = 1; k <= sqrtPointsPerFace; k++) {
				Vector3 loc = new Vector3 (-extents.x + (2 * i - 1) * extents.x / sqrtPointsPerFace, extents.y + 0.11f, -extents.z + (2 * k - 1) * extents.z / sqrtPointsPerFace);
				facetInstance = (GameObject)Instantiate (facet, loc, Quaternion.identity);
				facetInstance.name = faceNames[2] + i + k;

				facetInstance.transform.SetParent (FindObjectOfType<PlayerScript> ().transform);		

				loc = new Vector3 (-extents.x + (2 * i - 1) * extents.x / sqrtPointsPerFace, -extents.y-0.11f, -extents.z + (2 * k - 1) * extents.z / sqrtPointsPerFace);
				facetInstance = (GameObject)Instantiate (facet, loc, Quaternion.identity);
				facetInstance.name = faceNames[3] + i + k;

				facetInstance.transform.RotateAround (facetInstance.transform.position, Vector3.forward, 180f);
				facetInstance.transform.SetParent (FindObjectOfType<PlayerScript> ().transform);

			}
		}
		// for the z faces
		for (int i = 1; i <= sqrtPointsPerFace; i++) {
			for (int j = 1; j <= sqrtPointsPerFace; j++) {
				Vector3 loc = new Vector3 (-extents.x + (2 * i - 1) * extents.x / sqrtPointsPerFace, -extents.y + (2 * j - 1) * extents.y / sqrtPointsPerFace, extents.z + 0.11f);
				facetInstance = (GameObject)Instantiate (facet, loc, Quaternion.identity);
				facetInstance.name = faceNames[4] + i + j;

				facetInstance.transform.RotateAround (facetInstance.transform.position, Vector3.right, 90f);
				facetInstance.transform.SetParent (FindObjectOfType<PlayerScript> ().transform);

				loc = new Vector3 (-extents.x + (2 * i - 1) * extents.x / sqrtPointsPerFace, -extents.y + (2 * j - 1) * extents.y / sqrtPointsPerFace, -extents.z-0.11f);
				facetInstance = (GameObject)Instantiate (facet, loc, Quaternion.identity);
				facetInstance.name = faceNames[5] + i + j;

				facetInstance.transform.RotateAround (facetInstance.transform.position, Vector3.right, 270f);
				facetInstance.transform.SetParent (FindObjectOfType<PlayerScript> ().transform);

			}
		}

	}

}