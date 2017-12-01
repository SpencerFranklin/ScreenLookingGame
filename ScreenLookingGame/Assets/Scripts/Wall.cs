using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
	public bool isDoor = false;
	public enum Direction {North, South, East, West	};
	public bool isColliding;

	// Use this for initialization
	void Start ()
	{
		Debug.Log (this.gameObject.transform.localPosition + "  " + this.gameObject.name);
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public Direction getDirection()
	{
		if (this.gameObject.transform.forward == Vector3.forward) {
			return Direction.North;
		} else if (this.gameObject.transform.forward ==  -Vector3.forward) {
			return Direction.South;
		} else if (this.gameObject.transform.forward == Vector3.right) {
			return Direction.East;
		} else {
			return Direction.West;
		}
	}

	public bool isOpposite(Direction dir){
		if ((this.getDirection () == Direction.South && dir == Direction.North) || (this.getDirection () == Direction.North && dir == Direction.South)) {
			return true;
		} else if ((this.getDirection () == Direction.West && dir == Direction.East) || (this.getDirection () == Direction.East && dir == Direction.West)) {
			return true;
		} else {
			return false;
		}

	}

	void OnTriggerEnter(){

		isColliding = true;
	}

	void OnTriggerExit(){
		isColliding = false;
	}


}
