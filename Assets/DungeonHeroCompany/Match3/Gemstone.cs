using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Gemstone : MonoBehaviour {

	public float xOffset = -4.5f;//x方向的偏移  
	public float yOffset = -2.0f;//y方向的偏移  
	public int rowIndex = 0;  
	public int columIndex = 0;  

	public List<GameObject> gemstoneBgs;//regular pool 

	public List<GameObject> upgradedPool_level_2;// upgraded pool, cannot spawn directly, can only spawn when match 4+
	//public GameObject[] combinedBgs;

	public GemstoneType gemstoneType;//宝石类型  

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

		//combine special gem spawn list with regular spawn list:

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

	/// <summary>
	/// //NOTE:
	/// When create Gemstones, as you instantiate the gemstones, don't forget to get the type from the gemstoneBg (the prefab which has sprite renderer), and assign it to the type in this script!!!!
	/// </summary>

	public void RandomCreateGemstoneBg(){//随机的宝石类型 , special gems NOT included
		if (gemstoneBg != null)   
			return;  
		//generate a random index
		int randomIndex = Random.Range (0, gemstoneBgs.Count());
		//from the regular pool, generate a random gemstone
		gemstoneBg = Instantiate (gemstoneBgs [randomIndex])as GameObject;  
		//assign the gemstone type on this script
		gemstoneType = gemstoneBg.GetComponent<GemType> ().type;

		gemstoneBg.transform.parent = this.transform;  
	}  



	public void CreateUpgradedGemstone(GemstoneType t){//create special gemstone
		if (gemstoneBg != null)   
			return;  

		//loop through the special pool, fide the gemstone contains the right type, return it, and spawn it

		GameObject go = new GameObject ();
		for (int i = 0; i < upgradedPool_level_2.Count; i++) {
			if (upgradedPool_level_2 [i].GetComponent<GemType> ().type == t) {
				go = upgradedPool_level_2 [i];
			}
		}
		gemstoneBg = Instantiate (go) as GameObject;  

		gemstoneType = gemstoneBg.GetComponent<GemType> ().type;
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