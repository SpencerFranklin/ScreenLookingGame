using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour {

	public float delay;
	float timer = 0f;
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;

		if (timer >= delay) {
			Destroy (gameObject);
		}
	}
}
