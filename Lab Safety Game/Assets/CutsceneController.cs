using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;


public class CutsceneController : MonoBehaviour {
	private int index = 0;
	private List<string> quotes = new List<string>();
	private List<GameObject> cutscenes = new List<GameObject>();
	// Use this for initialization
	void Start () {
		fadeStart ();
		quotes.Add ("Caitlin: Hey Eric! Whatcha doing? \n");
		quotes.Add ("Eric: Hey Caitlin, so the news says that there’s some sort of outbreak...\nit’s making people do dumb things…And supposedly, they die from it!");
		quotes.Add ("Eric: I’m going to lab to to see if I can make a cure. \nIf you’ve got the time, why not come?");
		quotes.Add ("Caitlin: Oh no! I didn’t know we needed an outbreak for that to happen! We’ve got to hurry!\n");
		quotes.Add ("Caitlin: I haven’t been in here in forever, \nEric, are you sure this is a good idea?\n");
		quotes.Add ("Eric: No worries Caitlin, I’ll show you how it’s done. \nI actually read about this cure in this book I read a while back, but we’re going to need to prep some materials first. \nIt’s going to be dangerous, so try not to do too many dumb things, okay? \nI’ll give you some guidance!\n");

		GameObject.Find ("textMsg").GetComponent<TextMesh> ().text = quotes [index];
		cutscenes.Add (GameObject.Find ("cutscene1"));
		cutscenes.Add (GameObject.Find ("cutscene1"));
		cutscenes.Add (GameObject.Find ("cutscene1"));
		cutscenes.Add (GameObject.Find ("cutscene1"));
		cutscenes.Add (GameObject.Find ("cutscene2"));
		cutscenes.Add (GameObject.Find ("cutscene2"));
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
						SceneManager.LoadScene ("Main");
					} 
					break;
				
				default:
					break;
				}

			}
		}
	}
}
