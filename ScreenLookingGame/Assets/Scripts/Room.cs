using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
	public GameObject room;
	public string name;
	//public bool isColliding;
	public List<Vector2> gridList = new List<Vector2>();
	public List<Room> blocksInRoom = new List<Room>();
	// Use this for initialization
	void Start ()
	{

	}
	public Room (){

	}

	public Room (GameObject room_, string name_)
	{
		room = room_;
		name = name_;
	}
	// Update is called once per frame
	void Update ()
	{
		
	}

	//returns a list of all walls in the room
	public List<Transform> getChildren ()
	{
		List<Transform> children = new List<Transform> ();

		//for each wall on the current room object, add it to a list of walls
		foreach (Transform child in room.transform) {
			if (!child.name.Equals ("Cube") && !child.name.Equals ("Cube(Clone)")) {
				children.Add (child);
			}
		}
		return children;

	}

	public GameObject southWall(){
		foreach (Transform child in room.transform) {
			if ((int) child.gameObject.GetComponent<Wall> ().getDirection() == (int)(Global.Direction.South)) {
				return child.gameObject;
			}
		}
		return null;
	}

	public GameObject northWall(){
		foreach (Transform child in room.transform) {
			if ((int) child.gameObject.GetComponent<Wall> ().getDirection() == (int)(Global.Direction.North)) {
				return child.gameObject;
			}
		}
		return null;
	}

	public GameObject eastWall(){
		foreach (Transform child in room.transform) {
			if ((int) child.gameObject.GetComponent<Wall> ().getDirection() == (int)(Global.Direction.East)) {
				return child.gameObject;
			}
		}
		return null;
	}

	public GameObject westWall(){
		foreach (Transform child in room.transform) {
			if ((int) child.gameObject.GetComponent<Wall> ().getDirection() == (int)(Global.Direction.West)) {
				return child.gameObject;
			}
		}
		return null;
	}

	public List<Wall> getWallsAsList ()
	{
		List<Wall> walls = new List<Wall> ();
		foreach (Transform child in room.transform) {
			if (!child.name.Equals ("Cube") && !child.name.Equals ("Cube(Clone)")) {
				walls.Add (child.gameObject.GetComponent<Wall> ());

			}
		}
		return walls;

	}



	public bool isColliding(){
		List<Wall> walls = new List<Wall> ();
		foreach (Transform child in room.transform) {
			if (!child.name.Equals ("Cube") && !child.name.Equals ("Cube(Clone)")) {
				if (child.gameObject.GetComponent<Wall> ().isColliding) {
					return true;
				}
			}
		}
		return false;
	}


}
