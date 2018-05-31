using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ReadySetGo : MonoBehaviour {
	public Text red, green, yellow, blue;
	public JoystickAssigner ja;
	public Text countdown;
	public AudioClip[] clips;

	public bool reed, greeen, bluue, yelllow;

	private int time = 5;
	private System.Random rng = new System.Random();

	private void Shuffle<T>(List<T> list) {  
		int n = list.Count;
		while (n > 1) {
			n--;
			int k = rng.Next(n + 1);
			T value = list[k];
			list[k] = list[n];
			list[n] = value;
		}
	}

	public static string getNextScene(){
		if(PlayerPrefs.HasKey("maps")) {
			string maps = PlayerPrefs.GetString("maps");
			string nextMap = maps.Split("%".ToCharArray())[0];
			if(!string.IsNullOrEmpty(nextMap)) {
				if(maps.Length == nextMap.Length) {
					maps = maps.Replace(nextMap, "");
				} else {
					maps = maps.Replace(nextMap + "%", "");
				}
				PlayerPrefs.SetString("maps", maps);
				return nextMap;
			}
		}
		//Default map
		return "controllerMapper";
	}

	void StartGame() {
		PlayerPrefs.DeleteAll();
		PlayerPrefs.SetInt("yellowPlayer", ja.assigned[0]);
		PlayerPrefs.SetInt("greenPlayer", ja.assigned[1]);
		PlayerPrefs.SetInt("redPlayer", ja.assigned[2]);
		PlayerPrefs.SetInt("bluePlayer", ja.assigned[3]);

		PlayerPrefs.SetInt("redScore", 0);
		PlayerPrefs.SetInt("greenScore", 0);
		PlayerPrefs.SetInt("yellowScore", 0);
		PlayerPrefs.SetInt("blueScore", 0);

		List<string> maps = new List<string>();
		maps.Add("TrashHoarder");
		maps.Add("OvenThing");
		maps.Add("CollectFallingTrash");
		maps.Add("Balle knall og fall");

		Shuffle<string>(maps);
		
		maps.Add("winners");
		
		string m = "";
		foreach(string map in maps) {
			m += "%" + map;
		}
		m = m.Remove(0, 1);

		PlayerPrefs.SetString("maps", m);

		SceneManager.LoadScene(getNextScene());
	}

	void UpdateText() {
		GetComponent<AudioSource>().PlayOneShot(clips[time]);
		countdown.text = time + "";
		time = Mathf.Clamp(time-1, 0, 10);
	}
	
	void Update() {
		if(red.text == ja.ledig &&
			green.text == ja.ledig &&
			blue.text == ja.ledig &&
			yellow.text == ja.ledig) {

			reed = false;
			greeen = false;
			bluue = false;
			yelllow = false;
		} else {
			reed = red.text == ja.ledig || (red.text != ja.ledig && red.transform.parent.GetComponent<Outline>().effectColor == Color.green);
			greeen = green.text == ja.ledig || (green.text != ja.ledig && green.transform.parent.GetComponent<Outline>().effectColor == Color.green);
			bluue = blue.text == ja.ledig || (blue.text != ja.ledig && blue.transform.parent.GetComponent<Outline>().effectColor == Color.green);
			yelllow = yellow.text == ja.ledig || (yellow.text != ja.ledig && yellow.transform.parent.GetComponent<Outline>().effectColor == Color.green);
		}

		countdown.enabled = reed && greeen && bluue && yelllow;

		if(reed && greeen && bluue && yelllow) {
			if(!IsInvoking("StartGame")) {
				Invoke("StartGame", 5f);
				time = 5;
				InvokeRepeating("UpdateText", 0f, 1f);
			}
		} else {
			if(IsInvoking("StartGame"))
				CancelInvoke();
		}
	}
}
