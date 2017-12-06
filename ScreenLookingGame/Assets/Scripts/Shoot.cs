using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {

	public string playerID;

	public GameObject bullet;
	public float speed;
	GameObject barrel;

	// Use this for initialization
	void Start () {
		barrel = transform.Find ("Gun").Find("Barrel").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown(playerID + "Fire1")) {
			GameObject instantiatedProjectile = Instantiate(bullet, barrel.transform.position, barrel.transform.rotation);

			string l = LayerMask.LayerToName (barrel.layer);
			instantiatedProjectile.gameObject.layer = LayerMask.NameToLayer (l);

			instantiatedProjectile.GetComponent<Rigidbody>().velocity = barrel.transform.TransformDirection(Vector3.forward*speed);
		}
	}
}
