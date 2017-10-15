﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolButtons : MonoBehaviour {

	private GameObject[] toolList;
	public ControleDeFases control;

	private GameObject Cannon;
	private GameObject LaserCannon;

	void Start () {
		toolList = GameObject.FindGameObjectsWithTag("Tool");
	}
	
	void OnMouseDown() {

		if (control.fase == FaseDeJogo.Jogo) {
			foreach (GameObject tool in toolList)
				tool.SetActive (false);

			if (this.name == "Button1")
				control.activateCannon = true;

			if (this.name == "Button2")
				control.activateLaser = true;
		}
	}
}
