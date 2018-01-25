﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Q_Alpha_Spawner : MonoBehaviour {

	public GameObject[] alien;
	public GameObject planet;
	public Text scoreTxt;
	public Text livesTxt;

	public ControleDeFases control;

	float limx;
	float limy;
	float limxb;
	float limyb;

	int score = 0;
	int lives = 10;
	public int spawnNumber = 0;

	void Start () {

		float spawnRate = 0.5f;

		InvokeRepeating("CreateAlien", 0, spawnRate);

		scoreTxt.text = "Aliens Mortos: " + score;
		livesTxt.text = "HP: " + lives;

	}

	void Update () {

		GameObject[] alienList = GameObject.FindGameObjectsWithTag("Alien");

		foreach (GameObject alien in alienList)
		{
			Vector2 alienPos = new Vector2(alien.transform.position.x, alien.transform.position.y);

			Camera camera = Camera.main;
			CameraControl script = camera.GetComponent("CameraControl") as CameraControl;

			limx = 2*script.limx - 2.5f;
			limy = 2*script.limy - 2.5f;
			limxb = 2*script.limxb + 2.5f;
			limyb = 2*script.limyb + 2.5f;

			if (alienPos.x > limxb || alienPos.x < limx || alienPos.y > limyb || alienPos.y < limy){
				Destroy(alien);
                lives = lives - 1;
                if (lives <= 0)
                {
                    SceneManager.LoadScene("GameOver");
                }
                livesTxt.text = "HP: " + lives;
            }
		}
	}

	void CreateAlien()
	{
		
		Vector2 planetPosition = new Vector2(planet.transform.position.x, planet.transform.position.y);
		int qnt = alien.Length;
		int side = Random.Range(1, 5);
		int type = Random.Range(0, (qnt));

		Vector2 move, alienPos;

		Camera camera = Camera.main;
		CameraControl script = camera.GetComponent("CameraControl") as CameraControl;

		float angle = Random.Range (0f, 2*Mathf.PI);

		alienPos = new Vector2 (planetPosition.x, planetPosition.y);

		alienPos = alienPos + 2*(new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)));

		float dx = alienPos.x - planetPosition.x;
		float dy = alienPos.y - planetPosition.y;

		move = new Vector2(dx, dy);
		move = move.normalized;

		if (control.fase == FaseDeJogo.Jogo){
				GameObject newAlien = Instantiate (alien[type], alienPos, alien[0].transform.rotation);
				newAlien.SetActive (true);
				newAlien.GetComponent<Rigidbody2D> ().velocity = move * 5;
			
			spawnNumber++;
		}
	}

	public void addScore(){
		score = score + 1;
		scoreTxt.text = "Aliens mortos: " + score/2;
	}
}
