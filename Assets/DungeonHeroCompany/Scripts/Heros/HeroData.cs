using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroData: ScriptableObject {
	public enum Destination{A,B,C,D,E,F,G
	}

	[SerializeField]
	string _name;
	public string Name
	{
		get
		{
			return _name;
		}

		set
		{
			_name = value;
		}
	}

	[SerializeField]
	float _health;
	public float Health
	{
		get
		{
			return _health;
		}

		set
		{
			_health = value;
		}
	}

	[SerializeField]
	Sprite _icon;
	public Sprite Icon
	{
		get
		{
			return _icon;
		}

		set
		{
			_icon = value;
		}
	}
		
}
