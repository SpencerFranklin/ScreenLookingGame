using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMapCreator : MonoBehaviour {

	public GameObject unitObj;
	public GameObject unitDoorWall;
	public int mapWidth;
	public int mapDepth;

	private GameObject map;


	// Use this for initialization
	void Start () {
		map = new GameObject("Map");
		Vector3 pos = Vector3.zero;
		for (int i = 0; i < mapWidth; i++) {
			pos.x = i * 10; //unitObj.transform.localScale.x;
			for (int j = 0; j < mapDepth; j++) {
				pos.z = j * 10; //unitObj.transform.localScale.z;
				GameObject curRoom = (GameObject)Instantiate (unitObj, pos, Quaternion.identity);
				curRoom.transform.parent = map.transform;
				curRoom.transform.name = "" + i + ":" + j;

				checkOverlap (curRoom, i, j);
			}
		}
	}


	void checkOverlap (GameObject cur, int curI, int curJ) {
		string prev = null;
		GameObject findObject = null;
		Quaternion rot = Quaternion.identity;
		prev = "" + curI + ":" + (curJ - 1);

		for (int i = 0; i < 2; i++) {
			if (i==1) {
				prev = "" + (curI - 1) + ":" + curJ;
				rot *= Quaternion.Euler (0, 90f, 0);
			}
			if (map.transform.Find (prev)) {
				findObject = map.transform.Find (prev).gameObject;

				foreach (Transform child1 in findObject.transform) {
					foreach (Transform child2 in cur.transform) {
						if (child1.position == child2.position) {
							//tag inside walls for reference
							//child1.gameObject.tag = "InsideWall";
							//child2.gameObject.tag = "InsideWall";
							int c = Random.Range (0, 100);
							if (c < 70) {
								int d = Random.Range (0, 100);
								if (d < 15) {
									Vector3 pos = child1.position;
									pos.y -= 5;
									GameObject doorWall = (GameObject)Instantiate (unitDoorWall, pos, rot);
									doorWall.transform.parent = child1.parent;
								}

								//Get rid of walls even if no door created
								Destroy (child1.gameObject);
								Destroy (child2.gameObject);
							}
							break;
						}
					}
				}
			}
		}
	}
}
