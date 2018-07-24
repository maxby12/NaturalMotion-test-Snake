using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormMouthController : MonoBehaviour {

	private bool applePicked;
	private float timer;
	private GameController gc;

	void Start() {
		applePicked = false;
		timer = 0;
		gc = GameObject.Find("GameController").GetComponent<GameController>();
	}

	void FixedUpdate() {
		if (timer > 1f) {
			applePicked = false;
			timer = 0;
		} else timer += Time.deltaTime;
	}

	void OnCollisionEnter (Collision col)
	{
		print("Mouth: "+col.gameObject.tag);

		if(col.gameObject.tag == "Apple" && !applePicked) {
			Destroy(col.gameObject);
			gc.addScore(25);
			gc.decreaseApples();
			applePicked = true;
		}
		else if(col.gameObject.tag == "Apple Trunk" && !applePicked) {
			Destroy(col.transform.parent.gameObject);
			Destroy(col.gameObject);
			GameObject.Find("GameController").GetComponent<GameController>().addScore(25);
			applePicked = true;
		}
		else if(col.gameObject.tag == "Worm") {
			Destroy(transform.parent.parent.gameObject);
		}
	}

}
