using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
	public float targetTime = 10.0f;
	private Animator animator;
	public GameObject gameover;
	public GameObject wingame;
	public GameObject normalsetup;
	public GameObject progressbar;
	private bool leftOrRight  = false;
	private bool upOrDown  = false;
	private float progress = 0;
	private float lastCorrectTime = 0;
	private float lastCorrectTimeShave = 0;
	private bool endGame = false;
	private bool winGame = false;
	private float MAX_PROGRESS = 80;
	private float delay = 5f;

	// Use this for initialization
	void Start()
	{
		animator = this.GetComponent<Animator>();
		gameover.gameObject.SetActive (false);
		wingame.gameObject.SetActive (false);
		leftOrRight = false;
		upOrDown = false;
		progress = 0;
		lastCorrectTime = 0;
		lastCorrectTimeShave = 0;
	}

	// Update is called once per frame
	void Update()
	{
		if (!endGame) {
			if (Input.GetKeyUp (KeyCode.RightArrow)) {
				if (leftOrRight) {
					giveProgress ();
				} else {
					animator.SetInteger ("State", 0);
				}
			} else if (Input.GetKeyUp (KeyCode.LeftArrow)) {
				if (!leftOrRight) {
					giveProgress ();
				} else {
					animator.SetInteger ("State", 0);
				}
			}
			if (Input.GetKeyUp (KeyCode.UpArrow)) {
				if (upOrDown) {
					giveProgressShave ();
				} else {
					animator.SetInteger ("State", 0);
				}
			} else if (Input.GetKeyUp (KeyCode.DownArrow)) {
				if (!upOrDown) {
					giveProgressShave ();
				} else {
					animator.SetInteger ("State", 0);
				}
			}
			if (lastCorrectTime - targetTime > 0.5f && lastCorrectTimeShave - targetTime > 0.5f) {
				animator.SetInteger ("State", 0);//if it's been too long since you did something correct
			}
			targetTime -= Time.deltaTime;
		} else {
			delay -= Time.deltaTime;
			if (delay < 0) {
				if (winGame) {
					SceneManager.LoadScene ("Eyes");
				} else {
					Manager.Instance.deaths++;
					Debug.Log (Manager.Instance.deaths);
					SceneManager.LoadScene ("Bunsen");
				}
			}
		}
		GameObject.Find ("checkbox").transform.position = new Vector3 (-3.8f+progress*(9/MAX_PROGRESS), 4.5f, 0.9f);
		if (targetTime <= 0.0f && !endGame)
		{
			timerEnded();
			endGame = true;
		}
		if (progress > MAX_PROGRESS && !endGame) {//got enough 
			Debug.Log("WIN");
			winGame = true;
			endGame = true;
			this.gameObject.transform.position = new Vector3 (-4.24f,3.5f,10.0f);
			animator.SetInteger ("State", 3);
			wingame.gameObject.SetActive (true);
			normalsetup.gameObject.SetActive (false);
		}
	}
	void giveProgress()
	{
		leftOrRight = !leftOrRight;
		animator.SetInteger ("State", 1);
		progress++;
		lastCorrectTime = targetTime;
	}
	void giveProgressShave()
	{
		upOrDown = !upOrDown;
		animator.SetInteger ("State", 4);
		lastCorrectTimeShave = targetTime;
	}
	void timerEnded()
	{
		animator.SetInteger ("State", 2);
		gameover.gameObject.SetActive (true);
		normalsetup.gameObject.SetActive (false);
	}

}
