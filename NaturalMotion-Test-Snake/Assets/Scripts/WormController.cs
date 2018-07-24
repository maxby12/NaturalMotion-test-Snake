using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormController : MonoBehaviour {

	// ATTRIBUTES
	public List<Transform> bodyParts = new List<Transform>();
	public float speed;
	public float minSpeed;
	public float maxSpeed;
	public float accelerationTime = 60;
	public float rotationSpeed = 50;
	public float minDistance = 0.25f;
	public int initialSize = 4;

	public GameObject bodyPrefab;

	private float distance;
	private float time;
	private Transform currentBodyPart;
	private Transform previousBodyPart;

	// METHODS AND EVENTS
	void Start ()
	{
		for (int i = 0; i < initialSize; i++) {
			addBodyPart();
		}

		time = 0;
	}

	void Update ()
	{
		Movement();

		speed = Mathf.SmoothStep(minSpeed, maxSpeed, time / accelerationTime);
		time += Time.deltaTime;


		if (Input.GetKey(KeyCode.Q)) addBodyPart();
	}

	void OnCollisionEnter (Collision col)
	{
		
		if(col.gameObject.tag == "Wall") {
			Destroy(gameObject);
		}
	}

	public void Movement() {
		float currentSpeed = speed;

		if (Input.GetKey (KeyCode.W))
			currentSpeed *= 1.5f;

		bodyParts[0].Translate(bodyParts [0].forward * currentSpeed * Time.smoothDeltaTime, Space.World);

		if (Input.GetAxis ("Horizontal") != 0)
			bodyParts [0].Rotate(Vector3.up * rotationSpeed * Time.deltaTime * Input.GetAxis ("Horizontal"));

		for (int i = 1; i < bodyParts.Count; i++) {
			currentBodyPart = bodyParts [i];
			previousBodyPart = bodyParts [i - 1];

			distance = Vector3.Distance(previousBodyPart.position, currentBodyPart.position);
			Vector3 newPosition = previousBodyPart.position;

			newPosition.y = bodyParts[0].position.y;

			float time = Time.deltaTime * distance / minDistance * currentSpeed;

			if (time > 0.5f)
				time = 0.5f;

			currentBodyPart.position = Vector3.Slerp(currentBodyPart.position, newPosition, time);
			currentBodyPart.rotation = Quaternion.Slerp(currentBodyPart.rotation, previousBodyPart.rotation, time);
		}
	}

	public void addBodyPart() {
		Transform newPart = (Instantiate(bodyPrefab, bodyParts[bodyParts.Count - 1].position, bodyParts[bodyParts.Count - 1].rotation) as GameObject).transform;
		newPart.SetParent(transform);

		bodyParts.Add(newPart);
	}

}
