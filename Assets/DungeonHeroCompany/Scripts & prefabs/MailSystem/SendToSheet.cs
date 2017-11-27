using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SendToSheet : MonoBehaviour { //attach this on each mail

	private Button btn;
	// Use this for initialization
	void Start () {
		btn = gameObject.GetComponent<Button> ();
		btn.onClick.AddListener (SendDataToSheet);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SendDataToSheet(){
		//hide the mail panel
		MailListBehavior.instance.HideMailList();

		if (gameObject.GetComponent<MailData> ().heroData != null) {//if this mal contains hero data
			//slide the sheet down
			SheetOperator.instance.SlideDown ();
		}
	}
}
