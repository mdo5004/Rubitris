using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerPrefsManager : MonoBehaviour
{

	const string MASTER_VOLUME_KEY = "master_volume";
	const string DIFFICULTY_KEY = "difficulty";
	const string LEVEL_KEY = "level_unlocked_";

	// Volume get & set
	public static void SetMasterVolume (float volume)
	{
		if (volume >= 0f && volume <= 1f) {
			PlayerPrefs.SetFloat (MASTER_VOLUME_KEY, volume);
		} else {
			Debug.LogError ("Master volume out of range");
		}
	}
	public static float GetMasterVolume ()
	{
		return PlayerPrefs.GetFloat (MASTER_VOLUME_KEY);
	}

	// Progress get & set
	public static void UnlockLevel (int level)
	{
		if (level <= SceneManager.sceneCountInBuildSettings - 1) {
			PlayerPrefs.SetInt (LEVEL_KEY + level.ToString (), 1);
		} else {
			Debug.LogError ("Trying to unlock level not in build order");
		}
	}
	public static bool IsLevelUnlocked (int level)
	{
		if (level <= SceneManager.sceneCountInBuildSettings - 1) {
			int levelBeingChecked = PlayerPrefs.GetInt (LEVEL_KEY + level.ToString ());
			if (levelBeingChecked == 1f) {
				return true;
			}
		} else {
			Debug.LogError ("Trying to unlock level not in build order");
		}
	return false;
	}

	// Difficulty get & set
	public static void SetDifficulty (int difficulty)
	{
		if (difficulty >= 1 && difficulty <= 3) {
			PlayerPrefs.SetInt (DIFFICULTY_KEY, difficulty);
		} else {
		Debug.LogError("Difficulty must be between 1 and 3 (int)");
		}
	}
	public static float GetDifficulty(){
		return PlayerPrefs.GetInt (DIFFICULTY_KEY);
	}
		
}
