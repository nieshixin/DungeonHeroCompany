using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailDataManager : MonoBehaviour {//singleton
	[SerializeField]
	public HeroData[] heroList;

	private static MailDataManager theInstance;

	public static MailDataManager instance {
		get {
			if (!theInstance) {
				var inst = FindObjectOfType<MailDataManager> ();
				theInstance = inst ? inst : new GameObject ().AddComponent<MailDataManager> ();
			}
			return theInstance;
		}
	}

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void AddOneHeroMail(){
		MailOperator.instance.GenerateHeroMail (heroList[1]);
		
	}

	public void AddOneSpamMail(){
	}
}
