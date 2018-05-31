using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour {
	public string player = "red";

	private Text count;

	void Start () {
		count = GetComponent<Text>();
	}
	
	void Update() {
		bool enabled = PlayerPrefs.GetInt(player.ToLower() + "Player", 0) != 0;
		count.transform.parent.gameObject.SetActive(enabled);
		int score = PlayerPrefs.GetInt(player.ToLower() + "Score", 0);
		count.text = score + "";
	}
}
