using UnityEngine;

[ExecuteInEditMode]
public class CameraRotate : MonoBehaviour {
	public Vector3 rotationVector = Vector3.zero;
	[Range(0.00f, 1.00f)]
	public float speed = .1f;

	private Camera cam;
	void Start() {
		cam = GetComponentInChildren<Camera>();
	}

	void FixedUpdate() {
		transform.eulerAngles += (rotationVector * speed);
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.red;
		Gizmos.DrawSphere(transform.position, 1f);
		if(cam != null) {
			Gizmos.DrawLine(transform.position, cam.transform.position);
			Gizmos.DrawSphere(cam.transform.position, 1f);
		}
	}
}
