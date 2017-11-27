using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailListBehavior : MonoBehaviour {

	// Use this for initialization
	Animator anim;
	public static MailListBehavior instance;

	void Start () {
		instance = this;

		anim = GetComponent<Animator> (); 
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ShowMailList(){
		anim.SetBool ("Show", true);
	}
	public void HideMailList(){
		anim.SetBool ("Show", false);
	}
}
