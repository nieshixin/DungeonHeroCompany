using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleToHeroEffects : MonoBehaviour {
	[HideInInspector]
	public static ParticleToHeroEffects instance;
	//particle prefab
	public GameObject particle;
	public GameObject destination;
	public Transform[] paths;
	void Start(){
		instance = this;
	}
	void Update () 
	{

	}



	public void SpawnParticles(Transform t){
		GameObject go = Instantiate (particle, t.position, Quaternion.identity, transform) as GameObject;
		iTween.MoveTo (go, iTween.Hash (
			//"position", destination.transform.position,
			"path", paths,
			"time", 0.7f,
			"easetype", "easeOutQuad",
			"oncomplete", "OnFinishAnimation",
			"oncompleteparams", go));
	}

}
