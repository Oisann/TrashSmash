using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlace : MonoBehaviour {



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = transform.parent.position + new Vector3(0f, 0.84f, 0f);
		transform.localPosition = transform.parent.InverseTransformPoint(pos);
		transform.rotation = Quaternion.Euler(0f, 0f, 0f);
	}
}
