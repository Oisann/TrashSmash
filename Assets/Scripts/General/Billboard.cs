using UnityEngine;

public class Billboard : MonoBehaviour {

	private Camera cam;

	void Start() {
		cam = GameObject.FindObjectOfType<Camera>();	
	}
	
	void Update() {
		transform.LookAt(cam.transform);
	}
}
