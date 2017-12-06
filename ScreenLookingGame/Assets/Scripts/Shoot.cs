using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {

	public enum PlayerID{
		P1,P2,P3,P4
	}
	public PlayerID pID;
	private string playerID;

	public GameObject bullet;
	public float speed;
	GameObject barrel;

	// Use this for initialization
	void Start () {
		playerID = pID.ToString ();
		barrel = transform.Find ("Gun").Find("Barrel").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown(playerID + "Fire1")) {
			GameObject instantiatedProjectile = Instantiate(bullet, barrel.transform.position, barrel.transform.rotation);
			instantiatedProjectile.GetComponent<Rigidbody>().velocity = barrel.transform.TransformDirection(Vector3.forward*speed);
		}
	}
}
