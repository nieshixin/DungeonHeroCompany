using UnityEngine;
using UnityEditor;

public class HeroDataAsset
{
	[MenuItem("Assets/Create/Hero Data")]
	public static void CreateAsset ()
	{
		ScriptableObjectUtility.CreateAsset<HeroData> ();
	}
}