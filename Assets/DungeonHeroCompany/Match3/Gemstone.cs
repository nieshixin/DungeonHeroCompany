using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gemstone : MonoBehaviour {

	public float xOffset = -4.5f;//x方向的偏移  
	public float yOffset = -2.0f;//y方向的偏移  
	public int rowIndex = 0;  
	public int columIndex = 0;  
	public GameObject[] gemstoneBgs;//宝石数组  
	public int gemstoneType;//宝石类型  
	private GameObject gemstoneBg;  
	private Match3Controller Match3Controller;  
	[SerializeField]
	private SpriteRenderer spriteRenderer;  
	public bool isSelected{  
		set{  
			if(value){  
				spriteRenderer.color=Color.red;  
			}else{  
				spriteRenderer.color=Color.white;  
			}  
		}  
	}  
	// Use this for initialization  
	void Start () {  
		Match3Controller = GameObject.Find ("Match3Controller").GetComponent<Match3Controller> ();  
		spriteRenderer = gemstoneBg.GetComponent<SpriteRenderer> ();  
	}  

	// Update is called once per frame  
	void Update () {  

	}  
	public void UpdatePosition(int _rowIndex,int _columIndex){//宝石的位置  
		rowIndex = _rowIndex;  
		columIndex = _columIndex;  
		this.transform.position = new Vector3 (columIndex + xOffset, rowIndex + yOffset, 0);  
	}  
	public void TweenToPosition(int _rowIndex,int _columIndex){//调用iTween插件实现宝石滑动效果  
		rowIndex = _rowIndex;  
		columIndex = _columIndex;  
		iTween.MoveTo (this.gameObject, iTween.Hash ("x", columIndex + xOffset, "y", rowIndex + yOffset, "time", 0.5f));  
	}  
	public void RandomCreateGemstoneBg(){//随机的宝石类型  
		if (gemstoneBg != null)   
			return;  
		gemstoneType = Random.Range (0, gemstoneBgs.Length);  
		gemstoneBg = Instantiate (gemstoneBgs [gemstoneType])as GameObject;  
		gemstoneBg.transform.parent = this.transform;  
	}  
	public void OnMouseDown(){  

		Match3Controller.Select (this);  
	}  

	/*
	public void OnMouseUp(){
		Debug.Log (this.rowIndex + " ," + this.columIndex);
		Match3Controller.MatchCheck (this);
	}
	*/
	public void Dispose(){  
		Destroy (this.gameObject);  
		Destroy (gemstoneBg.gameObject);  
		Match3Controller = null;  
	}  
}