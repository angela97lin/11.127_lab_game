using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ColorChanger : MonoBehaviour {

	private GameObject player;
	private float duration;
	private bool endgame;

	private float t = 0;
	
	private SpriteRenderer renderer;
	
	void Start()
	{
		renderer = GetComponent<SpriteRenderer>();
		player = GameObject.FindWithTag("Player");
		duration = player.GetComponent<GoggleController>().duration;
		endgame = player.GetComponent<GoggleController>().endgame;
		
	}
	
	// Update is called once per frame
	void Update () 
	{
			ChangeColor();
	}

	void ChangeColor() 
	{
		renderer.color = Color.Lerp(Color.cyan, Color.red, t);

		if (t<1){
			t += Time.deltaTime/duration;
		}
	}
}
