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
	private bool gameOver = false;

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
		
		if (time >= duration && !gameOver) {
			if (importantItems == 1) {
				win = true;	
			}
			gameOver = true;

		} else if (time >= duration) {
			EndGame (win);
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
			if (time >= duration + 5f) {
				SceneManager.LoadScene("Cutscene2");
			}

		}
		else 
		{
			
			endText.text = "Proper equipment is very important for safety!";
			if (time >= duration + 5f) {
				Manager.Instance.deaths++;
				SceneManager.LoadScene("Eyes");
			}

		}
	}
}