using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormMouthController : MonoBehaviour {

	void OnCollisionEnter (Collision col)
	{
		print("Mouth: "+col.gameObject.tag);
		if(col.gameObject.tag == "Worm") {
			Destroy(transform.parent.parent.gameObject);
		}
	}

}
