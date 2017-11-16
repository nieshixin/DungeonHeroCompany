using UnityEngine;
using UnityEditor;

public class HeroDataListAsset
{
	[MenuItem("Assets/Create/Hero Data List")]
	public static void CreateAsset ()
	{
		ScriptableObjectUtility.CreateAsset<HeroDataList> ();
	}
}