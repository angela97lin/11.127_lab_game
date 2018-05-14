using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystallizeGameController : MonoBehaviour
{
	private float timetoGlass = 10.0f;
	private float timetoCrystallize = 10.0f;
	private float totalTime = 0f;
	private Animator animator;
	private Animator glassAnimator;
	public GameObject gameover;
	public GameObject gameovertooearly;

	public GameObject wingame;
	public GameObject normalsetup;
	public GameObject smoke;
	public GameObject crystal;
	public GameObject bubble;

	private bool endGame = false;
	private string phase = "glass";
	private bool crystallizedState = false;
	private int changedState = 4;

	// Use this for initialization
	void Start()
	{
		animator = this.GetComponent<Animator>();
		glassAnimator = GameObject.Find ("dish").GetComponent<Animator>();
		timetoGlass = Random.Range (5f,10f);
		timetoCrystallize = Random.Range (10f,20f);
		totalTime = timetoGlass;
		gameover.gameObject.SetActive (false);
		wingame.gameObject.SetActive (false);
	}

	// Update is called once per frame
	void Update()
	{
		if (!endGame) {
			
			totalTime -= Time.deltaTime;
			if (phase == "glass") {
				if (totalTime > timetoGlass / 1.5) {
					glassAnimator.SetInteger ("State", 5);
				} else if (totalTime > timetoGlass / 2) {
					glassAnimator.SetInteger ("State", 4);
				} else if (totalTime > timetoGlass / 3) {
					glassAnimator.SetInteger ("State", 3);
				} else if (totalTime > timetoGlass / 4) {
					glassAnimator.SetInteger ("State", 2);
				} else {
					glassAnimator.SetInteger ("State", 1);
				} 
				if (totalTime < 0) {
					timerEnded ();
					endGame = true;
				}
			} else {
				if (totalTime > timetoCrystallize / 2 && changedState == 4 && totalTime < timetoCrystallize / 1.5) {
					changedState--;
					int rand = Random.Range (1, 5);
					if (rand == 1) {
						
						crystal.gameObject.SetActive (false);
						smoke.gameObject.SetActive (true);
						bubble.gameObject.SetActive (false);
					} else if (rand == 2) {
						
						crystal.gameObject.SetActive (false);
						smoke.gameObject.SetActive (false);
						bubble.gameObject.SetActive (true);
					} else {
						crystal.gameObject.SetActive (false);
						smoke.gameObject.SetActive (false);
						bubble.gameObject.SetActive (false);
					}
				} else if (totalTime > timetoCrystallize / 5 && changedState == 3 && totalTime < timetoCrystallize / 2) {
					int rand = Random.Range (1, 3);
					changedState--;
					if (rand == 1) {
						
						crystal.gameObject.SetActive (false);
						smoke.gameObject.SetActive (true);
						bubble.gameObject.SetActive (false);
					} else if (rand == 2) {
						
						crystal.gameObject.SetActive (false);
						smoke.gameObject.SetActive (false);
						bubble.gameObject.SetActive (true);
					} else {
						crystal.gameObject.SetActive (false);
						smoke.gameObject.SetActive (false);
						bubble.gameObject.SetActive (false);
					}
				} else if (totalTime > timetoCrystallize / 10 && changedState == 2 && totalTime < timetoCrystallize / 5) {
					int rand = Random.Range (1, 2);
					changedState--;
					if (rand == 1) {
						crystal.gameObject.SetActive (false);
						smoke.gameObject.SetActive (true);
						bubble.gameObject.SetActive (false);
					} else if (rand == 2) {
						crystal.gameObject.SetActive (false);
						smoke.gameObject.SetActive (false);
						bubble.gameObject.SetActive (true);
					} 
				} else if(changedState == 1  && totalTime < timetoCrystallize / 10){
					Debug.Log ("H DEADI");
					crystallizedState = true;
					crystal.gameObject.SetActive (true);
					smoke.gameObject.SetActive (false);
					bubble.gameObject.SetActive (false);
				} 
				if (totalTime < 0) {
					timerEnded ();
					endGame = true;
				}
			}
		}

		if (Input.GetMouseButtonDown(0)) {
			Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

			RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
			if (hit.collider != null) {
				switch (hit.collider.gameObject.name) {
				case "dipglass":
					if (phase == "glass") {
						if (glassAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Last") == false) {
							timerEnded ();
							endGame = true;
						} else {
							GameObject.Find ("stirrod").transform.position = new Vector3 (-3f, 3.5f, 0f);
							totalTime = timetoCrystallize;
							phase = "heat";
						}
					}
					break;
				case "stopheating":
					if (phase == "heat") {
						if (crystallizedState == false) {
							notHeatedEnough ();
							endGame = true;
						} else {
							Debug.Log ("WIN");
							endGame = true;
							this.gameObject.transform.position = new Vector3 (-4.24f, 3.5f, 0.0f);
							animator.SetInteger ("State", 3);
							wingame.gameObject.SetActive (true);
							normalsetup.gameObject.SetActive (false);
						}
					} else {
						notHeatedEnough ();
					}
					break;
				default:
					break;
				}

			}
		}
	}
	void timerEnded()
	{
		animator.SetInteger ("State", 2);
		gameover.gameObject.SetActive (true);
		normalsetup.gameObject.SetActive (false);
	}
	void notHeatedEnough()
	{
		animator.SetInteger ("State", 2);
		gameovertooearly.gameObject.SetActive (true);
		normalsetup.gameObject.SetActive (false);
	}
}
