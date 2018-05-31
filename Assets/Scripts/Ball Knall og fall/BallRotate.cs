using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallRotate : MonoBehaviour {

	public float rotSpeed;
	public string playerName = "red";
	public int playerNum = 1;
	Vector3 ballRot;
	public float moveSpeed;
	Rigidbody rb;
	public float speed;

	public CountdownTimer timer;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		playerNum = PlayerPrefs.GetInt(playerName.ToLower() + "Player", 0);
		if(playerNum == 0) {
			Destroy(gameObject);
		}
	}

	void score(string s) {
		int i = PlayerPrefs.GetInt(s + "Score", 0);
		i++;
		PlayerPrefs.SetInt(s + "Score", i);
	}
	
	// Update is called once per frame
	void Update () {
		if(timer == null) {
			timer = GameObject.FindObjectOfType<CountdownTimer>();
		} else {
			if(timer.currentTime <= 0f) {
				score(playerName);
				playerName = "";
				Destroy(this);
			}
		}

		if (Input.GetAxis ("Horizontal" + playerNum) != 0f)
		{
			ballRot.z  = (-Input.GetAxis ("Horizontal" + playerNum));
			transform.Rotate (ballRot,Space.World);
			rb.AddForce (Vector3.right * Input.GetAxis ("Horizontal" + playerNum) * speed,ForceMode.Impulse);
		}

		if (Input.GetAxis ("Vertical" + playerNum) != 0f)
		{
			ballRot.x  = (Input.GetAxis ("Vertical" + playerNum));
			transform.Rotate (ballRot,Space.World);
			rb.AddForce (Vector3.forward * Input.GetAxis ("Vertical" + playerNum )* speed,ForceMode.Impulse);
		}
			
		if (transform.position.y <= -10)
		{
			Destroy (gameObject);
		}

		}
		
	}

