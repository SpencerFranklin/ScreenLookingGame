using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {
	int[] pick = new int[]{-1, 1};
	int dir;
	// Use this for initialization
	void Start () {
		dir = pick[Random.Range (0, pick.Length)];
	}
	
	// Update is called once per frame
	void Update () {
		this.gameObject.transform.Rotate (0, dir * 3 * Time.deltaTime, 0);
	}
}
