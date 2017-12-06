using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour {
	public enum Direction {North, South, East, West	};
	public static List<GameObject> furniture = new List<GameObject>();
	public static List<GameObject> art = new List<GameObject>();

	Global ins;
	// Use this for initialization
	void Start () {

	}

	void Awake(){
		
		furniture.Add ((GameObject)Resources.Load ("Table1", typeof(GameObject)));
		furniture.Add ((GameObject)Resources.Load ("Table2", typeof(GameObject)));
		furniture.Add ((GameObject)Resources.Load ("Armchair", typeof(GameObject)));
		furniture.Add ((GameObject)Resources.Load ("Rocking Chair", typeof(GameObject)));

		art.Add ((GameObject)Resources.Load ("Painting1", typeof(GameObject)));
		art.Add ((GameObject)Resources.Load ("Painting2", typeof(GameObject)));
		art.Add ((GameObject)Resources.Load ("Painting3", typeof(GameObject)));
		art.Add ((GameObject)Resources.Load ("Painting4", typeof(GameObject)));
		art.Add ((GameObject)Resources.Load ("Painting5", typeof(GameObject)));
		art.Add ((GameObject)Resources.Load ("Painting6", typeof(GameObject)));
		art.Add ((GameObject)Resources.Load ("Painting7", typeof(GameObject)));



	


		ins = this;
	}
	// Update is called once per frame
	void Update () {
		
	}
}
