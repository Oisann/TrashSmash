using UnityEngine;
using System.Collections.Generic;

namespace TrashHoarder {
	public class DoorOpener : MonoBehaviour {
		[Range(-3.00f, 0.00f)]
		public float doorPosition = 0f;
		public BoxCollider dropper;

		private bool dontLower = true;

		/*void Start() {
			Invoke("StartLower", 5f);
		}*/

		void StartLower() {
			dontLower = false;
		}
		
		void FixedUpdate() {
			if(dontLower)
				return;
			if(dropper != null)
				dropper.enabled = true;
			doorPosition = Mathf.Clamp(doorPosition - Time.fixedDeltaTime, -3f, 0f);
			if(doorPosition <= -3f)
				dontLower = true;
		}

		void Update() {
			transform.position = new Vector3(transform.position.x, doorPosition, transform.position.z);
		}

		void OnTriggerEnter(Collider coll) {
			if(!IsInvoking("StartLower") && coll.CompareTag("Player"))
				Invoke("StartLower", 5f);
		}
	}
}