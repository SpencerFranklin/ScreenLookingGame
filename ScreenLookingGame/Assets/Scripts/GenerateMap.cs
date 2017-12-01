using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMap : MonoBehaviour
{
	private List<Room> currentMap = new List<Room> ();
	public List<GameObject> allRoomGameObj = new List<GameObject> ();
	private List<Room> allRooms = new List<Room> ();
	GameObject parentObj;
	private int totalRoomLimit = 5;
	private int roomPointer = 0;
	public float wallSpawnFrequency;
	// Use this for initialization
	void Start ()
	{
		if (allRoomGameObj.Count > 0) {
			foreach (GameObject o in allRoomGameObj) {
				allRooms.Add (new Room (o, o.name));
			}
		}
		//create empty object to be parent of the map
		parentObj = GameObject.Instantiate (new GameObject ());
		parentObj.name = "Map";
		parentObj.transform.position = new Vector3 (0, 0, 0);

		wallSpawnFrequency = .25f;

		GameObject obj = Instantiate (Resources.Load ("Square", typeof(GameObject))) as GameObject;
		obj.gameObject.transform.RotateAround (obj.gameObject.transform.position, Vector3.up, Random.Range(0, 3) * 90);
		//Debug.Log (obj.name);
		Room current = new Room (obj, obj.name);
		currentMap.Add (current);

		Generate ();
			
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
					GameObject newObj = Instantiate (Resources.Load (allRooms [Random.Range (0, allRooms.Count)].name.Replace("(Clone)", "") , typeof(GameObject))) as GameObject;
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
				Debug.Log (w.name + "  -    " + w.getDirection ());
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
		
	}


}
