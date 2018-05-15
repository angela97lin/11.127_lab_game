using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MetalSetup : MonoBehaviour {

	public GameObject zinc;
	public GameObject copper;
	public GameObject magnesium;
	public GameObject iron;
	public bool timerOn;
	public float maxTime;
	public float timeLeft;
	public Image timerBar;

	public enum Metal {zinc, iron, magnesium, copper, timesup};

	public Metal chosenMetal;
	public GameObject instructionText;
	public GameObject lose_magnesium;
	public GameObject lose_copper;
	public GameObject lose_iron;
	public GameObject lose_time;
	public GameObject winText;
	public GameObject fire;
	public GameObject restartText;

	public bool gameEnded;
	public bool won;


	// Use this for initialization
	void Start () {
		won = false;

		timerOn = true;
		timeLeft = 5f;
		maxTime = 5f;
		chosenMetal = Metal.timesup;
		instructionText.gameObject.SetActive (true);		
		lose_iron.gameObject.SetActive (false);	
		lose_magnesium.gameObject.SetActive (false);	
		lose_copper.gameObject.SetActive (false);	
		winText.gameObject.SetActive (false);	
		restartText.gameObject.SetActive (false);		
	}

	void restartTimer(){
		gameEnded = true;
		timerOn = true;
		timeLeft = 5f;
		maxTime = 5f;
	}

	void timerEnded()
	{
		timerOn = false;
		instructionText.gameObject.SetActive (false);
		lose_time.gameObject.SetActive (true);
	}

	void Check() {
		gameEnded = true;
		timerOn = false;
		instructionText.gameObject.SetActive (false);
		restartTimer ();

		if (chosenMetal == Metal.zinc) {
			winText.gameObject.SetActive (true);
			won = true;
		} else if (chosenMetal == Metal.iron) {
			lose_iron.gameObject.SetActive (true);
			fire.gameObject.SetActive (true);
			restartText.gameObject.SetActive (true);

			Manager.Instance.deaths++;

		} else if (chosenMetal == Metal.copper) {
			lose_copper.gameObject.SetActive (true);
			fire.gameObject.SetActive (true);
			restartText.gameObject.SetActive (true);

			Manager.Instance.deaths++;

		} else if (chosenMetal == Metal.magnesium) {
			lose_magnesium.gameObject.SetActive (true);
			fire.gameObject.SetActive (true);
			restartText.gameObject.SetActive (true);

			Manager.Instance.deaths++;
		} else {
			restartText.gameObject.SetActive (true);

			timerEnded ();
		}
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
					SceneManager.LoadScene ("Acid");
				}
			} else {
				if (timeLeft > 0) {
					print (timeLeft);
					timeLeft -= Time.deltaTime;
					timerBar.fillAmount = timeLeft / maxTime;
				} else {
					SceneManager.LoadScene ("MetalSetup");
				}
			}
		} else {
			if (timerOn) {
				if (timeLeft > 0) {
					timeLeft -= Time.deltaTime;
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
					if (name.Equals ("fe")) {
						chosenMetal = Metal.iron;
						Check ();
					} else if (name.Equals ("cu")) {
						chosenMetal = Metal.copper;
						Check ();

					} else if (name.Equals ("zn")) {
						chosenMetal = Metal.zinc;
						Check ();

					} else if (name.Equals ("mg")) {
						chosenMetal = Metal.magnesium;
						Check ();

					} 
				}
			}
		}
	}
}
