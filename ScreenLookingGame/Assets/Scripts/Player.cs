using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	public bool it;
	public int id;
	public float timeNotIt = 0;


	public Player(int id_, bool it_){
		it = it_;
		id = id_;
	}

	void Start () {
		
	}

	void Awake(){
		DontDestroyOnLoad (this);
	}
	// Update is called once per frame
	void Update () {
		if (!it)
			timeNotIt += Time.deltaTime;
	}
}
