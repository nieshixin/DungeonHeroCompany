﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookTargetFollowMouse : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		transform.position =  Camera.main.ScreenToWorldPoint (Input.mousePosition);
		
	}
}
