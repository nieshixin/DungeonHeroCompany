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
	public List<GameObject> upgradedPool_level_3;

	//public GameObject[] combinedBgs;

	public GemstoneType gemstoneType;//宝石类型  

	private GameObject gemstoneBg;  
	private Match3Controller Match3Controller;  
	[SerializeField]
	private SpriteRenderer spriteRenderer;  

	private Color spColor;
	private int targetIndex;
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

		//spColor = spriteRenderer.color;

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



	public void CreateUpgradedGemstone(int chain, GemstoneType t){//create special gemstone
		if (gemstoneBg != null) {
			return;  
		}

		//loop through the special pool, fide the gemstone contains the right type, return it, and spawn it
	
		GameObject compare;
		//int targetIndex;
		switch(chain){
		/*
		case 6:
			for (int i = 0; i < upgradedPool_level_3.Count; i++) { //in the upgraded gem pool, look for the type given
				compare = upgradedPool_level_3 [i] as GameObject;
				if (compare.GetComponent<GemType> ().type == t) {
					targetIndex = upgradedPool_level_3.IndexOf (compare);
					break;
				}
			}
			break;


		case 5:
			for (int i = 0; i < upgradedPool_level_3.Count; i++) { //in the upgraded gem pool, look for the type given
				compare = upgradedPool_level_3 [i] as GameObject;
				if (compare.GetComponent<GemType> ().type == t) {
					targetIndex = upgradedPool_level_3.IndexOf (compare);
					break;
				}
			}
			break;
*/
		case 4:
		  // check chain so we can spawn different tiers
			for (int i = 0; i < upgradedPool_level_2.Count; i++) { //in the upgraded gem pool, look for the type given
				compare = upgradedPool_level_2[i] as GameObject;
				if (compare.GetComponent<GemType> ().type == t) {
					targetIndex = upgradedPool_level_2.IndexOf (compare);
					break;
				}
			}
			break;

		case 5:
			// check chain so we can spawn different tiers
			for (int i = 0; i < upgradedPool_level_2.Count; i++) { //in the upgraded gem pool, look for the type given
				compare = upgradedPool_level_2[i] as GameObject;
				if (compare.GetComponent<GemType> ().type == t) {
					targetIndex = upgradedPool_level_2.IndexOf (compare);
					break;
				}
			}
			break;
		case 6:
			
			// check chain so we can spawn different tiers
			for (int i = 0; i < upgradedPool_level_2.Count; i++) { //in the upgraded gem pool, look for the type given
				compare = upgradedPool_level_2[i] as GameObject;
				if (compare.GetComponent<GemType> ().type == t) {
					targetIndex = upgradedPool_level_2.IndexOf (compare);
					break;
				}
			}
			break;
		}

		//NOTICE!
		//if compare is null, targetIndex will result in 0, which will result the spawned gem is always the first one in the list!!!!


		gemstoneBg = Instantiate (upgradedPool_level_2[targetIndex]) as GameObject;  

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
		//calculate scores here
		Destroy (this.gameObject);  
		Destroy (gemstoneBg.gameObject);  
		Match3Controller = null;  
	}  

	public void OnMouseOver(){
	//	spColor.a = 255f;
		//spriteRenderer.color = spColor;
	}

	public void OnMouseExit(){
	//	spColor.a = 200f;
		//spriteRenderer.color = spColor; 
	}
}