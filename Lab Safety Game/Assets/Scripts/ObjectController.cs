using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour {
	
	public bool isImportant;
	public GameObject player;
	public Vector3 finalPostition;

	private bool isPressed;
	private bool active;

	private Vector3 startPosition;
	private int importantItems;

	// Use this for initialization
	void Start () {
		startPosition = transform.position;
		active = true;
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		if (isPressed && active) 
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
		if (active)
		{
			transform.position = startPosition;
		}
	}

	void OnTriggerEnter2D (Collider2D other) 
	{
		if (other.gameObject.name == "Bean")
		{
			active = false;
			transform.localPosition = finalPostition;
			transform.eulerAngles = Vector3.zero;
			if (isImportant){
				player.GetComponent<BeanController>().importantItems += 1;
			}
		}
	}

	
}
