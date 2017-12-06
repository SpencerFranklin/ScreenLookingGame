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
		if (it)
			notItObj.SetActive (false);
		else
			itObj.SetActive (false);
	}

	void Awake(){
		DontDestroyOnLoad (this);
	}
	// Update is called once per frame
	void Update () {
		if (!it)
			timeNotIt += Time.deltaTime;
	}

	void OnCollisionEnter(Collision c){
		int l = c.gameObject.layer;
		if (l == LayerMask.NameToLayer("P1") || 
			l == LayerMask.NameToLayer("P2") ||
			l == LayerMask.NameToLayer("P3") ||
			l == LayerMask.NameToLayer("P4")) {

			string cl = LayerMask.LayerToName (l);

			if (!it) {
				GameObject o = GameObject.Find (cl);
				if (o.GetComponent<Player> ().it != true) {
					o.GetComponent<Player> ().it = true;
				} else {
					it = true;
				}
			}
		
		}

	}
}
