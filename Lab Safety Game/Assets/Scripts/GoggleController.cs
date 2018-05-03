using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoggleController : MonoBehaviour {

	public float duration;
	public Text endText;
	public Text instructionText;
	public GameObject splats;

	private float time;
	public bool endgame;
	private float position;
	private bool win;

	private bool isRotating;
	private Vector3 mouseReference;
	private Vector3 mouseOffset;
	private Vector3 rotation;

	// Use this for initialization
	void Start () {
		endgame = false;
		win = false;
		time = 0;
		rotation = Vector3.zero;
		endText.text = "";
	}
	
	// Update is called once per frame
	void Update () {
		position = transform.localEulerAngles.z;
		if (position > 180)
		{
			position -= 360;
		}

		if (!endgame)
		{
			if (isRotating)
			{
				if (position < 90 && position > 0) 
				{
					mouseOffset = (Input.mousePosition - mouseReference);
					rotation.z = -(mouseOffset.x + mouseOffset.z * 0.4f);
					
					transform.Rotate(rotation);

					mouseReference = Input.mousePosition;
				}

				if (position <= 0)
				{
					isRotating = false;
					endgame = true;
					win = true;
				}
			}
		}

		if (time >= duration) 
		{
			endgame = true;
			EndGame(win);
		}

		time += Time.deltaTime;
	}
	void OnMouseDown()
	{
		if (!endgame)
		{
			isRotating = true;
			mouseReference = Input.mousePosition;
		}
		
	}

	void OnMouseUp()
	{
		if (!endgame)
		{
			isRotating = false;
		}
		
	}

	void EndGame(bool win)
	{
		splats.SetActive(true);
		instructionText.text = "";
		if (win) 
		{
			endText.text = "Great Job! You win!";
		}
		else 
		{
			endText.text = "Eye wear is very important for eye safety!";
		}
	}
}
