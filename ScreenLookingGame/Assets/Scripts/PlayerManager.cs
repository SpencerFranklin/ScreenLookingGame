using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;


public class PlayerManager : MonoBehaviour {

	public int numPlayers = 4;
	List<Player> playerList = new List<Player> ();
	List<GameObject> roomList = new List<GameObject>();
	public List<GameObject> playersInScene = new List<GameObject> ();

	int rounds = 3;

	// Use this for initialization
	void Start () {
		//SceneManager.LoadScene ("EndGame");
		List<int> usedSpawns = new List<int> ();
		GenerateMap.ins.StartMap ();
		for (int i = 0; i < numPlayers; i++) {
			playerList.Add(new Player(i, false));
		}
		playerList [Random.Range (0, numPlayers)].it = true;

		foreach (GameObject o in GameObject.FindGameObjectsWithTag("RoomParent")) {
			roomList.Add (o);
		}

		for (int i = 0; i < playerList.Count; i++) {
			GameObject o = Instantiate (Resources.Load ("FPSController", typeof(GameObject))) as GameObject;
			o.GetComponent<Player> ().id = playerList [i].id;
			o.GetComponent<Player> ().it = playerList [i].it;

			o.GetComponent<FirstPersonController> ().playerID = ("P" + (i+1));
			o.GetComponentInChildren<Shoot>().playerID = ("P" + (i+1));

			setPlayerCamera (i + 1, o);

			int pick = Random.Range (0, roomList.Count);
			while (usedSpawns.Contains(pick)){
				pick = Random.Range (0, roomList.Count);
			}

			o.transform.position = roomList [pick].transform.position;
			usedSpawns.Add (pick);
			playersInScene.Add (o);
		}
	}

	void setPlayerCamera(int p, GameObject go) {
		float[] vals = new float[2];

		if (p == 1) {
			vals [0] = 0f;
			vals [1] = .5f;
		} else if (p == 2) {
			vals [0] = .5f;
			vals [1] = .5f;
		} else if (p == 3) {
			vals [0] = 0;
			vals [1] = 0;
		} else if (p == 4) {
			vals [0] = .5f;
			vals [1] = 0;
		}
		Camera c = go.GetComponentInChildren<Camera>();
		c.rect = new Rect (vals [0], vals [1], .5f, .5f);

	}


	
	// Update is called once per frame
	void Update () {
		int itCount = 0;
		foreach (GameObject o in playersInScene) {
			if (o.GetComponent<Player> ().it) {
				itCount++;
			}
		}
		if (rounds <= 0) {
			SceneManager.LoadScene ("EndGame");
		}
		if (itCount == numPlayers) {
			NewMatch ();
		}
	}


	void NewMatch (){
		List<int> usedSpawns = new List<int> ();
		GenerateMap.ins.StartMap ();
		for (int i = 0; i < numPlayers; i++) {
			playerList [i].it = false;
		}
		playerList [Random.Range (0, numPlayers)].it = true;

		foreach (GameObject o in GameObject.FindGameObjectsWithTag("RoomParent")) {
			roomList.Add (o);
		}

		for (int i = 0; i < playersInScene.Count; i++) {
			int pick = Random.Range (0, roomList.Count);
			while (usedSpawns.Contains(pick)){
				pick = Random.Range (0, roomList.Count);
			}
			playersInScene[i].transform.position = roomList [pick].transform.position;
			usedSpawns.Add (pick);
		}
		rounds--;

	}
}
