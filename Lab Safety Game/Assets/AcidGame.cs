using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

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
	public GameObject lose_text;
	public GameObject check;
	public GameObject fire;


	public List<GameObject> textList;
	public enum Chemicals {sodium_hydroxide, calcium_hydroxide, ammonia, water, sulfuric_acid};

	public List<Chemicals> userList;
	public List<Chemicals> correctList;

	public float targetTime = 10.0f;
	 

	// Use this for initialization
	void Start () {

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
		lose_text.gameObject.SetActive (false);
		fire.gameObject.SetActive (false);


	}

	void timerEnded()
	{
		instruction_text.gameObject.SetActive (false);
		lose_text.gameObject.SetActive (true);
	}
	
	// Update is called once per frame
	void Update () {

		targetTime -= Time.deltaTime;
		print (targetTime);
		if (targetTime <= 50.0f)
		{
			timerEnded();
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
					bool correct = userList.SequenceEqual (correctList);
					if (correct) {
						instruction_text.gameObject.SetActive (false);
						win_text.gameObject.SetActive (true);
					} else {
						instruction_text.gameObject.SetActive (false);
						lose_text.gameObject.SetActive (true);

						if (userList.Contains (Chemicals.water) && userList.Contains (Chemicals.sulfuric_acid)) {
							int water_index = userList.IndexOf (Chemicals.water); 
							int sa_index = userList.IndexOf (Chemicals.sulfuric_acid); 
							if (sa_index < water_index) {
								fire.gameObject.SetActive (true);
							}

						}
			
					}
				}
			}
		}
	}

}
