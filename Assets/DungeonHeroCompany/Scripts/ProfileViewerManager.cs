using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfileViewerManager : MonoBehaviour {

	private static ProfileViewerManager theInstance;
	//[SerializeField]
	//GameObject blurMask; 
	[SerializeField]
	GameObject bg; //backgroud page
	public static ProfileViewerManager instance {
		get {
			if (!theInstance) {
				var inst = FindObjectOfType<ProfileViewerManager> ();
				theInstance = inst ? inst : new GameObject ().AddComponent<ProfileViewerManager> ();
			}
			return theInstance;
		}
	}


	// Use this for initialization
	void Start () {
		theInstance = this; 

		//blurMask = transform.Find ("Blur").gameObject;
		bg = transform.Find ("Backgroud").gameObject;
			
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Blur(){
		//blurMask.SetActive (true);
	}
	void UnBlur(){
		//blurMask.SetActive (false);
	}

	public void ViewProfile(){
		//Blur ();
		bg.SetActive (true);
		Debug.Log ("panel open");
	}


	public void UnviewProfile(){
		//DUnBlur ();
		bg.SetActive (false);
		Debug.Log ("panel closed");
	}
}
