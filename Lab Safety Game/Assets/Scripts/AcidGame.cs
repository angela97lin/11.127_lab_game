using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AcidGame : MonoBehaviour {

//	public GameObject gameover;
//	public GameObject wingame;
//	public GameObject normalsetup;
//	public GameObject progressbar;


	public GameObject sodium_hydroxide; //naoh 
	public GameObject calcium_hydroxide; //caoh2
	public GameObject ammonia;//nh4oh
	public GameObject water;
	public GameObject sulfuric_acid;
	public GameObject sodium_hydroxide_text; //naoh 
	public GameObject calcium_hydroxide_text; //caoh2
	public GameObject ammonia_text;//nh4oh
	public GameObject water_text;
	public GameObject sulfuric_acid_text;
	public GameObject instruction_text;
	public GameObject win_text;
	public GameObject lose_sodium_hydroxide_text;
	public GameObject lose_calcium_hydroxide_text;
	public GameObject lose_ammonia_text;
	public GameObject lose_too_many_text;
	public GameObject lose_order_text;
	public GameObject lose_time_text;

	public GameObject lose_general_text;
	public GameObject restart_text;

	public GameObject check;
	public GameObject fire;

	public Image timerBar;

	public List<GameObject> textList;
	public enum Chemicals {sodium_hydroxide, calcium_hydroxide, ammonia, water, sulfuric_acid};

	public List<Chemicals> userList;
	public List<Chemicals> correctList;

	public bool timerOn;
	public float maxTime;
	public float timeLeft;


	public bool gameEnded;
	public bool won;


	// Use this for initialization
	void Start () {
		timerOn = true;
		timeLeft = 5f;
		maxTime = 5f;
		userList = new List<Chemicals> ();
		correctList = new List<Chemicals> ();
		correctList.Add (Chemicals.water);
		correctList.Add (Chemicals.sulfuric_acid);


		textList = new List<GameObject> ();
		textList.Add (sodium_hydroxide_text);
		textList.Add (calcium_hydroxide_text);
		textList.Add (ammonia_text);
		textList.Add (sulfuric_acid_text);
		textList.Add (water_text);

		foreach (GameObject g in textList) {
			g.gameObject.SetActive (false);
		}
			
		instruction_text.gameObject.SetActive (true);
		win_text.gameObject.SetActive (false);
		lose_sodium_hydroxide_text.gameObject.SetActive (false);
		lose_calcium_hydroxide_text.gameObject.SetActive (false);
		lose_ammonia_text.gameObject.SetActive (false);
		lose_too_many_text.gameObject.SetActive (false);
		lose_order_text.gameObject.SetActive (false);
		lose_time_text.gameObject.SetActive (false);
		lose_general_text.gameObject.SetActive (false);
		fire.gameObject.SetActive (false);
		restart_text.gameObject.SetActive (false);

	}


	void restartTimer(){
		gameEnded = true;
		timerOn = true;
		timeLeft = 5f;
		maxTime = 5f;
	}

	void timerEnded()
	{
		instruction_text.gameObject.SetActive (false);
		lose_time_text.gameObject.SetActive (true);
	}


	void Check(){
		closeOtherText ();
		timerOn = false;
		bool correct = userList.SequenceEqual (correctList);
		if (correct) {
			instruction_text.gameObject.SetActive (false);
			win_text.gameObject.SetActive (true);
			restartTimer ();
			won = true;
		} else {
			instruction_text.gameObject.SetActive (false);
			Manager.Instance.deaths++;
			restartTimer ();
			restart_text.gameObject.SetActive (true);

			if (userList.Contains (Chemicals.water) && userList.Contains (Chemicals.sulfuric_acid)) {
				int water_index = userList.IndexOf (Chemicals.water); 
				int sa_index = userList.IndexOf (Chemicals.sulfuric_acid); 
				if (sa_index < water_index) {
					fire.gameObject.SetActive (true);
					lose_order_text.gameObject.SetActive (true);
				}
			} else if (userList.Count > 2) {
				lose_too_many_text.gameObject.SetActive (true);
			} else if (userList.Contains (Chemicals.ammonia)) {
				lose_ammonia_text.gameObject.SetActive (true);
			} else if (userList.Contains (Chemicals.calcium_hydroxide)) {
				lose_calcium_hydroxide_text.gameObject.SetActive (true);
			} else if (userList.Contains (Chemicals.sodium_hydroxide)) {
				lose_sodium_hydroxide_text.gameObject.SetActive (true);
			} else {
				if (timeLeft == 0) {
					timerEnded ();
				} else {
					lose_general_text.gameObject.SetActive (true);
				}
			}
		}
	}

	void closeOtherText(){
		water_text.gameObject.SetActive (false);
		sulfuric_acid_text.gameObject.SetActive (false);
		ammonia_text.gameObject.SetActive (false);
		sodium_hydroxide_text.gameObject.SetActive (false);
		calcium_hydroxide_text.gameObject.SetActive (false);
	}

	// Update is called once per frame
	void Update () {
		if (gameEnded) {
			if (won) {
				if (timeLeft > 0) {
					print (timeLeft);
					timeLeft -= Time.deltaTime;
					timerBar.fillAmount = timeLeft / maxTime;
				} else {
					SceneManager.LoadScene ("Cutscene3");
				}
			} else {
				if (timeLeft > 0) {
					print (timeLeft);
					timeLeft -= Time.deltaTime;
					timerBar.fillAmount = timeLeft / maxTime;
				} else {
					SceneManager.LoadScene ("Acid");
				}
			}
		} else {
			if (timerOn) {
				if (timeLeft > 0) {
					timeLeft -= Time.deltaTime;
					print (timeLeft + " " + maxTime);
					print (timeLeft / maxTime);
					timerBar.fillAmount = timeLeft / maxTime;
				} else {
					Check ();
				}
			}
			if (Input.GetMouseButtonDown (0)) {
				Vector2 origin = new Vector2 (Camera.main.ScreenToWorldPoint (Input.mousePosition).x,
					                Camera.main.ScreenToWorldPoint (Input.mousePosition).y);
				RaycastHit2D hit = Physics2D.Raycast (origin, Vector2.zero, 0f);
				if (hit) {
					print (hit.transform.gameObject.name);
					string name = hit.transform.gameObject.name;
					foreach (GameObject g in textList) {
						g.gameObject.SetActive (false);
					}
					if (name.Equals ("water")) {
						water_text.gameObject.SetActive (true);
						userList.Add (Chemicals.water);
					} else if (name.Equals ("h2so4")) {
						sulfuric_acid_text.gameObject.SetActive (true);
						userList.Add (Chemicals.sulfuric_acid);

					} else if (name.Equals ("nh4oh")) {
						ammonia_text.gameObject.SetActive (true);
						userList.Add (Chemicals.ammonia);

					} else if (name.Equals ("caoh2")) {
						calcium_hydroxide_text.gameObject.SetActive (true);
						userList.Add (Chemicals.calcium_hydroxide);

					} else if (name.Equals ("naoh")) {
						sodium_hydroxide_text.gameObject.SetActive (true);
						userList.Add (Chemicals.sodium_hydroxide);

					} else if (name.Equals ("check")) {
						Check ();
					}
				}
			}
		}
	}

}
