using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatoPotato : MonoBehaviour {

	public float rotSpeed;
	public float arenaSizeX, arenaSizeZ;

	// Use this for initialization
	void Start () {
		arenaSizeX = 5;
		arenaSizeZ = 5;
	}
	
	// Update is called once per frame
	void Update () {

		arenaSizeX = Mathf.Clamp (arenaSizeX - (Time.deltaTime / 5), 1f, 1000f);
		arenaSizeZ = Mathf.Clamp (arenaSizeZ - (Time.deltaTime / 5), 1f, 1000f);

		transform.localScale = new Vector3 (arenaSizeX, 1, arenaSizeZ);

		transform.Rotate (new Vector3(0, Time.deltaTime * rotSpeed));
		
	}

	void OnTriggerExit(Collider other){
		if (other.GetComponent<Rigidbody>() != null)
		other.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.None;
	}

	void OnTriggerEnter(Collider other){
		if (other.GetComponent<Rigidbody>() != null)
			other.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezePositionY;
	}

	void OnTriggerStay(Collider other){
		if (other.GetComponent<Rigidbody> () != null && other.transform.position.y < 1)
			other.GetComponent<Rigidbody> ().AddForce (Vector3.down * 10);
	}

}
