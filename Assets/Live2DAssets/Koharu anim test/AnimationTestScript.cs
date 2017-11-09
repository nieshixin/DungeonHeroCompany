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
		if (Input.GetKeyDown(KeyCode.Q)) {
			anim.SetBool ("Dance",true);
		}
		if (Input.GetKeyDown(KeyCode.W)) {
			anim.SetBool ("Dance",false);
		}
		if (Input.GetKeyDown(KeyCode.A)) {
			anim.SetTrigger ("Normal");
		}
		else if (Input.GetKeyDown(KeyCode.S)) {
			anim.SetTrigger ("Happy");
		}
		else if (Input.GetKeyDown(KeyCode.D)) {
			anim.SetTrigger ("Cry");
		}
		
	}
}
