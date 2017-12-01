using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour {
	public enum Direction {North, South, East, West	};
	Global ins;
	// Use this for initialization
	void Start () {
		
	}

	void Awake(){
		ins = this;
	}
	// Update is called once per frame
	void Update () {
		
	}
}
