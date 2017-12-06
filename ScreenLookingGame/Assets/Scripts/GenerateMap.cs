using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMap : MonoBehaviour
{
	public static GenerateMap ins;
	private List<Room> currentMap = new List<Room> ();
	public List<GameObject> allRoomGameObj = new List<GameObject> ();
	private List<Room> allRooms = new List<Room> ();
	GameObject parentObj;
	public int totalRoomLimit = 2;
	public int minOfMinRange = 1;
	public int maxOfMinRange = 4;
	public int minOfMaxRange = 9;
	public int maxOfMaxRange = 15;
	List<Vector2> mapGrid = new List<Vector2>();
	List<GameObject> currentMapObj = new List<GameObject> ();

	public List<Material> matList = new List<Material> ();
	private int roomPointer = 0;
	public float wallSpawnFrequency;
	// Use this for initialization
	void Awake(){
		ins = this;
	}
	public void StartMap ()
	{
		if (allRoomGameObj.Count > 0) {
			foreach (GameObject o in allRoomGameObj) {
				Room temp = new Room (o, o.name);
				allRooms.Add (temp);
			}
		}
		//create empty object to be parent of the map
		//parentObj = GameObject.Instantiate (new GameObject ());
		//parentObj.name = "Map";
		//parentObj.transform.position = new Vector3 (0, 0, 0);

		//wallSpawnFrequency = .25f;

		//GameObject obj = Instantiate (Resources.Load ("Square", typeof(GameObject))) as GameObject;
		//obj.gameObject.transform.RotateAround (obj.gameObject.transform.position, Vector3.up, Random.Range(0, 3) * 90);
		//Debug.Log (obj.name);
		//Room current = new Room (obj, obj.name);
		//currentMap.Add (current);

		//Generate ();
		MakeMap();
		placeDoors ();
	}

	public void MakeMap(){
		//int i = 0;
		bool mustMove = false;
		while (totalRoomLimit > 0) {
			//i++;
			GameObject g = MakeRoom ();
			//g.transform.position = new Vector3 (g.transform.position.x + i * 5, g.transform.position.y, g.transform.position.z);
			currentMap.Add (g.GetComponent<Room>());
			currentMapObj.Add (g);
			totalRoomLimit--;
		}
		foreach (Room r in currentMap) {
			if (mapGrid.Count > 0) {
				foreach (Vector2 v in r.gridList) {
					if (mapGrid.Contains (v)) {
						mustMove = true;
						break;
					}
				}
			}

			if (!mustMove) {
				mapGrid.AddRange (r.gridList);
				string str = "";
				foreach (Vector2 v in r.gridList){
					str = str + " " + v.ToString ();
				}
//				Debug.Log ("Added " + str);

			} else {
				Vector2 mapConnectPt = mapGrid [0];
				Vector2 newRoomConnectPt = r.gridList [0];
				int rand = Random.Range (0, 4);
				float xBuf = 0;
				float yBuf = 0;

				if (rand == 0) {
					yBuf = -1f;
					foreach (Vector2 v in r.gridList) {
						if (v.y > newRoomConnectPt.y)
							newRoomConnectPt = v;
					}
					foreach (Vector2 v in mapGrid) {
						if (v.y < mapConnectPt.y)
							mapConnectPt = v;
					}
				} else if (rand == 1) {
					yBuf = 1f;
					foreach (Vector2 v in r.gridList) {
						if (v.y < newRoomConnectPt.y)
							newRoomConnectPt = v;
					}
					foreach (Vector2 v in mapGrid) {
						if (v.y > mapConnectPt.y)
							mapConnectPt = v;
					}

				} else if (rand == 2) {
					xBuf = -1f;
					foreach (Vector2 v in r.gridList) {
						if (v.x > newRoomConnectPt.x)
							newRoomConnectPt = v;
					}
					foreach (Vector2 v in mapGrid) {
						if (v.x < mapConnectPt.x)
							mapConnectPt = v;
					}
				} else {
					xBuf = 1f;
					foreach (Vector2 v in r.gridList) {
						if (v.x < newRoomConnectPt.x)
							newRoomConnectPt = v;
					}
					foreach (Vector2 v in mapGrid) {
						if (v.x > mapConnectPt.x)
							mapConnectPt = v;
					}
				}


				Vector2 distance = mapConnectPt - newRoomConnectPt;
				int ctr = 0;

				for (int i = 0; i < r.gridList.Count; i++){
					r.gridList [i] = r.gridList [i] + new Vector2 (distance.x +xBuf , distance.y + yBuf );
					//Debug.Log ("updating gridlist");
					mapGrid.Add (r.gridList [i]);
				}
				r.room.transform.position = r.room.transform.position + (new Vector3 (distance.x +xBuf , 0, distance.y + yBuf ));
				/*
				foreach (Vector2 v in r.gridList) {
					if (v == newRoomConnectPt) {
						if (rand == 0) {
							r.blocksInRoom [ctr].swapNorthWall ();
						} else if (rand == 1) {
							r.blocksInRoom [ctr].swapSouthWall ();
						} else if (rand == 2) {
							r.blocksInRoom [ctr].swapEastWall ();
						} else {
							r.blocksInRoom [ctr].swapWestWall ();
						}
					}
					ctr++;
				}
				foreach (Vector2 v in mapGrid) {
					if (v == mapConnectPt) {
						if (rand == 0) {
							SwapWallAt (Global.Direction.South, mapConnectPt, new Vector2 (distance.x +xBuf , distance.y + yBuf ));
						} else if (rand == 1) {
							SwapWallAt (Global.Direction.North, mapConnectPt, new Vector2 (distance.x +xBuf , distance.y + yBuf ));
						} else if (rand == 2) {
							SwapWallAt (Global.Direction.West, mapConnectPt, new Vector2 (distance.x +xBuf , distance.y + yBuf ));
						} else {
							SwapWallAt (Global.Direction.East, mapConnectPt, new Vector2 (distance.x +xBuf , distance.y + yBuf ));
						}
						break;
					}
				}
				*/

				PaintRoom (r);


				//mapGrid.AddRange (r.gridList);
				string str = "";
				foreach (Vector2 v in r.gridList){
					str = str + " " + v.ToString ();
				}
//				Debug.Log ("Added " + str);
			}
		}
	}

	void placeDoors() {

		GameObject[] list = GameObject.FindGameObjectsWithTag ("Room");
		List<GameObject> cList = new List<GameObject>();
		foreach (GameObject go in list) {
			foreach (Transform child in go.transform) {
				child.name = child.position.ToString();
				cList.Add(child.gameObject);
			}
			//go.transform.DetachChildren ();
			//Destroy (go);
		}
		foreach (GameObject go in cList) {
			Transform p1 = go.transform.parent;
			go.transform.parent = p1.parent;
			Destroy (p1.gameObject);
		}


		GameObject[] pList = GameObject.FindGameObjectsWithTag ("RoomParent");
		List<GameObject> dList = new List<GameObject>();
		for (int x = 0; x < pList.Length; x++) {
			for (int i = x + 1; i < pList.Length; i++) {
				foreach (Transform child in pList[x].transform) {
					foreach (Transform child2 in pList[i].transform) {
						if (child.name == child2.name) {
							child.name = "Door";
							child2.name = "Door";
						}
					}
				}
			}
		}

		Debug.Log (dList.Count);
		/*foreach(GameObject go in dList) {
			GameObject obj = Instantiate (Resources.Load ("Door", typeof(GameObject)), go.transform.parent) as GameObject;
			obj.transform.position = go.transform.position;
			obj.transform.rotation = go.transform.rotation;
			//Debug.Log (go);
			Destroy (go);
		}*/
	}

	void SwapWallAt( Global.Direction dir, Vector2 pt, Vector2 distance){
		GameObject[] list =  GameObject.FindObjectsOfType<GameObject>();



		foreach (GameObject o in currentMapObj) {
			if (o.transform.position.x == pt.x && o.transform.position.z == pt.y) {
				//o.AddComponent<AudioListener> ();
				//Room pick = o.GetComponent<Room> ().blocksInRoom [o.GetComponent<Room> ().gridList.IndexOf (pt)];
				string s = "";
				foreach (Vector2 vec in o.GetComponent<Room>().gridList) {
					s = s + " " + vec;
				}
				Debug.Log ("Pt :" + pt + " List:  " + s);
					
			}
		}
		int q = 0;
		foreach (GameObject o in list) {
			

			string s = "";
			if (o.tag == "Room" && o.transform.parent.gameObject.GetComponent<Room>().gridList.Contains(pt)) {
				
				foreach (Vector2 vec in o.transform.parent.gameObject.GetComponent<Room>().gridList) {
					s = s + " " + vec;
				}
				Debug.Log("The point: " + pt + " room #: " + q + " room gridlist: " + s);
				for (int i = 0; i < o.transform.parent.gameObject.GetComponent<Room>().gridList.Count; i++){
					//o.GetComponent<Room>().gridList [i] = o.GetComponent<Room>().gridList [i] + distance;
					//Debug.Log(
				}

				Transform pick = o.transform.parent.GetChild (o.transform.parent.gameObject.GetComponent<Room> ().gridList.IndexOf (pt));
				Debug.Log (o.transform.parent.gameObject.GetComponent<Room> ().gridList.IndexOf (pt));
				foreach (Transform wall in pick) {
						if ((int)wall.gameObject.GetComponent<Wall> ().getDirection () == (int)dir && !wall.gameObject.name.Equals ("Door(Clone)")) {
							GameObject obj = Instantiate (Resources.Load ("Door", typeof(GameObject)), o.transform) as GameObject;
							obj.transform.position = wall.position;
							obj.transform.rotation = wall.rotation;
							Debug.Log (wall.gameObject);
							Destroy (wall.gameObject);	
						}
					

				}
				/*foreach (Transform block in o.transform) {
					Debug.Log (o.GetComponent<Room>().gridList[i]);
					if (o.GetComponent<Room> ().gridList [i] == pt && block.position.x == pt.x && block.position.z == pt.y) {
						foreach (Transform wall in block) {
							if ((int)wall.gameObject.GetComponent<Wall> ().getDirection () == (int)dir && !wall.gameObject.name.Equals ("Door(Clone)")) {
								GameObject obj = Instantiate (Resources.Load ("Door", typeof(GameObject)), block.transform) as GameObject;
								obj.transform.position = wall.position;
								obj.transform.rotation = wall.rotation;
								Destroy (wall.gameObject);	
							}

						}

					}

					i++;

				}*/
				/*string s = "";
				foreach (Vector2 vec in o.gameObject.GetComponent<Room>().gridList) {
					s = s + " " + vec;
				}
				Debug.Log ("Pt :" + pt + " List:  " + s);*/
				break;

			}
			q++;
		}

	}


	public GameObject MakeRoom(){
		Room r = new Room (); //make new room
		int gX = 0;
		int gY = 0;
		Vector2 gridPos = new Vector2(gX, gY);
		int numRooms = Random.Range (Random.Range (minOfMinRange, maxOfMinRange), Random.Range (minOfMaxRange, maxOfMaxRange));
		r.gridList.Add (gridPos);
		int gridCounter = 0;
		numRooms--;

		while (numRooms > 0) {
			gX = (int) r.gridList [gridCounter].x;
			gY = (int) r.gridList [gridCounter].y;

			if (!r.gridList.Contains(new Vector2(gX, gY + 1)) && numRooms > 0){ // if the grid space above is empty
				if (Random.value < .7f) {
					r.gridList.Add (new Vector2 (gX, gY + 1));
					numRooms--;
				}

			}
			if (!r.gridList.Contains(new Vector2(gX, gY - 1)) && numRooms > 0){ // if the grid space below is empty
				if (Random.value < .7f) {
					r.gridList.Add (new Vector2 (gX, gY - 1));
					numRooms--;
				}

			}
			if (!r.gridList.Contains(new Vector2(gX + 1, gY)) && numRooms > 0){ // if the grid space right is empty
				if (Random.value < .7f) {
					r.gridList.Add (new Vector2 (gX + 1, gY));
					numRooms--;
				}

			}
			if (!r.gridList.Contains(new Vector2(gX - 1, gY)) && numRooms > 0){ // if the grid space left is empty
				if (Random.value < .7f) {
					r.gridList.Add (new Vector2 (gX - 1, gY));
					numRooms--;
					//Debug.Log (new Vector2 (gX - 1, gY));
				}

			}

			if (gridCounter + 1 < r.gridList.Count)
				gridCounter++;
		}

		foreach (Vector2 v in r.gridList){
	//		Debug.Log (v);
		}

		return InsRoom (r);
	}

	//delete the inner walls of a room
	public void ClearInnerWalls(Room r){
		int i = 0;
		foreach (Vector2 v in r.gridList) {
			if (r.gridList.Contains (new Vector2 (v.x, v.y - 1))) {
				Destroy (r.blocksInRoom [i].southWall());
			}
			if (r.gridList.Contains (new Vector2 (v.x, v.y + 1))) {
				Destroy (r.blocksInRoom [i].northWall());
			}
			if (r.gridList.Contains (new Vector2 (v.x + 1, v.y))) {
				Destroy (r.blocksInRoom [i].eastWall());
			}
			if (r.gridList.Contains (new Vector2 (v.x -1, v.y))) {
				Destroy (r.blocksInRoom [i].westWall());
			}
			i++;
		}


	}

	//instantiate the room 
	public GameObject InsRoom(Room r){
		GameObject p = new GameObject();
		p.name = "Room";
		p.tag = "RoomParent";
		foreach (Vector2 v in r.gridList) {
			GameObject obj = Instantiate (Resources.Load ("Square", typeof(GameObject)), p.transform) as GameObject;
			obj.transform.position = new Vector3(obj.transform.position.x + v.x, obj.transform.position.y, obj.transform.position.z + v.y);
			r.blocksInRoom.Add (new Room(obj, obj.name));
		}
		ClearInnerWalls (r);
		r.room = p;
		r.spawnObjectsInRoom ();
		r.addLights ();
		Room tmp = p.AddComponent<Room>() as Room;
		p.GetComponent<Room> ().blocksInRoom = r.blocksInRoom;
		p.GetComponent<Room> ().gridList = r.gridList;
		p.GetComponent<Room> ().room = p;

		return p;

	}

	void PaintRoom(Room r){
		Material m = matList [Random.Range (0, matList.Count)];
		foreach (Transform block in r.room.transform) {
			foreach (Transform wall in block) {
				if (wall.tag != "Floor") {
					Renderer rend = wall.GetComponent<Renderer> ();
					rend.material = m;
				}
			}
		}
	}




	void Generate ()
	{
		while (totalRoomLimit > 0) {
			Room current = currentMap [roomPointer];
			//number of doors to add to the current room
			//Debug.Log (current.getWallsAsList ().ToString ());

			int doorCount = (int)Random.Range (1, current.getChildren ().Count);

			for (int i = 0; i < doorCount; i++) { 
				//for each door to be added, pick a random wall
				int j = Random.Range (0, current.getChildren ().Count);
				//while the selected wall has a door
				while (current.getChildren () [j].gameObject.GetComponent<Wall> ().isDoor == true) {
					//pick a new wall
					j = Random.Range (0, current.getChildren ().Count);
				}
				//set the selected wall with no door to have a wall	
				current.getChildren () [j].gameObject.GetComponent<Wall> ().isDoor = true;

			
			}


			foreach (Wall w in current.getWallsAsList()) { // for each wall in the current room
				if (w.isDoor) { // if the current wall has a door
					GameObject newObj = Instantiate (Resources.Load ("Square" , typeof(GameObject))) as GameObject;
					newObj.gameObject.transform.RotateAround (newObj.gameObject.transform.position, Vector3.up, Random.Range(0, 3) * 90);
					Room newRoom = new Room (newObj, newObj.name);
					int i = 1;
					foreach (Wall tmpWall in newRoom.getWallsAsList()) {
						if (w.isOpposite (tmpWall.getDirection ())) {
							newRoom.room.transform.position = w.transform.position;
							newRoom.room.transform.position += -(tmpWall.transform.position - w.transform.position);
							currentMap.Add (newRoom);
							Debug.Log ("I am room " + newRoom.name + " is colliding: " + newRoom.isColliding());
							break;
						}
						if (i == newRoom.getWallsAsList ().Count) {
							Destroy (newObj);
						}
						i++;

					}

					//line up the lineupWall object to the wall w

				}
				//Debug.Log (w.name + "  -    " + w.getDirection ());
			}
			if (roomPointer + 1 <= currentMap.Count) {
				roomPointer++;
			}
			totalRoomLimit--;

		}
	}
	// Update is called once per frame
	void Update ()
	{
		GameObject[] pList = GameObject.FindGameObjectsWithTag ("RoomParent");
		foreach (GameObject g in pList) {
			foreach (Transform child in g.transform) {
				if (child.name == "Door") {
					GameObject obj = Instantiate (Resources.Load ("Door", typeof(GameObject)), g.transform) as GameObject;
					obj.transform.position = child.position;
					obj.transform.rotation = child.rotation;
					//Debug.Log (wall.gameObject);
					Destroy (child.gameObject);
				}
			}
		}
	}




}
