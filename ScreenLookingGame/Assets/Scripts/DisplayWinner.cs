using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class DisplayWinner : MonoBehaviour {
	float i = -1;
	int bestPlayer = -1;
	bool disp = false;
	// Use this for initialization
	void End () {
		Text t = this.gameObject.GetComponent<Text> ();


		Debug.Log (bestPlayer);
		if (bestPlayer == 0) {
			t.text = "Top Left player wins!";
		}
		if (bestPlayer == 1) {
			t.text = "Top Right player wins!";
		}
		if (bestPlayer == 2) {
			t.text = "Bottom Left player wins!";
		}
		if (bestPlayer == 3) {
			t.text = "Bottom Right player wins!";
		}
		if (bestPlayer == -1) {
			t.text = "Broke";
		}
		bool disp = true;

	}
	
	// Update is called once per frame
	void Update () {
		if (!disp) {
			foreach (GameObject o in GameObject.FindGameObjectsWithTag("Playerr")) {
				if (o.GetComponent<Player> ().timeNotIt > i) {
					i = o.GetComponent<Player> ().timeNotIt;
					bestPlayer = o.GetComponent<Player> ().id;
				}
			}
			End ();
		}

	}
}
