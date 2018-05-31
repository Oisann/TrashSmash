using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrashFallController : MonoBehaviour {

	public float speed;
	private Rigidbody rb;
	public float jumpForce;
	bool axisInUse;
	public Text scoreP1;
	public int numScoreP1;
	public string playerName = "red";
	public int playerNum = 1;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		playerNum = PlayerPrefs.GetInt(playerName.ToLower() + "Player", 0);
		if(playerNum == 0) {
			Destroy(gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
		//Vector3 movePos = transform.position;

		if (Input.GetAxis ("Horizontal" + playerNum) != 0f)
			{
			//movePos += transform.right * (Input.GetAxis ("Horizontal" + playerNum) / 100f) * speed;
			rb.AddForce (Vector3.right * Input.GetAxis ("Horizontal" + playerNum) * speed,ForceMode.Acceleration);
			}

		//if (transform.position != movePos) {
		//	rb.MovePosition (movePos);
		//}
			
		if (Physics.Raycast (transform.position, -transform.up, 1.01f)) {
			if (Input.GetAxis ("Jump" + playerNum) != 0f && !axisInUse) {
				axisInUse = true;
				rb.AddForce (Vector3.up * jumpForce);
			}else if(Input.GetAxis ("Jump" + playerNum) == 0f)
				axisInUse = false;
		} 

	}

	void OnTriggerEnter(Collider other){
		if (other.tag == "Trash") {
			numScoreP1++;
			scoreP1.text = numScoreP1 + "";
			Destroy (other.gameObject);

		}
	}

}
