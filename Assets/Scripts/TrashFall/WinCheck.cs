using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCheck : MonoBehaviour {
	private CountdownTimer timer;
	private bool check = false;
	private bool addedScore = false;

	void Start () {
		
	}

	void score(string s) {
		if(addedScore)
			return;
		int i = PlayerPrefs.GetInt(s + "Score", 0);
		i++;
		PlayerPrefs.SetInt(s + "Score", i);
		addedScore = true;
	}

	void Update () {
		if (timer == null) {
			timer = GameObject.FindObjectOfType<CountdownTimer> ();
		} else {
			if(timer.currentTime < 0f && !check) {
				TrashFallController[] all = GameObject.FindObjectsOfType<TrashFallController> ();
				int winner = 0;
				string wName = "red";
				foreach(TrashFallController tfc in all) {
					if (tfc.numScoreP1 > winner) {
						winner = tfc.numScoreP1;
						wName = tfc.playerName;
					}
				}
				score(wName.ToLower());
				check = true;
			}
		}
	}
}
