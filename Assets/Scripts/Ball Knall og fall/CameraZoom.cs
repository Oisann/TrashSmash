using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour {

	public float speed;
	public RotatoPotato rotPot;
	float stop;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		stop = rotPot.arenaSizeX;

		if (stop == 1)
			return;

		transform.Translate (Vector3.forward  * Time.deltaTime * speed, Space.Self);
		//transform.position.z = Mathf.Clamp (arenaSizeX - (Time.deltaTime / 5), 1f, 1000f);
	}
}
