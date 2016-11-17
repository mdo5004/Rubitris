using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {
	int score;
	Text scoreText;
	// Use this for initialization
	void Start () {
		score = 0;
		scoreText = GetComponent<Text> ();
		scoreText.text = score.ToString ();
	}

	public void ChangeScoreBy(int scoreChange)
	{
		score += scoreChange;
		scoreText.text = score.ToString ();
	}

	void Update() {
		if (score == 0){
			scoreText.color = Color.white;
		} else if (score > 0){
			scoreText.color = Color.green;
		} else if (score < 0){
			scoreText.color = Color.red;
		}
	}

}
