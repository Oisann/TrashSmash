using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionExplode : MonoBehaviour {


	public float ForceAwakens; 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter(Collision col){
		if (col.gameObject.GetComponent<Rigidbody>() != null)
		col.gameObject.GetComponent<Rigidbody> ().AddExplosionForce (ForceAwakens, transform.position, 100);
	}

}
