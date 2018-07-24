using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public GameObject snake;
	public float scorePeriod = 5f;
	public int scoreGain = 10;
	public float spawnPeriod = 4f;
	public int maxApples = 4;
	public Transform applePrefab;

	private float timer;
	private float spawnTimer;
	private int ticksToSpawn = 1;
	private int currentApples;
	private int score;
	private Text txtScore; 
	private GameObject spawnCube;

	// Use this for initialization
	void Start () {
		timer = 0;
		score = 0;
		txtScore = GameObject.Find("txtScore").GetComponent<Text>();
		updateScoreUI();

		currentApples = 0;
		spawnTimer = 0;
		spawnCube = GameObject.Find("SpawnCube").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if (snake != null) {
			timer += Time.deltaTime;
			spawnTimer += Time.deltaTime;

			if (timer > scorePeriod) {
				score += scoreGain;

				updateScoreUI();

				timer = 0;
			}

			if (spawnTimer > spawnPeriod) {
				if (currentApples >= maxApples) return;

				float w = spawnCube.transform.localScale.x;
				float h = spawnCube.transform.localScale.y;

				float rand = Random.value;
				Vector3 randomPosition = spawnCube.transform.position/2 + new Vector3(w * rand, 0, h * rand);

				var hitColliders = Physics.OverlapSphere(randomPosition, 2);
				if (hitColliders.Length > 1) return; //Only collide with the spawnCube

				Instantiate(applePrefab, spawnCube.transform.position + new Vector3(w * rand, 0, h * rand), applePrefab.transform.rotation);
				currentApples += 1;

				snake.GetComponent<WormController>().addBodyPart();

				spawnTimer = 0;
			}
		}
		else {
			print("End of Game");
			ApplicationModel.score = score;
			SceneManager.LoadScene("ScoreDialog");
		}
	}

	public void addScore(int score) {
		this.score += score;
		updateScoreUI();
	}

	public void decreaseApples() {
		currentApples -= 1;
	}

	private void updateScoreUI() {
		txtScore.text = score.ToString();
	}
}
