using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour {
	public float startTime = 60f;
	public int biggestSize = 75;
	public int smallestSize = 45;

	[HideInInspector]
	public float currentTime = 0f;
	
	private Text timer;
	private bool growing = false;
	private float fontSize = 60f;
	void Start() {
		currentTime = startTime;
		timer = GetComponent<Text>();
		fontSize = timer.fontSize;
	}
	
	void Update() {
		currentTime -= Time.deltaTime;

		if(currentTime <= 0f) {
			timer.text = "Time's up!";
			return;
		}

		if(currentTime < 59.9f) {
			float l = Mathf.Clamp(currentTime, 0f, 1000000f);
			timer.text = Mathf.CeilToInt(l) + "";
		} else {
			timer.text = Mathf.CeilToInt(currentTime) + "";
		}
		
		/*
		fontSize = growing ? fontSize + Time.deltaTime : fontSize - Time.deltaTime;

		if(fontSize >= biggestSize)
			growing = false;

		if(fontSize <= smallestSize)
			growing = true;

		timer.fontSize = Mathf.RoundToInt(fontSize);
		*/
	}
}
