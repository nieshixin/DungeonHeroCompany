using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceOperator : MonoBehaviour {
	[HideInInspector]
	public static DeviceOperator instance;
	// Use this for initialization

	public GameObject originalPos;
	public GameObject destinationPos;

	void Start () {
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void ShowDevice(){
		iTween.MoveTo (gameObject, iTween.Hash (
			"position", destinationPos.transform.position,
			"time", 0.3f,
			"easetype",iTween.EaseType.easeOutQuad
		));
	}

	public void CloseDevice(){
		iTween.MoveTo (gameObject, iTween.Hash (
			"position", originalPos.transform.position,
			"time", 0.3f,
			"easetype",iTween.EaseType.easeOutQuad
		));
	}
}
