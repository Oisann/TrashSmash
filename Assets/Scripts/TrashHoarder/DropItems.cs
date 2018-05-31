using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace TrashHoarder {
	public class DropItems : MonoBehaviour {
		public Transform pile;
		public Text counter;
		public int size = 0;

		private CountdownTimer timer;

		void Update() {
			if(timer == null) {
				timer = GameObject.FindObjectOfType<CountdownTimer>();
			} else {
				if(timer.currentTime <= 0) {
					return;
				}
			}
			
			if(pile == null)
				return;
			pile.localScale = new Vector3(1f, 1f, 1f) * Mathf.Clamp01((0.1f * size));
			if(counter != null) {
				if(size <= 0)
					counter.text = "";
				else
					counter.text = size + "";
			}
		}

		void OnTriggerEnter(Collider coll) {
			if(timer != null)
				if(timer.currentTime <= 0)
					return;
				
			if(coll.CompareTag("TrashHolding") || coll.CompareTag("Trash")) {
				size++;
				Destroy(coll.gameObject);
			}
		}
	}
}
