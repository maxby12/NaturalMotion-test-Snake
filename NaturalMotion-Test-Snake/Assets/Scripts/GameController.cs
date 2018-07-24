using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public GameObject snake;
	public float scorePeriod = 5f;
	public int scoreGain = 10;

	private float timer;
	private int score;
	private Text txtScore; 

	// Use this for initialization
	void Start () {
		timer = 0;
		score = 0;
		txtScore = GameObject.Find("txtScore").GetComponent<Text>();

		txtScore.text = score.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		if (snake != null) {
			timer += Time.deltaTime;
			if (timer > scorePeriod) {
				score += scoreGain;

				txtScore.text = score.ToString();

				timer = 0;
			}
		}
		else {
			print("End of Game");
			ApplicationModel.score = score;
			SceneManager.LoadScene("ScoreDialog");
		}
	}
}
