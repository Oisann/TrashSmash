using UnityEngine;
using System.Collections.Generic;

namespace TrashHoarder {
	public class MineSmallPile : MonoBehaviour {
		public DropItems di;
		public List<PlayerController> playersInTrigger = new List<PlayerController>();
		
		void Start() {
			//di = transform.parent.GetComponentInParent<DropItems>();
		}

		public void Mine(PlayerController pc) {
			if(playersInTrigger.Contains(pc) && di.size >= 1) {
				di.size--;
				pc.SpawnGarbage();
			}
		}

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
