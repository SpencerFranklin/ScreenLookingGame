using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
	public bool isDoor = false;
	public bool isColliding;
	public bool isConnecter = false;

	// Use this for initialization
	void Start ()
	{
		this.addArt ();
		//Debug.Log (this.gameObject.transform.localPosition + "  " + this.gameObject.name);
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public Global.Direction getDirection()
	{
		if (this.gameObject.transform.forward == Vector3.forward) {
			return Global.Direction.North;
		} else if (this.gameObject.transform.forward ==  -Vector3.forward) {
			return Global.Direction.South;
		} else if (this.gameObject.transform.forward == Vector3.right) {
			return Global.Direction.East;
		} else {
			return Global.Direction.West;
		}
	}

	public bool isOpposite(Global.Direction dir){
		if ((this.getDirection () == Global.Direction.South && dir == Global.Direction.North) || (this.getDirection () == Global.Direction.North && dir == Global.Direction.South)) {
			return true;
		} else if ((this.getDirection () == Global.Direction.West && dir == Global.Direction.East) || (this.getDirection () == Global.Direction.East && dir == Global.Direction.West)) {
			return true;
		} else {
			return false;
		}

	}

	void OnTriggerEnter(){
		Debug.Log ("--------------------------------");
		isColliding = true;
	}

	void OnTriggerExit(){

		isColliding = false;
	}

	public void addArt(){
		if (Random.value > .75f && this.tag != "Door") {
			GameObject o = Instantiate (Global.art[Random.Range(0, Global.art.Count)], this.transform) as GameObject;
			o.transform.rotation = o.transform.rotation;

			if ((int) this.getDirection() == 0){
				o.transform.position = new Vector3 (o.transform.position.x, o.transform.position.y, o.transform.position.z - .01f);
				o.transform.Rotate (0, 0, 180);
			}
			if ((int) this.getDirection() == 1){
				o.transform.position = new Vector3 (o.transform.position.x, o.transform.position.y, o.transform.position.z + .01f);
				o.transform.Rotate (0, 0, 180);

			}	
			if ((int) this.getDirection() == 2){
				o.transform.position = new Vector3 (o.transform.position.x-.01f, o.transform.position.y, o.transform.position.z);
				o.transform.Rotate (0, 0, 180);

			}	
			if ((int) this.getDirection() == 3){
				o.transform.position = new Vector3 (o.transform.position.x+.01f, o.transform.position.y, o.transform.position.z);
				o.transform.Rotate (0, 0, 180);

			}	
		}
	}




}
