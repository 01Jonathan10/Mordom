﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienAControl : MonoBehaviour {

    float distance;
    float proportion;
	float initD;
	Vector2 initSpeed;

    public GameObject planet;
	private AlienSpawner parent;

    void Start ()
    {
        float dx = planet.transform.position.x - transform.position.x;
        float dy = planet.transform.position.y - transform.position.y;
		initD = new Vector2(dx, dy).magnitude;
		initSpeed = GetComponent<Rigidbody2D>().velocity;

		parent = GameObject.FindGameObjectWithTag ("Respawn").GetComponent<AlienSpawner>();
    }
	
	// Update is called once per frame
	void Update () {
        float dx = planet.transform.position.x - transform.position.x;
        float dy = planet.transform.position.y - transform.position.y;

        distance = new Vector2(dx, dy).magnitude;
		proportion = (distance / initD) * 3;
        GetComponent<Rigidbody2D>().velocity = initSpeed * proportion;
    }

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.tag == "Tool") {
			parent.addScore ();
			Destroy (this.gameObject);
			Destroy (coll.transform.parent.gameObject);
		}
	}
}