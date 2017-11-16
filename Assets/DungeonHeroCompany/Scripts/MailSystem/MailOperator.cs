using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
		go.GetComponent<MailData> ().heroData = data;
		//update position
		go.transform.localPosition = new Vector3 (10f, -1 * transform.childCount * 25, 0);
	}


}
