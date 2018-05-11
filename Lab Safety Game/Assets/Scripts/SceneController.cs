using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneController : MonoBehaviour {


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {	
		if (Input.GetMouseButtonDown(0)) {
			Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

			RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
			if (hit.collider != null) {
				switch (hit.collider.gameObject.name) {
				case "Bean":
					SceneManager.LoadScene ("Eyes");
					break;
				case "beaker":
					SceneManager.LoadScene ("Acid");
					break;
				case "BeanFile":
					SceneManager.LoadScene ("Bunsen");
					break;
				default:
					break;
				}

			}
		}

	}
}
