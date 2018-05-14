using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;


public class Cutscene2Controller : MonoBehaviour {
	private int index = 0;
	private List<string> quotes = new List<string>();
	private List<GameObject> cutscenes = new List<GameObject>();
	// Use this for initialization
	void Start () {
		fadeStart ();
		quotes.Add ("Eric: Great, we’ve geared ourselves up! \nNow, let’s get all of the lab equipment \nset up! Hmm… I have forgotten the steps… Caitlin, can \nyou get me the book with the cure? It’s in my briefcase!\n");
		quotes.Add ("Caitlin: Sure thing!");
		quotes.Add ("Caitlin: Hey, Eric, what’s this? Doesn’t it look like \nthe poison causing the outbreak as described in the book? \nWhy do you have it?-\n");
		quotes.Add ("Eric: Nothing’s going to get in the way of my plan! \nI need to infect more people so I can \nfinally sell all of my my master educational games and books! ");
		quotes.Add ("Eric: You’re not supposed to \ntouch that--give it to me!");
		quotes.Add ("CRASH!!!");
		quotes.Add ("Eric: Huh? Did the poison just...");
		quotes.Add ("Eric: BLehawefjalwkjeflajwefja;wf..duhhh....");
		quotes.Add ("Caitlin: No, Eric! Looks like he forgot a key lab safety principle...\nhow fitting. What am I supposed to do?");
		quotes.Add ("I guess I’ve got no choice but to follow what this book \nsays and continue with the cure. \n\n");

		GameObject.Find ("textMsg").GetComponent<TextMesh> ().text = quotes [index];
		cutscenes.Add (GameObject.Find ("cutscene3"));
		cutscenes.Add (GameObject.Find ("cutscene3"));
		cutscenes.Add (GameObject.Find ("cutscene4"));
		cutscenes.Add (GameObject.Find ("cutscene5"));
		cutscenes.Add (GameObject.Find ("cutscene6"));
		cutscenes.Add (GameObject.Find ("cutscene7"));
		cutscenes.Add (GameObject.Find ("cutscene8"));
		cutscenes.Add (GameObject.Find ("cutscene9"));
		cutscenes.Add (GameObject.Find ("cutscene10"));
		cutscenes.Add (GameObject.Find ("cutscene10"));
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
