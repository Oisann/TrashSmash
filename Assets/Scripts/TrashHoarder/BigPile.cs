using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace TrashHoarder {
	public class BigPile : MonoBehaviour {
		[Range(0, 700)]
		public int minedAmount = 0;

		[Header("Linking")]
		public Image progress;
		public Canvas progressBar;
		public CapsuleCollider mineTrigger;

		private CapsuleCollider coll;
		private float startRadius = 8f;
		private Vector3 startPos;

		private CountdownTimer timer;

		void Start() {
			coll = GetComponentInChildren<CapsuleCollider>();	
			startRadius = coll.radius;
			startPos = transform.position;
		}
		
		void Update() {
			if(timer == null) {
				timer = GameObject.FindObjectOfType<CountdownTimer>();
			} else {
				if(timer.currentTime <= 0) {
					return;
				}
			}
			if(progress != null) {
				int minePercent = Mathf.RoundToInt(minedAmount % 100f);
				progress.rectTransform.sizeDelta = new Vector2(minePercent / 10f, 2);
			}

			int moveDown = Mathf.FloorToInt(minedAmount / 100f);
			transform.position = new Vector3(0f, -moveDown, 0f) + startPos;

			//update collider
			if(coll != null) {
				int yPos = Mathf.RoundToInt(transform.position.y);
				if(yPos < -4.1f)
					yPos--;

				coll.radius = (startRadius + yPos);
				coll.enabled = minedAmount != 700;
				if(mineTrigger != null) {
					mineTrigger.radius = coll.radius + 1;
					if(mineTrigger.enabled != coll.enabled)
						mineTrigger.GetComponent<MinePile>().playersInTrigger.Clear();
					mineTrigger.enabled = coll.enabled;
				}
			}

			if(progressBar != null) {
				progressBar.gameObject.SetActive(minedAmount != 700);
			}
		}

		public void Mine(PlayerController pc) {
			if(timer != null)
				if(timer.currentTime <= 0)
					return;

			if(mineTrigger.GetComponent<MinePile>().playersInTrigger.Contains(pc)) {
				minedAmount = Mathf.Clamp(minedAmount + 5, 0, 700);
				pc.Mined();
			}
		}
	}
}
