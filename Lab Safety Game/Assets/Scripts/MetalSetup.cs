using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MetalSetup : MonoBehaviour {

	public GameObject zinc;
	public GameObject copper;
	public GameObject magnesium;
	public GameObject iron;
	public bool timerOn;
	public float maxTime;
	public float timeLeft;
	public Image timerBar;

	public enum Metal {zinc, iron, magnesium, copper};

	public Metal chosenMetal;
	public GameObject instructionText;
	public GameObject lose_magnesium;
	public GameObject lose_copper;
	public GameObject lose_iron;
	public GameObject lose_time;
	public GameObject winText;
	public GameObject fire;

	// Use this for initialization
	void Start () {

		timerOn = true;
		timeLeft = 5f;
		maxTime = 5f;

		instructionText.gameObject.SetActive (true);		
		lose_iron.gameObject.SetActive (false);	
		lose_magnesium.gameObject.SetActive (false);	
		lose_copper.gameObject.SetActive (false);	
		winText.gameObject.SetActive (false);		
	}

	void timerEnded()
	{
		timerOn = false;
		instructionText.gameObject.SetActive (false);
		lose_time.gameObject.SetActive (true);
	}

	void Check() {
		timerOn = false;
		instructionText.gameObject.SetActive (false);

		if (chosenMetal == Metal.zinc) {
			winText.gameObject.SetActive (true);
		} else if (chosenMetal == Metal.iron) {
			lose_iron.gameObject.SetActive (true);
			fire.gameObject.SetActive (true);

		} else if (chosenMetal == Metal.copper) {
			lose_copper.gameObject.SetActive (true);
			fire.gameObject.SetActive (true);
		} else if (chosenMetal == Metal.magnesium) {
			lose_magnesium.gameObject.SetActive (true);
			fire.gameObject.SetActive (true);

		}
	}
	
	// Update is called once per frame
	void Update () {
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
