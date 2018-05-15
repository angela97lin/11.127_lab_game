using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadController : MonoBehaviour {

	public float delay = 3f;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		delay -= Time.deltaTime;
		if (delay < 0) {
			SceneManager.LoadScene ("Cutscene");
		}


	}
}
