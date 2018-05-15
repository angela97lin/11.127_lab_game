using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BeanController : MonoBehaviour {
    
    public float duration;
    public Text endText;
	public Text instructionText;
	public GameObject splats;

    private float time;
	//public bool endgame;
	private bool win;
    public int importantItems;

    void Start ()
    {
        //endgame = false;
		win = false;
		time = 0;
        importantItems = 0;
        endText.text = "";
    }

    void Update ()
    {
		
        if (time >= duration) 
		{
			if (importantItems == 3)
        	{
            	win = true;	
        	}
			EndGame(win);
		}

        

		time += Time.deltaTime;
    }

    void EndGame(bool win)
	{
		splats.SetActive(true);
		instructionText.text = "";
		if (win) 
		{
			endText.text = "Great Job! You win!";
			SceneManager.LoadScene("Cutscene2");
		}
		else 
		{
			Manager.Instance.deaths++;
			endText.text = "Proper equipment is very important for safety!";
			SceneManager.LoadScene("Eyes");
		}
	}
}