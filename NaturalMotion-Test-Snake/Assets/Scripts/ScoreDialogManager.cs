using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreDialogManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		(GameObject.Find("txtScore").GetComponent<Text>()).text = ApplicationModel.score.ToString();
	}
	
	public void Play() {
		ApplicationModel.score = 0;
		SceneManager.LoadScene("Level1");
	}

	public void Return() {
		SceneManager.LoadScene("Menu");
	}
}
