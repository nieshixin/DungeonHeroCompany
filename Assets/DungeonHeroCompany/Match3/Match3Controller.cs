using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Match3Controller : MonoBehaviour {  
	public Match3Controller instance;

	public Gemstone gemstone;  
	public int rowNum=7;//宝石列数  
	public int columNum=10;//宝石行数  
	public ArrayList gemstoneList;//定义列表  
	private Gemstone currentGemstone;  
	private ArrayList matchesGemstone;  
	public AudioClip match3Clip;  
	public AudioClip swapClip;  
	public AudioClip erroeClip;  
	AudioSource audi;

	private Gemstone exchangeBack;
	[SerializeField]
	private List<GemstoneType> priorSpawnInfo;

	void Start () {  
		instance = this;

		priorSpawnInfo = new List<GemstoneType> ();

		gemstoneList = new ArrayList ();//新建列表  
		matchesGemstone = new ArrayList ();  
		Initialize ();

		 audi = this.gameObject.GetComponent<AudioSource> ();  
	}

	void Initialize(){
		for (int rowIndex=0; rowIndex<rowNum; rowIndex++) {  

			ArrayList temp=new ArrayList();  

			for(int columIndex=0;columIndex<columNum;columIndex++){  
				Gemstone c = AddRegularGemstone(rowIndex,columIndex);  
				temp.Add(c);  

			}  
			gemstoneList.Add(temp);  
		}  
		if (CheckHorizontalMatches () || CheckVerticalMatches ()) {//开始检测匹配消除  
			RemoveMatches();  
		}
	}


	public Gemstone AddRegularGemstone(int rowIndex,int columIndex){//生成宝石  
		Gemstone c = Instantiate (gemstone)as Gemstone;  
		c.transform.parent = this.transform;//生成宝石为GameController子物体  
		c.GetComponent<Gemstone>().RandomCreateGemstoneBg();  
		c.GetComponent<Gemstone>().UpdatePosition(rowIndex,columIndex);  
		return c;  
	}  

	public Gemstone AddUpgradedGemstone(int rowIndex,int columIndex, int chain, GemstoneType t){//add corresponding upgraded gemstone when match 4+
		Gemstone c = Instantiate (gemstone)as Gemstone;  
		c.transform.parent = this.transform;//生成宝石为GameController子物体  
		Debug.Log("AddUpgradedGemstone() received type: " + t.ToString());
		c.GetComponent<Gemstone>().CreateUpgradedGemstone(chain, t);  
		c.GetComponent<Gemstone>().UpdatePosition(rowIndex,columIndex);  
		return c;  
	}  

	// Update is called once per frame  
	void Update () {  
		
	}  
	public void Select(Gemstone c){  
		//iTween.MoveTo (c.gameObject, iTween.Hash ());
		//Debug.Log ("x: " + c.rowIndex + ", y: " + c.columIndex);
		//Destroy (c.gameObject);  
		if (currentGemstone == null) {  
			currentGemstone = c;  
			currentGemstone.isSelected=true;  
			return;  
		}
		else {  
			if(Mathf.Abs(currentGemstone.rowIndex-c.rowIndex) == 0 || Mathf.Abs(currentGemstone.columIndex-c.columIndex) == 0){  //check neighbour tiles
				//ExangeAndMatches(currentGemstone,c);  
				StartCoroutine(ExangeAndMatches(currentGemstone,c));  
			}else{  
				this.gameObject.GetComponent<AudioSource>().PlayOneShot(erroeClip);  
			}  
			currentGemstone.isSelected=false;  
			currentGemstone=null;  
		}

	}

	IEnumerator ExangeAndMatches(Gemstone c1,Gemstone c2){//确定位置正确，开始check是否种type匹配
		int c1_row_before = c1.rowIndex;
		int c1_colum_before = c1.columIndex;
		int c2_row_before = c2.rowIndex;
		int c2_colum_before = c2.columIndex;

		Exchange2(c1,c2);  
		yield return new WaitForSeconds (0.2f);  
		if (CheckHorizontalMatches () || CheckVerticalMatches ()) {  //if matches, start matching
			RemoveMatches ();  
		} else {  //if doesn't match, exchange back
			Exchange2(c1,exchangeBack);  
		}  
	}  


	bool CheckHorizontalMatches(){//实现检测水平方向的匹配  
		bool isMatches = false;  
		for (int rowIndex=0; rowIndex<rowNum; rowIndex++) {  
			for (int columIndex=0; columIndex<columNum-2; columIndex++) {  
				if ((GetGemstone (rowIndex, columIndex).gemstoneType == GetGemstone (rowIndex, columIndex + 1).gemstoneType) && (GetGemstone (rowIndex, columIndex).gemstoneType == GetGemstone (rowIndex, columIndex + 2).gemstoneType)) {  
					//Debug.Log ("发现行相同的宝石");  
					AddMatches(GetGemstone(rowIndex,columIndex));  
					AddMatches(GetGemstone(rowIndex,columIndex+1));  
					AddMatches(GetGemstone(rowIndex,columIndex+2));  
					isMatches = true;  
				}  
			}  
		}  
		return isMatches;  
	}  

	bool CheckVerticalMatches(){//实现检测垂直方向的匹配  
		bool isMatches = false;  
		for (int columIndex=0; columIndex<columNum; columIndex++) {  
			for (int rowIndex=0; rowIndex<rowNum-2; rowIndex++) {  
				if ((GetGemstone (rowIndex, columIndex).gemstoneType == GetGemstone (rowIndex + 1, columIndex).gemstoneType) && (GetGemstone (rowIndex, columIndex).gemstoneType == GetGemstone (rowIndex + 2, columIndex).gemstoneType)) {  
					//Debug.Log("发现列相同的宝石");  
					AddMatches(GetGemstone(rowIndex,columIndex));  
					AddMatches(GetGemstone(rowIndex+1,columIndex));  
					AddMatches(GetGemstone(rowIndex+2,columIndex));  
					isMatches=true;  
				}
			}  
		}  
		return isMatches;  
	}  

	void AddMatches(Gemstone c){  
		if (matchesGemstone == null)  
			matchesGemstone = new ArrayList ();  
		int Index = matchesGemstone.IndexOf (c);//检测宝石是否已在数组当中  
		if (Index == -1) {  
			matchesGemstone.Add(c);  
		}  
	}  
	void RemoveMatches(){//NOTE: HERE can get how many chain the match gets
		//how many match we get?
		//Debug.Log("Match NUM: " + matchesGemstone.Count);

		/////
		//Modify container!!
		if (matchesGemstone.Count >= 4) {
			Debug.Log ("REMOVE MATCHES  " + matchesGemstone.Count);
			Gemstone sample = matchesGemstone [0] as Gemstone;
			GemstoneType t = sample.gemstoneType;
			Debug.Log (t.ToString());
			priorSpawnInfo.Add (t);
			Debug.Log ("in the list:" + priorSpawnInfo [0].ToString ());
		}

		for (int i=0; i < matchesGemstone.Count; i++) {  
			Gemstone c=matchesGemstone[i]as Gemstone;  
			//Debug.Log ("TYPE" + c.gemstoneType);
			RemoveGemstone(c);  
		}  

		matchesGemstone = new ArrayList ();  
		StartCoroutine (WaitForCheckMatchesAgain ());  
	}  


	IEnumerator WaitForCheckMatchesAgain(){//连续检测匹配消除  
		yield return new WaitForSeconds (0.5f);  
		if (CheckHorizontalMatches () || CheckVerticalMatches ()) {  
			RemoveMatches();  
		}  
	}  

	//very important: this function is: REMOVE+ GENERATE new gemstones
	//only responsible for individual spawning, should not know anything about the chain number, RemoveMatches() should know the chain number
	//the prioerSpawnContainer has the highest spawn priority
	void RemoveGemstone(Gemstone c){//删除宝石  
		//Debug.Log("删除宝石");  
		c.Dispose ();  
		//play sound
		this.gameObject.GetComponent<AudioSource> ().PlayOneShot (match3Clip);  
		//all the gemstones fall
		for (int i=c.rowIndex+1; i<rowNum; i++) {  
			Gemstone temGemstone=GetGemstone(i,c.columIndex);  
			temGemstone.rowIndex--;  
			SetGemstone(temGemstone.rowIndex,temGemstone.columIndex,temGemstone);  
			//temGemstone.UpdatePosition(temGemstone.rowIndex,temGemstone.columIndex);  
			temGemstone.TweenToPosition(temGemstone.rowIndex,temGemstone.columIndex);  
		}  


		//START SPAWNING
		Gemstone newGemstone;

		//check the priorSpawninfo, if has anything, spawn upgraded gems first
		if(priorSpawnInfo.Count != 0){
			Debug.Log ("has prior spawns  " + priorSpawnInfo.Count);
			Debug.Log ("the chain is : " + matchesGemstone.Count);
			newGemstone = AddUpgradedGemstone (rowNum, c.columIndex, matchesGemstone.Count , priorSpawnInfo[0]);  
			priorSpawnInfo.RemoveAt (0);
			Debug.Log ("remove complete "+ priorSpawnInfo.Count);
		}else{
			newGemstone = AddRegularGemstone (rowNum, c.columIndex);  
		}
		

		newGemstone.rowIndex--;  
		SetGemstone (newGemstone.rowIndex, newGemstone.columIndex, newGemstone);  
		//newGemstone.UpdatePosition (newGemstone.rowIndex, newGemstone.columIndex);  
		newGemstone.TweenToPosition (newGemstone.rowIndex, newGemstone.columIndex);  
	}  


	public Gemstone GetGemstone(int rowIndex,int columIndex){//通过行号和列号，获取对应位置的宝石  
		ArrayList temp = gemstoneList [rowIndex]as ArrayList;  
		Gemstone c = temp [columIndex]as Gemstone;  
		return c;  
	}  


	public void SetGemstone(int rowIndex,int columIndex,Gemstone c){//设置所对应行号和列号的宝石  
		ArrayList temp = gemstoneList [rowIndex]as ArrayList;  
		temp [columIndex] = c;  
	}  
	public void Exchange(Gemstone c1,Gemstone c2){//实现宝石交换位置  
		this.gameObject.GetComponent<AudioSource> ().PlayOneShot (swapClip);  
		SetGemstone (c1.rowIndex, c1.columIndex, c2);  
		SetGemstone (c2.rowIndex, c2.columIndex, c1);  
		//交换c1，c2的行号  
		int tempRowIndex;  
		tempRowIndex = c1.rowIndex;  
		c1.rowIndex = c2.rowIndex;  
		c2.rowIndex = tempRowIndex;  
		//交换c1，c2的列号  
		int tempColumIndex;  
		tempColumIndex = c1.columIndex;  
		c1.columIndex = c2.columIndex;  
		c2.columIndex = tempColumIndex;  

		//c1.UpdatePosition (c1.rowIndex, c1.columIndex);  
		//c2.UpdatePosition (c2.rowIndex, c2.columIndex);  
		c1.TweenToPosition (c1.rowIndex, c1.columIndex);  
		c2.TweenToPosition (c2.rowIndex, c2.columIndex);  
	}  


	public void Exchange2 (Gemstone c1,Gemstone c2){//实现宝石交换位置  , c1: original, c2: destination

		int originalRow = c1.rowIndex;
		int originalColum = c1.columIndex;
		int destRow = c2.rowIndex;
		int destColum = c2.columIndex;



		if (c1.rowIndex == c2.rowIndex ) {//when they are on the same ROW, we are modifying and comparing the columns
			
			int tempGemNum = Mathf.Abs (c1.columIndex - c2.columIndex);
			if ((c1.columIndex - c2.columIndex) < 0) {  //if the original gem is moving upward, affected gems move downwards

				for (int i = 0; i < tempGemNum; i++) { //except the original c1, all affected gems move 1 down 
					Gemstone temp = GetGemstone (c1.rowIndex, c1.columIndex + 1 + i); //because the original one is at below, 向上遍历
					SetGemstone (temp.rowIndex, temp.columIndex - 1, temp);
					temp.columIndex -= 1;
					//finally the visual tween move
					temp.TweenToPosition (temp.rowIndex, temp.columIndex);
				}
				//play sound effect
				StartCoroutine(playClip(swapClip, tempGemNum));
			} else {// when the original gem is moving downwards, affected gems move upwards

				for (int i = 0; i < tempGemNum; i++) { //except the original c1, all affected gems move 1 down 
					Gemstone temp = GetGemstone (c1.rowIndex, c1.columIndex - 1 - i); //同理，向下遍历
					SetGemstone (temp.rowIndex, temp.columIndex + 1, temp);
					temp.columIndex += 1;
					//finally the visual tween move
					temp.TweenToPosition (temp.rowIndex, temp.columIndex);
				}
				//play sound effect
				StartCoroutine(playClip(swapClip, tempGemNum));
			}
		}
			if (c1.columIndex == c2.columIndex ) {//when they are on the same COLUM, we are modifying and comparing the rows

				int tempGemNum2 = Mathf.Abs (c1.rowIndex - c2.rowIndex);
				if ((c1.rowIndex - c2.rowIndex) < 0) {  //if the original gem is moving upward, affected gems move downwards
				
					for (int i = 0; i < tempGemNum2; i++) { //except the original c1, all affected gems move 1 down 
						Gemstone temp = GetGemstone (c1.rowIndex + 1 + i, c1.columIndex); //because the original one is at below, 向上遍历
						SetGemstone (temp.rowIndex - 1, temp.columIndex, temp);
						temp.rowIndex -= 1;
					//finally the visual tween move
					temp.TweenToPosition (temp.rowIndex, temp.columIndex);


					}
				//play sound effect
				StartCoroutine(playClip(swapClip, tempGemNum2));

				} else {// when the original gem is moving downwards, affected gems move upwards

					for (int i = 0; i < tempGemNum2; i++) { //except the original c1, all affected gems move 1 down 
						Gemstone temp = GetGemstone (c1.rowIndex - 1 - i, c1.columIndex); //同理，向下遍历
						SetGemstone (temp.rowIndex + 1, temp.columIndex, temp);
						temp.rowIndex += 1;
					//finally the visual tween move
					temp.TweenToPosition (temp.rowIndex, temp.columIndex);


					}
				//play sound effect
				StartCoroutine(playClip(swapClip, tempGemNum2));
				}
			}

		SetGemstone (destRow, destColum, c1); 
		c1.rowIndex = destRow;
		c1.columIndex = destColum;
		//finally the visual tween move
		c1.TweenToPosition (c1.rowIndex, c1.columIndex);
		//play sound effect
		//audi.PlayOneShot (swapClip);

		exchangeBack = GetGemstone (originalRow,originalColum);
		//	c1.TweenToPosition (c1.rowIndex, c1.columIndex);  
		//c2.TweenToPosition (c2.rowIndex, c2.columIndex);  
	}

	IEnumerator playClip(AudioClip theClip, int repeat){
		
		for (int i = 0; i < repeat; i++) {
			audi.PlayOneShot (theClip);	
			yield return new WaitForSeconds (0.03f);  
		}
	

	}
}