using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashShooter : MonoBehaviour {

	public GameObject[] Trash;
	float trashRate;
	// Use this for initialization
	void Start () {


		trashRate = Random.Range (1.0f, 3.0f);
		Invoke ("SpawnTrash", trashRate);
	}
	
	// Update is called once per frame
	void SpawnTrash () {

		Instantiate (Trash[Random.Range(0,Trash.Length)], transform.position, transform.rotation);
		trashRate = Random.Range (1.0f, 3.0f);
		Invoke ("SpawnTrash", trashRate);

	}
}
