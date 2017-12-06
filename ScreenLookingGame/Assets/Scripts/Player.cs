using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	public bool it;
	public int id;
	public float timeNotIt = 0;
	public GameObject notItObj;
	public GameObject itObj;

	public Player(int id_, bool it_){
		it = it_;
		id = id_;
	}

	void Start () {
		this.gameObject.transform.tag = "Playerr";
		if (it) {
			notItObj.SetActive (false);
			itObj.SetActive (true);

		} else {
			notItObj.SetActive (true);

			itObj.SetActive (false);
		}
	}

	void Awake(){
		DontDestroyOnLoad (this);
	}
	// Update is called once per frame
	void Update () {
		
		if (it) {
			notItObj.SetActive (false);
			itObj.SetActive (true);

		} else {
			notItObj.SetActive (true);

			itObj.SetActive (false);
		}
	}

	void OnCollisionEnter(Collision c){
		string n = c.gameObject.name.Substring(0,2);
		if (n == "P1" || 
			n == "P2" ||
			n == "P3" ||
			n == "P4") {

			Debug.Log ("Hit");
			if (!it) {
				GameObject o = GameObject.Find (n);
				if (o.GetComponent<Player> ().it != true) {
					o.GetComponent<Player> ().it = true;
				} else {
					it = true;
				}
			}
			Destroy (c.gameObject);
		}

	}
}
