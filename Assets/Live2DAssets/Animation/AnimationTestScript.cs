using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTestScript : MonoBehaviour {
	Animator anim;
	// Use this for initialization
	void Start () {
		anim = gameObject.GetComponent<Animator> ();
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.A)) {
			if (anim.GetBool ("Cry")) {
				anim.SetBool ("Cry", false);
			}
			else if (anim.GetBool ("Cry") == false) {
				anim.SetBool ("Cry", true);
			}
		}
		
	}
}
