using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
	public float time = 30f;
	public float continueAfter = 5f;
	public string winText = "";

	private CountdownTimer timer;

	void loadNext() {
		string scene = ReadySetGo.getNextScene();
		SceneManager.LoadScene(scene);
	}
	
	void Update () {
		if (timer == null) {
			timer = GameObject.FindObjectOfType<CountdownTimer> ();
		} else {
			if (timer.currentTime > time) {
				timer.currentTime = time;
			}

			if(timer.currentTime <= 0f && !IsInvoking("loadNext"))
				Invoke("loadNext", continueAfter);
		}
	}
}
