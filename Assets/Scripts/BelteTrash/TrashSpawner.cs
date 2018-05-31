using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashSpawner : MonoBehaviour {

	public float tSpawnRate;
	public GameObject[] trash;
	float spawnRate;
	float trashSpeed;
	Vector3 spawnPlace;


	// Use this for initialization
	void Start () {
		spawnRate = 2;
		Invoke ("SpawnTrash", spawnRate);
	}
	
	// Update is called once per frame
	void Update () {
		spawnPlace = new Vector3 (Random.Range (-52f, -44f), transform.position.y, transform.position.z);
		spawnRate -= Time.deltaTime / tSpawnRate;
	}

	void SpawnTrash(){
		Instantiate (trash[Random.Range(0,trash.Length)], spawnPlace, transform.rotation);
		Invoke ("SpawnTrash", spawnRate);
	}

}
