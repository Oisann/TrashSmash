using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoystickAssigner : MonoBehaviour {
	public Text red, green, blue, yellow;
	public string ledig = "LEDIG";
	public List<int> assigned = new List<int>();
	public List<bool> ready = new List<bool>();

	private List<bool> pressed = new List<bool>();
	private List<bool> pressedReady = new List<bool>();

	void Start() {
		red.text = ledig;
		green.text = ledig;
		blue.text = ledig;
		yellow.text = ledig;

		//Beautiful code incoming
		assigned.Add(0);
		assigned.Add(0);
		assigned.Add(0);
		assigned.Add(0);

		ready.Add(false);
		ready.Add(false);
		ready.Add(false);
		ready.Add(false);

		pressed.Add(false);
		pressed.Add(false);
		pressed.Add(false);
		pressed.Add(false);
		pressed.Add(false);
		pressed.Add(false);
		pressed.Add(false);
		pressed.Add(false);
		pressed.Add(false);
		pressed.Add(false);
		pressed.Add(false);
		pressed.Add(false);

		pressedReady.Add(false);
		pressedReady.Add(false);
		pressedReady.Add(false);
		pressedReady.Add(false);
		pressedReady.Add(false);
		pressedReady.Add(false);
		pressedReady.Add(false);
		pressedReady.Add(false);
		pressedReady.Add(false);
		pressedReady.Add(false);
		pressedReady.Add(false);
		pressedReady.Add(false);
	}

	Text numberToText(int i) {
		if(i == 0)
			return yellow;
		if(i == 1)
			return green;
		if(i == 2)
			return red;
		if(i == 3)
			return blue;
		return null;
	}

	void Update() {
		for(int i = 1; i < 12; i++) {
			if(Input.GetAxis("Jump" + i) != 0f && !pressedReady[i]) {
				pressedReady[i] = true;
				if(assigned.Contains(i)) {
					int pos = assigned.IndexOf(i);
					Text t = numberToText(pos);
					if(t != null) {
						if(t.transform.parent.GetComponent<Outline>().effectColor == Color.black) {
							t.transform.parent.GetComponent<Outline>().effectColor = Color.green;
						} else {
							t.transform.parent.GetComponent<Outline>().effectColor = Color.black;
						}
					}
				}
			}

			if(Input.GetAxis("Fire" + i) != 0f && !pressed[i]) {
				pressed[i] = true;
				if(assigned.Contains(i)) {
					int pos = assigned.IndexOf(i);
					Text t = numberToText(pos);
					if(t != null) {
						if(t.transform.parent.GetComponent<Outline>().effectColor == Color.black) {
							if(pos == 0) {
								assigned[0] = 0;
								if(assigned[1] == 0) {
									assigned[1] = i;
								} else if(assigned[2] == 0) {
									assigned[2] = i;
								} else if(assigned[3] == 0) {
									assigned[3] = i;
								}
							} else if(pos == 1) {
								assigned[1] = 0;
								if(assigned[2] == 0) {
									assigned[2] = i;
								} else if(assigned[3] == 0) {
									assigned[3] = i;
								}
							} else if(pos == 2) {
								assigned[2] = 0;
								if(assigned[3] == 0) {
									assigned[3] = i;
								}
							} else {
								assigned[3] = 0;
							}
						}
					}
				} else {
					if(assigned[0] == 0) {
						assigned[0] = i;
					} else if(assigned[1] == 0) {
						assigned[1] = i;
					} else if(assigned[2] == 0) {
						assigned[2] = i;
					} else if(assigned[3] == 0) {
						assigned[3] = i;
					}
				}
			}

			if(Input.GetAxis("Fire" + i) == 0f && pressed[i]) {
				pressed[i] = false;
			}

			if(Input.GetAxis("Jump" + i) == 0f && pressedReady[i]) {
				pressedReady[i] = false;
			}
		}

		yellow.text = assigned[0] == 0 ? ledig : "Player " + assigned[0];
		green.text = assigned[1] == 0 ? ledig : "Player " + assigned[1];
		red.text = assigned[2] == 0 ? ledig : "Player " + assigned[2];
		blue.text = assigned[3] == 0 ? ledig : "Player " + assigned[3];
	}
}
