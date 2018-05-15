using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BeakerController : MonoBehaviour {
    public float duration;
    public Text instructionText;
    public Text endText;
    public GameObject zincJar;
    public int zinc;

    public int target;
    private bool inHood;
    private bool win;

    private bool isPressed;
	public bool active;
    private Vector3 finalPostition;
    private Vector3 startPosition;
    private float time;

    void Start() 
    {
        zinc = 0;
        time = 0;
        active = false;
        inHood = false;
        win = false;
        endText.text = "";
        target = (int) Mathf.Round(zincJar.GetComponent<ZincController>().total / 2);
        finalPostition = new Vector3(-7.17f, -1.13f, 0f);
        startPosition = transform.position;
    }

    void Update() 
    {
        if (zinc >= target)
        {
            time += Time.deltaTime;
        }
        if (isPressed && active) 
		{
			Vector2 MousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
      		Vector2 objPosition = Camera.main.ScreenToWorldPoint(MousePosition);
      		transform.position = objPosition;
		}

        if (time >= duration) 
		{
			if (inHood){
                win = true;
            }
			EndGame(win);
		}
    }

    void EndGame(bool win)
	{
		instructionText.text = "";
		if (win) 
		{
			endText.text = "Great Job! You win!";
            SceneManager.LoadScene("Crystallize");
		}
		else 
		{
            Manager.Instance.deaths++;
			endText.text = "You suffocated! Fumes are very dangerous!";
            SceneManager.LoadScene("Fumes");
		}
	}

    void OnMouseDown()
	{
		if (active) 
        {
            isPressed = true;
        }
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
		if (other.gameObject.name == "fumehood")
		{
			active = false;
            inHood = true;
			transform.localPosition = finalPostition;		
		}
	}

    
}