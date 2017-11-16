using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScaler : MonoBehaviour {

	public static InventoryScaler instance;
		
	// Use this for initialization
	void Start () {
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void UpgradeInventorySlots(){// upgrade slots by adding 1 row
		Inventory mi = GameObject.FindGameObjectWithTag ("MainInventory").GetComponent<Inventory> ();
		mi.height += 1;
		mi.updateSlotAmount ();
		mi.updateSlotSize ();
		mi.adjustInventorySize ();
		RescaleSlots ();

	}

	public void RescaleSlots(){ // when in different resolution, the canvas scaler will scale every children in slots, we scale them back to 1
		
		Transform t = GameObject.FindGameObjectWithTag ("MainInventory").transform.Find ("Slots - Inventory");
			foreach (Transform slot in t) {
				slot.localScale = new Vector3 (1f, 1f, 1f);
				foreach (Transform item in slot) {
				
						item.localScale = new Vector3 (1f, 1f, 1f);
						foreach (Transform part in item) {
							
							part.localScale = new Vector3 (1f, 1f, 1f);
							}
						}
					}
				}
			

		

}
