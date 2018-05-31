using UnityEngine;
using System.Collections.Generic;

namespace TrashHoarder {
	public class CameraWinner : MonoBehaviour {
		public DropItems red, green, blue, yellow;
		public Transform redPlayer, greenPlayer, bluePlayer, yellowPlayer;

		public CountdownTimer timer;

		private Camera cam;
		private bool addedScore = false;

		void Start() {
			cam = GetComponentInChildren<Camera>();
		}

		bool isBiggest(int cur) {
			return cur >= (red.size) &&
					cur >= (yellow.size) &&
					cur >= (green.size) &&
					cur >= (blue.size);
		}

		void score(string s) {
			if(addedScore)
				return;
			int i = PlayerPrefs.GetInt(s + "Score", 0);
			i++;
			PlayerPrefs.SetInt(s + "Score", i);
			addedScore = true;
		}

		void Update() {
			if(timer != null) {
				if(timer.currentTime > 0f) {
					return;
				}
			} else {
				timer = GameObject.FindObjectOfType<CountdownTimer>();
				return;
			}

			if(isBiggest(red.size)) {
				transform.position = redPlayer.position + (redPlayer.up * 10f);
				score("red");
			} else if(isBiggest(yellow.size)) {
				transform.position = yellowPlayer.position + (yellowPlayer.up * 10f);
				score("yellow");
			} else if(isBiggest(blue.size)) {
				transform.position = bluePlayer.position + (bluePlayer.up * 10f);
				score("blue");
			} else if(isBiggest(green.size)) {
				transform.position = greenPlayer.position + (greenPlayer.up * 10f);
				score("green");
			}

			if(Vector3.Distance(transform.position, cam.transform.position) >= 20f)
				cam.transform.Translate(Vector3.forward * (Time.deltaTime * 10f), Space.Self);
		}

	}
}
