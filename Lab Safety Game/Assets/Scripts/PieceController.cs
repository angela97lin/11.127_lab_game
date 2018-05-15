using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PieceController : MonoBehaviour {
    //public GameObject jar;
    private BeakerController beaker;

    private bool isPressed;

    void Start() 
    {
        isPressed = false;
        beaker = GameObject.Find("BoilingBeaker").GetComponent<BeakerController>();
    }

    void Update()
    {
        if (isPressed) 
		{
			Vector2 MousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
      		Vector2 objPosition = Camera.main.ScreenToWorldPoint(MousePosition);
      		transform.position = objPosition;
		}
    }

    void OnMouseDown()
	{
		isPressed = true;
	}

	void OnMouseUp()
	{
		isPressed = false;
	}

    void OnTriggerEnter2D (Collider2D other) 
	{
		if (other.gameObject.name == "BoilingBeaker")
		{
			beaker.zinc += 1;
            if (beaker.zinc >= beaker.target) {
                beaker.active = true;
                GameObject.Find("BoilingBeaker").GetComponent<ParticleSystem>().Play();

            }
            gameObject.active = false;
		}
	}
}