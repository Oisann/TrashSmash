using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSpawner : MonoBehaviour {

	public string[] scenes;

	void Awake() {
		for(int i = 0; i < scenes.Length; i++) {
			SceneManager.LoadSceneAsync(scenes[i], LoadSceneMode.Additive);
		}
	}
}
