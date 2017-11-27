using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheetOperator : MonoBehaviour {
	
	private static SheetOperator theInstance;
	[HideInInspector]
	public static SheetOperator instance {
		get {
			if (!theInstance) {
				var inst = FindObjectOfType<SheetOperator> ();
				theInstance = inst ? inst : new GameObject ().AddComponent<SheetOperator> ();
			}
			return theInstance;
		}
	}

	public GameObject originalPos;
	public GameObject destinationPos;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SlideDown(){
		iTween.MoveTo (gameObject, iTween.Hash (
			"position", destinationPos.transform.position,
			"time", 0.5f,
			"easetype",iTween.EaseType.easeOutQuad
		));
	}

	public void SlideUp(){
		iTween.MoveTo (gameObject, iTween.Hash (
			"position", originalPos.transform.position,
			"time", 0.5f,
			"easetype",iTween.EaseType.easeOutQuad
		));
	}
}
