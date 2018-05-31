using UnityEngine;
using System.Collections.Generic;

namespace TrashHoarder {
	public class MinePile : MonoBehaviour {
		public List<PlayerController> playersInTrigger = new List<PlayerController>();
		
		void OnTriggerEnter(Collider coll) {
			PlayerController pc = coll.GetComponentInParent<PlayerController>();
			if(pc != null) {
				if(!playersInTrigger.Contains(pc))
					playersInTrigger.Add(pc);
			}
		}

		void OnTriggerExit(Collider coll) {
			PlayerController pc = coll.GetComponentInParent<PlayerController>();
			if(pc != null) {
				if(playersInTrigger.Contains(pc))
					playersInTrigger.Remove(pc);
			}
		}
	}
}
