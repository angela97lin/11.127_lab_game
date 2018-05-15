using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cutscene3Controller : MonoBehaviour {
	private int index = 0;
	private List<string> quotes = new List<string>();
	private List<GameObject> cutscenes = new List<GameObject>();
	// Use this for initialization
	void Start () {
		fadeStart ();
		quotes.Add ("Caitlin: Great, everything is all set up! \nNow all I’ve got to do is combine these things \ntogether, and the cure will be made!");

		GameObject.Find ("textMsg").GetComponent<TextMesh> ().text = quotes [index];
		cutscenes.Add (GameObject.Find ("cutscene11"));
		toggleCutscene ();
	}
	void toggleCutscene()
	{
		for (int i = 0; i < cutscenes.Count; i++) {
			cutscenes [i].gameObject.SetActive (false);
		}
		cutscenes [index].gameObject.SetActive (true);
	}
	void fadeStart()
	{
		GameObject.Find ("fader").GetComponent<SpriteRenderer> ().color = new Color(0,0,0,1f);
	}

	// Update is called once per frame
	void Update () {
		//fades out the scene
		if (GameObject.Find ("fader").GetComponent<SpriteRenderer> ().color.a > 0f) {
			GameObject.Find ("fader").GetComponent<SpriteRenderer> ().color -= new Color(0,0,0,.02f);
		}
		if (Input.GetMouseButtonDown (0)) {
			Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			Vector2 mousePos2D = new Vector2 (mousePos.x, mousePos.y);

			RaycastHit2D hit = Physics2D.Raycast (mousePos2D, Vector2.zero);
			if (hit.collider != null) {
				switch (hit.collider.gameObject.name) {
				case "continue":
					if (index < quotes.Count - 1) {
						index++;
						GameObject.Find ("textMsg").GetComponent<TextMesh> ().text = quotes [index];
						GameObject.Find ("fader").GetComponent<SpriteRenderer> ().color = new Color(0,0,0,1f);
						toggleCutscene ();
					} else {
						SceneManager.LoadScene ("Crystallize");
					} 
					break;

				default:
					break;
				}

			}
		}
	}
}
