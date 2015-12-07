using UnityEngine;
using System;
using System.Collections.Generic;

/// <summary>
/// The character class holds data for and displays an instance
/// of a character.
/// </summary>
public class Character
{
	// World coordinates
	public World world;
	public int x;
	public int y;
	
	// New coordinates after transitioning
	public int newX;
	public int newY;

	// Character stats
	public int health;
	public int maxHealth;
	public int attack;
	public int defense;
	public bool friendly;

	// Character Sprite Sheets
	public static Sprite[] charBase;
	public static Sprite[] charHair;
	public static Sprite[] charHead;
	public static Sprite[] charLegs;
	public static Sprite[] charShield;
	public static Sprite[] charTorso;
	public static Sprite[] charWeapon;

	// Display Components
	public GameObject container;
	private SpriteRenderer baseComponent;
	private SpriteRenderer hairComponent;
	private SpriteRenderer legsComponent;
	private SpriteRenderer torsoComponent;
	private SpriteRenderer headComponent;
	private SpriteRenderer shieldComponent;
	private SpriteRenderer weaponComponent;
	private bool _visible;
	public bool visible
	{
		get
		{
			return _visible;
		}
		set
		{
			if (value)
			{
				if (!_visible)
				{
					_visible = true;
					container = new GameObject("CharContainer");
					GameObject charComponent = new GameObject("CharWeapon");
					weaponComponent = charComponent.AddComponent<SpriteRenderer>();
					weaponComponent.sortingLayerName = "Characters";
					charComponent.transform.position = new Vector3(0, 16, 0);
					charComponent.transform.parent = container.transform;
					charComponent = new GameObject("CharShield");
					shieldComponent = charComponent.AddComponent<SpriteRenderer>();
					shieldComponent.sortingLayerName = "Characters";
					charComponent.transform.position = new Vector3(0, 16, 0);
					charComponent.transform.parent = container.transform;
					charComponent = new GameObject("CharHead");
					headComponent = charComponent.AddComponent<SpriteRenderer>();
					headComponent.sortingLayerName = "Characters";
					charComponent.transform.position = new Vector3(0, 16, 0);
					charComponent.transform.parent = container.transform;
					charComponent = new GameObject("CharTorso");
					torsoComponent = charComponent.AddComponent<SpriteRenderer>();
					torsoComponent.sortingLayerName = "Characters";
					charComponent.transform.position = new Vector3(0, 16, 0);
					charComponent.transform.parent = container.transform;
					charComponent = new GameObject("CharLegs");
					legsComponent = charComponent.AddComponent<SpriteRenderer>();
					legsComponent.sortingLayerName = "Characters";
					charComponent.transform.position = new Vector3(0, 16, 0);
					charComponent.transform.parent = container.transform;
					charComponent = new GameObject("CharHair");
					hairComponent = charComponent.AddComponent<SpriteRenderer>();
					hairComponent.sortingLayerName = "Characters";
					charComponent.transform.position = new Vector3(0, 16, 0);
					charComponent.transform.parent = container.transform;
					charComponent = new GameObject("CharBase");
					baseComponent = charComponent.AddComponent<SpriteRenderer>();
					baseComponent.sortingLayerName = "Characters";
					charComponent.transform.position = new Vector3(0, 16, 0);
					charComponent.transform.parent = container.transform;
					if (_baseType != -1)
					{
						baseComponent.sprite = charBase[_baseType];
					}
					else
					{
						baseComponent.sprite = null;
					}
					if (_hairType != -1)
					{
						hairComponent.sprite = charHair[_hairType];
					}
					else
					{
						hairComponent.sprite = null;
					}
					if (_legsType != -1)
					{
						legsComponent.sprite = charLegs[_legsType];
					}
					else
					{
						legsComponent.sprite = null;
					}
					if (_torsoType != -1)
					{
						torsoComponent.sprite = charTorso[_torsoType];
					}
					else
					{
						torsoComponent.sprite = null;
					}
					if (_headType != -1)
					{
						headComponent.sprite = charHead[_headType];
					}
					else
					{
						headComponent.sprite = null;
					}
					if (_shieldType != -1)
					{
						shieldComponent.sprite = charShield[_shieldType];
					}
					else
					{
						shieldComponent.sprite = null;
					}
					if (_weaponType != -1)
					{
						weaponComponent.sprite = charWeapon[_weaponType];
					}
					else
					{
						weaponComponent.sprite = null;
					}
				}
			}
			else
			{
				if (_visible)
				{
					_visible = false;
					Game.RemoveObject(container);
				}
			}
		}
	}

