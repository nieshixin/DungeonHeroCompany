using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MailOperator : MonoBehaviour {//singleton
	//this script gets data from mailManager, then instantiate mails and attach the data to the mail
	private static MailOperator theInstance;

	public static MailOperator instance {
		get {
			if (!theInstance) {
				var inst = FindObjectOfType<MailOperator> ();
				theInstance = inst ? inst : new GameObject ().AddComponent<MailOperator> ();
			}
			return theInstance;
		}
	}

	[SerializeField]
	public GameObject mailPrefab;
	[SerializeField]
	private GameObject parentToSpawn;
	void Start () {
		parentToSpawn = GameObject.FindGameObjectWithTag ("MailList");

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void GenerateHeroMail(HeroData data){
		//instantiate mail, with no data
		GameObject go =  Instantiate(mailPrefab, parentToSpawn.transform) as GameObject;
		//add data
		if (data != null) {
			go.GetComponent<MailData> ().heroData = data;
		}
		/*
		//update position
		if (transform.childCount == 0) {
			go.transform.localPosition = new Vector3 (5f, -5f, 0);
		} else {
			//get the width and height of the mail prefab

			go.transform.localPosition = new Vector3 (5f, -1 * transform.childCount * 35 - 5, 0);

		}
		//Debug.Log (go.transform.localPosition);
		*/
	}


}
