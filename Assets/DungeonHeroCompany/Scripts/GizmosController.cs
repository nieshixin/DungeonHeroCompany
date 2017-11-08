using UnityEngine;
using System.Collections;



public class GizmosController : MonoBehaviour 
{

	private Vector3 screenPoint;
	private Vector3 offset;

	public GameObject table;

	private Rigidbody rigi;
	void OnMouseDown()
	{
		screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

		offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

		if (!rigi) {
			rigi = gameObject.GetComponent<Rigidbody> ();
		}
		rigi.useGravity = false;
		Debug.Log ("1");
	}
	void OnMouseUp(){
		if (!rigi) {
			rigi = gameObject.GetComponent<Rigidbody> ();
		}
		rigi.useGravity = true;
		Debug.Log ("2");
	}

	void OnMouseDrag()
	{
		Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

		Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset ;
		//if (curPosition.y <= table.transform.position.y) {
			curPosition.y = table.transform.position.y + 0.5f;
		//}

		transform.position = curPosition;

		}
	}