	// Data Components
	private int _baseType;
	private int _hairType;
	private int _legsType;
	private int _torsoType;
	private int _headType;
	private int _shieldType;
	private int _weaponType;
	public int baseType
	{
		get { return _baseType; }
		set
		{
			_baseType = value;
			if (_visible)
			{
				if (value == -1)
				{
					baseComponent.sprite = null;
				}
				else
				{
					baseComponent.sprite = charBase[_baseType];
				}
			}
		}
	}
	public int hairType
	{
		get { return _hairType; }
		set
		{
			_hairType = value;
			if (_visible)
			{
				if (value == -1)
				{
					hairComponent.sprite = null;
				}
				else
				{
					hairComponent.sprite = charHair[_hairType];
				}
			}
		}
	}
	public int legsType
	{
		get { return _legsType; }
		set
		{
			_legsType = value;
			if (_visible)
			{
				if (value == -1)
				{
					legsComponent.sprite = null;
				}
				else
				{
					legsComponent.sprite = charLegs[_legsType];
				}
			}
		}
	}
	public int torsoType
	{
		get { return _torsoType; }
		set
		{
			_torsoType = value;
			if (_visible)
			{
				if (value == -1)
				{
					torsoComponent.sprite = null;
				}
				else
				{
					torsoComponent.sprite = charTorso[_torsoType];
				}
			}
		}
	}
	public int headType
	{
		get { return _headType; }
		set
		{
			_headType = value;
			if (_visible)
			{
				if (value == -1)
				{
					headComponent.sprite = null;
				}
				else
				{
					headComponent.sprite = charHead[_headType];
				}
			}
		}
	}
	public int shieldType
	{
		get { return _shieldType; }
		set
		{
			_shieldType = value;
			if (_visible)
			{
				if (value == -1)
				{
					shieldComponent.sprite = null;
				}
				else
				{
					shieldComponent.sprite = charShield[_shieldType];
				}
			}
		}
	}
	public int weaponType
	{
		get { return _weaponType; }
		set
		{
			_weaponType = value;
			if (_visible)
			{
				if (value == -1)
				{
					weaponComponent.sprite = null;
				}
				else
				{
					weaponComponent.sprite = charWeapon[_weaponType];
				}
			}
		}
	}
	public CharacterBehavior behavior;

	/// <summary>
	/// Initializes a new instance of the <see cref="Character"/> class.
	/// </summary>
	/// <param name="world">World character currently resides in.</param>
	/// <param name="x">The tile x position.</param>
	/// <param name="y">The tile y position.</param>
	/// <param name="behavior">The type of behavior controlling this character</param>
	public Character(World world, int x, int y, BehaviorType behavior = BehaviorType.None)
	{
		this.world = world;
		this.x = this.newX = x;
		this.y = this.newY = y;
		_baseType = -1;
		_hairType = -1;
		_legsType = -1;
		_torsoType = -1;
		_headType = -1;
		_shieldType = -1;
		_weaponType = -1;
		this.behavior = new CharacterBehavior(this, behavior);
		// Set default stats based on behavior
		switch (behavior)
		{
			case BehaviorType.Villager:
				this.maxHealth = this.health = 100;
				this.attack = 5;
				this.defense = 0;
				this.friendly = true;
				break;
			case BehaviorType.None:
			default:
				this.maxHealth = this.health = 1;
				this.attack = this.defense = 0;
				this.friendly = true;
				break;
		}
	}

	/// <summary>
	/// Update this character instance based on the world and characters.
	/// </summary>
	/// <param name="characters">The list of characters.</param>
	public void Update(List<Character> characters)
	{
		behavior.Update(characters);
	}

	/// <summary>
	/// Finishs the transition updating the current coordinates
	/// of the character.
	/// </summary>
	public void finishTransition()
	{
		this.x = this.newX;
		this.y = this.newY;
	}
}